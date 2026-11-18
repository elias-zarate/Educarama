<?php
// src/Service/ProgresionService.php

declare(strict_types=1);

namespace Educarama\Service;

use Educarama\Model\Accion;
use Educarama\Model\AccionCumplida;
use Educarama\Model\Competencia;
use Educarama\Model\CompetenciaProtagonista;
use Educarama\Model\Enum\Elemento;
use Educarama\Model\Enum\EstadoProgresion;
use Educarama\Model\Enum\NivelRol;
use Educarama\Model\Especialidad;
use Educarama\Model\EspecialidadProtagonista;
use Educarama\Model\Etapa;
use Educarama\Model\EtapaLograda;
use Educarama\Model\InsigniaOtorgada;
use Educarama\Model\Rama;
use Educarama\Model\TareaEtapaCumplida;
use Educarama\Repository\AccionCumplidaRepository;
use Educarama\Repository\AccionRepository;
use Educarama\Repository\AreaRamaRepository;
use Educarama\Repository\AreaRepository;
use Educarama\Repository\CompetenciaProtagonistaRepository;
use Educarama\Repository\CompetenciaRepository;
use Educarama\Repository\EspecialidadProtagonistaRepository;
use Educarama\Repository\EspecialidadRepository;
use Educarama\Repository\EtapaLogradaRepository;
use Educarama\Repository\EtapaRepository;
use Educarama\Repository\InsigniaOtorgadaRepository;
use Educarama\Repository\InsigniaRepository;
use Educarama\Repository\NomenclaturaRepository;
use Educarama\Repository\ProtagonistaRepository;
use Educarama\Repository\RamaRepository;
use Educarama\Repository\SeccionRepository;
use Educarama\Repository\TareaEtapaCumplidaRepository;
use Educarama\Repository\TareaEtapaRepository;
use Educarama\Service\Excepcion\AccesoDenegadoException;
use Educarama\Service\Excepcion\NoEncontradoException;
use Educarama\Service\Excepcion\ReglaNegocioException;
use Educarama\Service\Excepcion\RequisitosPendientesException;
use Educarama\Service\Vista\AccionEnPantalla;
use Educarama\Service\Vista\AreaEnPantalla;
use Educarama\Service\Vista\Aviso;
use Educarama\Service\Vista\CompetenciaEnPantalla;
use Educarama\Service\Vista\EspecialidadEnPantalla;
use Educarama\Service\Vista\EtapaEnPantalla;
use Educarama\Service\Vista\InsigniaEnPantalla;
use Educarama\Service\Vista\ContadorArea;
use Educarama\Service\Vista\PantallaCompetencias;
use Educarama\Service\Vista\PantallaEtapas;
use Educarama\Service\Vista\ResultadoProgresion;
use Educarama\Service\Vista\ResumenProgresion;
use Educarama\Service\Vista\TareaEnPantalla;
use Educarama\Service\Vista\TipoAviso;
use PDO;

/**
 * Progresión personal de un protagonista: competencias, acciones, tareas de
 * etapa, etapas logradas, especialidades e insignias.
 *
 * Reglas (ver reglas del proyecto, "Progresión"):
 *  - Etapa actual = la última lograda. Etapa en curso = la siguiente (o la primera).
 *  - Cada etapa tiene un cupo fijo de competencias (etapa.competencias_etapa),
 *    contado por competencia_protagonista.id_etapa.
 *  - Nada se confirma solo. Los requisitos que faltan se avisan con una
 *    RequisitosPendientesException y se pueden saltear repitiendo la llamada
 *    con $confirmarIgual = true. Lo único que bloquea siempre es confirmar una
 *    etapa sin tener lograda la anterior.
 *  - Permisos: leer, Ayudante; registrar y corregir, Responsable; quitar una
 *    etapa lograda o una insignia, Jefe. Sin ningún acceso a la sección del
 *    protagonista, NoEncontradoException (404): no se confirma que existe.
 *
 * Todas las operaciones reciben primero el id del usuario que la hace, y las
 * fechas son opcionales: si no se pasa, se usa la de hoy ('AAAA-MM-DD'). Una
 * fecha pasada se acepta (ej. la ceremonia del campamento del mes pasado);
 * una futura, no.
 */
final class ProgresionService extends ServicioBase
{
    /**
     * Caminantes es la única rama que elige elemento al lograr una etapa
     * (ver "Datos oficiales" en las reglas del proyecto). La tabla rama no tiene una columna
     * para esto, por eso va el id.
     */
    private const ID_RAMA_CAMINANTES = 3;


    public function __construct(
        PDO $conexion,
        private readonly PermisoService $permisos,
        private readonly ProtagonistaRepository $protagonistas,
        private readonly SeccionRepository $secciones,
        private readonly RamaRepository $ramas,
        private readonly EtapaRepository $etapas,
        private readonly CompetenciaRepository $competencias,
        private readonly AccionRepository $acciones,
        private readonly TareaEtapaRepository $tareas,
        private readonly InsigniaRepository $insignias,
        private readonly EspecialidadRepository $especialidades,
        private readonly AreaRamaRepository $areasRama,
        private readonly CompetenciaProtagonistaRepository $competenciasProtagonista,
        private readonly AccionCumplidaRepository $accionesCumplidas,
        private readonly TareaEtapaCumplidaRepository $tareasCumplidas,
        private readonly EtapaLogradaRepository $etapasLogradas,
        private readonly EspecialidadProtagonistaRepository $especialidadesProtagonista,
        private readonly InsigniaOtorgadaRepository $insigniasOtorgadas,
        private readonly NomenclaturaRepository $nomenclaturas,
        private readonly AreaRepository $areas
    ) {
        parent::__construct($conexion);
    }

    // =================================================================
    // Lectura
    // =================================================================

    /**
     * Situación de la progresión en la rama de la sección del protagonista:
     * etapas, contadores, lo adeudado para confirmar la etapa en curso y la
     * insignia de esa etapa si todavía no la recibió. (Ayudante en adelante.)
     */
    public function resumen(int $idUsuario, int $idProtagonista): ResumenProgresion
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Ayudante);
        return $this->armarResumen($idProtagonista, $rama);
    }

    /**
     * Las insignias de la rama del protagonista, cada una con su fecha de
     * entrega (o null si no la recibió), el elemento elegido en cada etapa
     * lograda (solo Caminantes) y el número de etapa de cada insignia
     * (para posicionarlas en el rompecabezas del perfil).
     *
     * @return array{insignias: InsigniaEnPantalla[], elementoPorEtapa: array<int, \Educarama\Model\Enum\Elemento>, numeroPorEtapa: array<int, int>, idRama: int}
     */
    public function insigniasParaPerfil(int $idUsuario, int $idProtagonista): array
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Ayudante);

        $otorgadas = [];
        foreach ($this->insigniasOtorgadas->listarPorProtagonista($idProtagonista) as $otorgada) {
            $otorgadas[$otorgada->idInsignia] = $otorgada->fecha;
        }

        $insignias = array_map(
            fn ($insignia) => new InsigniaEnPantalla($insignia, $otorgadas[$insignia->id] ?? null),
            $this->insignias->listarParaRama($rama->id),
        );

        // En Caminantes la imagen de cada insignia de etapa sale del elemento
        // elegido al lograr esa etapa (agua, tierra, fuego, aire).
        $elementoPorEtapa = [];
        foreach ($this->etapasLogradas->listarPorProtagonista($idProtagonista) as $lograda) {
            if ($lograda->elemento !== null) {
                $elementoPorEtapa[$lograda->idEtapa] = $lograda->elemento;
            }
        }

        // Mapa id de etapa → número (1, 2, 3, 4) para posicionar en el rompecabezas.
        $numeroPorEtapa = [];
        foreach ($this->etapas->listarPorRama($rama->id) as $etapa) {
            $numeroPorEtapa[$etapa->id] = (int) $etapa->numero;
        }

        return [
            'insignias'       => $insignias,
            'elementoPorEtapa' => $elementoPorEtapa,
            'numeroPorEtapa'  => $numeroPorEtapa,
            'idRama'          => $rama->id,
        ];
    }

    /**
     * Todo lo que muestra la pantalla de competencias (Ayudante en adelante):
     * el resumen y, por cada área de la rama, todas sus competencias con su
     * estado (sin fila = pendiente) y sus acciones con "cumplida sí/no".
     * Además dice qué botones mostrar según el rol del usuario.
     */
    public function pantallaCompetencias(int $idUsuario, int $idProtagonista): PantallaCompetencias
    {
        [$rama, $nivel] = $this->cargarConNivel($idUsuario, $idProtagonista, NivelRol::Ayudante);
        $resumen = $this->armarResumen($idProtagonista, $rama);
        $elegidas = $this->competenciasPorId($idProtagonista);

        // Acciones de toda la rama agrupadas por competencia, con las cumplidas
        // marcadas: dos consultas en total, no una por competencia.
        $accionesPorCompetencia = [];
        if ($rama->usaAcciones) {
            $cumplidas = [];
            foreach ($this->accionesCumplidas->listarPorProtagonista($idProtagonista) as $ac) {
                $cumplidas[$ac->idAccion] = true;
            }
            foreach ($this->acciones->listarPorRama($rama->id) as $accion) {
                $accionesPorCompetencia[$accion->idCompetencia][] = new AccionEnPantalla($accion, isset($cumplidas[$accion->id]));
            }
        }

        $competenciasPorArea = [];
        foreach ($this->competencias->listarPorRama($rama->id) as $competencia) {
            $elegida = $elegidas[$competencia->id] ?? null;
            $competenciasPorArea[$competencia->idArea][] = new CompetenciaEnPantalla(
                competencia: $competencia,
                estado: $elegida?->estado,
                idEtapa: $elegida?->idEtapa,
                acciones: $accionesPorCompetencia[$competencia->id] ?? [],
            );
        }

        $areas = array_map(
            fn (ContadorArea $contador) => new AreaEnPantalla($contador, $competenciasPorArea[$contador->idArea] ?? []),
            $resumen->areas,
        );

        $nombresEtapas = [];
        foreach ($this->etapas->listarPorRama($rama->id) as $etapa) {
            $nombresEtapas[(int) $etapa->id] = $etapa->titulo;
        }

        return new PantallaCompetencias(
            resumen: $resumen,
            areas: $areas,
            nombresEtapas: $nombresEtapas,
            terminos: array_map(fn ($n) => $n->texto, $this->nomenclaturas->listarPorRama($rama->id)),
            puedeEditar: $nivel->value >= NivelRol::Responsable->value,
            puedeQuitar: $nivel->value >= NivelRol::Jefe->value,
        );
    }

    /**
     * Lo que muestra la pantalla "Etapas e insignias" (Ayudante en adelante):
     * las etapas de la rama (logradas con su fecha y elemento, la en curso con
     * sus tareas), las insignias que puede recibir (de la rama y generales) con
     * su fecha de entrega y, si la rama las usa, las especialidades por área.
     */
    public function pantallaEtapas(int $idUsuario, int $idProtagonista): PantallaEtapas
    {
        [$rama, $nivel] = $this->cargarConNivel($idUsuario, $idProtagonista, NivelRol::Ayudante);
        [$actual, $enCurso, $todas] = $this->situacionEtapas($idProtagonista, $rama);

        $logradas = [];
        foreach ($this->etapasLogradas->listarPorProtagonista($idProtagonista) as $lograda) {
            $logradas[$lograda->idEtapa] = $lograda;
        }
        $tareasCumplidas = [];
        foreach ($this->tareasCumplidas->listarPorProtagonista($idProtagonista) as $cumplida) {
            $tareasCumplidas[$cumplida->idTareaEtapa] = $cumplida->fecha;
        }

        $etapas = array_map(fn (Etapa $etapa) => new EtapaEnPantalla(
            etapa: $etapa,
            lograda: $logradas[$etapa->id] ?? null,
            enCurso: $enCurso?->id === $etapa->id,
            esLaActual: $actual?->id === $etapa->id,
            tareas: array_map(
                fn ($tarea) => new TareaEnPantalla($tarea, $tareasCumplidas[$tarea->id] ?? null),
                $this->tareas->listarPorEtapa((int) $etapa->id),
            ),
        ), $todas);

        $otorgadas = [];
        foreach ($this->insigniasOtorgadas->listarPorProtagonista($idProtagonista) as $otorgada) {
            $otorgadas[$otorgada->idInsignia] = $otorgada->fecha;
        }
        $insignias = array_map(
            fn ($insignia) => new InsigniaEnPantalla($insignia, $otorgadas[$insignia->id] ?? null),
            $this->insignias->listarParaRama($rama->id),
        );

        $especialidades = [];
        if ($rama->usaEspecialidades) {
            $suyas = [];
            foreach ($this->especialidadesProtagonista->listarPorProtagonista($idProtagonista) as $ep) {
                $suyas[$ep->idEspecialidad] = $ep;
            }
            $porArea = [];
            foreach ($this->especialidades->listarTodas() as $especialidad) {
                $suya = $suyas[$especialidad->id] ?? null;
                $porArea[$especialidad->idArea][] = new EspecialidadEnPantalla($especialidad, $suya?->estado, $suya?->fecha);
            }
            foreach ($this->areas->listarTodas() as $area) {
                if (isset($porArea[$area->id])) {
                    $especialidades[] = ['area' => $area, 'especialidades' => $porArea[$area->id]];
                }
            }
        }

        return new PantallaEtapas(
            resumen: $this->armarResumen($idProtagonista, $rama),
            etapas: $etapas,
            insignias: $insignias,
            especialidades: $especialidades,
            pideElemento: $rama->id === self::ID_RAMA_CAMINANTES,
            puedeEditar: $nivel->value >= NivelRol::Responsable->value,
            puedeQuitar: $nivel->value >= NivelRol::Jefe->value,
        );
    }

    /** El resumen, una vez controlado el permiso. */
    private function armarResumen(int $idProtagonista, Rama $rama): ResumenProgresion
    {
        [$actual, $enCurso] = $this->situacionEtapas($idProtagonista, $rama);
        $elegidas = $this->competenciasPorId($idProtagonista);

        // Contadores del total y de cada área, recorriendo el catálogo de la rama.
        // Se recorre el catálogo (y no lo elegido) para no contar competencias de
        // otra rama si el protagonista cambió de sección.
        $porArea = [];
        $desarrolladasTotal = 0;
        $catalogo = $this->competencias->listarPorRama($rama->id);

        foreach ($catalogo as $competencia) {
            $porArea[$competencia->idArea] ??= ['desarrolladas' => 0, 'total' => 0];
            $porArea[$competencia->idArea]['total']++;

            if (($elegidas[$competencia->id] ?? null)?->estado === EstadoProgresion::Desarrollada) {
                $porArea[$competencia->idArea]['desarrolladas']++;
                $desarrolladasTotal++;
            }
        }

        $areas = [];
        foreach ($this->areasRama->listarPorRama($rama->id) as $areaRama) {
            $areas[] = new ContadorArea(
                idArea: $areaRama->idArea,
                titulo: $areaRama->titulo,
                desarrolladas: $porArea[$areaRama->idArea]['desarrolladas'] ?? 0,
                total: $porArea[$areaRama->idArea]['total'] ?? 0,
            );
        }

        // Insignia de la etapa en curso: según el programa se entrega cuando la etapa empieza.
        $insigniaPendiente = null;
        if ($enCurso !== null) {
            $insignia = $this->insignias->buscarPorEtapa($enCurso->id);
            if ($insignia !== null && $this->insigniasOtorgadas->buscar($idProtagonista, $insignia->id) === null) {
                $insigniaPendiente = $insignia;
            }
        }

        return new ResumenProgresion(
            rama: $rama,
            etapaActual: $actual,
            etapaEnCurso: $enCurso,
            enCursoElegidas: $enCurso === null ? 0 : $this->contarEnEtapa($elegidas, $enCurso->id, false),
            enCursoDesarrolladas: $enCurso === null ? 0 : $this->contarEnEtapa($elegidas, $enCurso->id, true),
            desarrolladasTotal: $desarrolladasTotal,
            competenciasTotal: count($catalogo),
            areas: $areas,
            adeudado: $enCurso === null ? [] : $this->adeudado($idProtagonista, $enCurso),
            insigniaPendiente: $insigniaPendiente,
        );
    }

    // =================================================================
    // Competencias
    // =================================================================

    /**
     * Avanza un paso el estado de la competencia (el botón que solo avanza):
     *
     *  - Pendiente → En proceso: la elige para la etapa en curso (le asigna su
     *    id_etapa). Avisa si la etapa ya llegó a su cupo. Si ya logró todas las
     *    etapas, la suma a la última y avisa.
     *  - En proceso → Desarrollada: avisa si faltan acciones requeridas. Si con
     *    esta la etapa en curso completa su cupo, sugiere confirmar la etapa.
     *
     * @throws RequisitosPendientesException si hay avisos y no se pasó $confirmarIgual
     * @throws ReglaNegocioException si ya está desarrollada o no es de la rama
     */
    public function avanzarCompetencia(
        int $idUsuario,
        int $idProtagonista,
        int $idCompetencia,
        bool $confirmarIgual = false,
        ?string $fecha = null,
    ): ResultadoProgresion {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Responsable);
        $competencia = $this->competenciaDeRama($idCompetencia, $rama);
        $fecha = $this->fecha($fecha);
        $elegida = $this->competenciasProtagonista->buscar($idProtagonista, $idCompetencia);

        if ($elegida === null) {
            $this->elegirCompetencia($idProtagonista, $competencia, $rama, $confirmarIgual, $fecha);
            return new ResultadoProgresion();
        }

        if ($elegida->estado === EstadoProgresion::EnProceso) {
            return $this->desarrollarCompetencia($idProtagonista, $competencia, $elegida, $rama, $confirmarIgual, $fecha);
        }

        throw new ReglaNegocioException('La competencia ya está desarrollada.');
    }

    /**
     * Retrocede un paso el estado (modo "Corregir"):
     * Desarrollada → En proceso, o En proceso → Pendiente (borra la fila).
     * Las acciones cumplidas se conservan: si la vuelve a elegir, siguen ahí.
     */
    public function retrocederCompetencia(int $idUsuario, int $idProtagonista, int $idCompetencia): void
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Responsable);
        $this->competenciaDeRama($idCompetencia, $rama);
        $elegida = $this->competenciasProtagonista->buscar($idProtagonista, $idCompetencia)
            ?? throw new ReglaNegocioException('La competencia ya está pendiente.');

        if ($elegida->estado === EstadoProgresion::Desarrollada) {
            // Conserva su id_etapa: sigue elegida para la misma etapa.
            $elegida->estado = EstadoProgresion::EnProceso;
            $elegida->fecha = $this->fecha(null);
            $this->competenciasProtagonista->actualizar($elegida);
            return;
        }

        $this->competenciasProtagonista->eliminar($idProtagonista, $idCompetencia);
    }

    // =================================================================
    // Acciones
    // =================================================================

    /**
     * Tilda una acción como cumplida. La competencia tiene que estar elegida
     * (En proceso o Desarrollada). Si ya estaba tildada no hace nada, así el
     * guardado inmediato puede repetir el pedido sin error.
     *
     * Si con esta acción la competencia En proceso completa sus acciones
     * requeridas, sugiere darla por desarrollada.
     */
    public function marcarAccion(int $idUsuario, int $idProtagonista, int $idAccion, ?string $fecha = null): ResultadoProgresion
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Responsable);
        $accion = $this->accionDeRama($idAccion, $rama);
        $competencia = $this->competenciaDeRama($accion->idCompetencia, $rama);
        $fecha = $this->fecha($fecha);

        $elegida = $this->competenciasProtagonista->buscar($idProtagonista, $competencia->id)
            ?? throw new ReglaNegocioException('Primero hay que elegir la competencia (pasarla a En proceso).');

        if ($this->accionesCumplidas->buscar($idProtagonista, $idAccion) !== null) {
            return new ResultadoProgresion();
        }

        $this->accionesCumplidas->insertar(new AccionCumplida($idProtagonista, $idAccion, $fecha));

        $sugerencias = [];
        if (
            $elegida->estado === EstadoProgresion::EnProceso
            && !$accion->esOpcional
            && $this->accionesRequeridasFaltantes($idProtagonista, $competencia, $rama) === []
        ) {
            $sugerencias[] = new Aviso(
                TipoAviso::CompetenciaLista,
                'Cumplió todas las acciones requeridas. ¿Dar la competencia por desarrollada?',
            );
        }

        return new ResultadoProgresion($sugerencias);
    }

    /** Destilda una acción. Si no estaba tildada no hace nada. */
    public function desmarcarAccion(int $idUsuario, int $idProtagonista, int $idAccion): void
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Responsable);
        $this->accionDeRama($idAccion, $rama);
        $this->accionesCumplidas->eliminar($idProtagonista, $idAccion);
    }

    // =================================================================
    // Tareas de etapa
    // =================================================================

    /** Tilda una tarea de etapa como cumplida. Si ya estaba tildada no hace nada. */
    public function marcarTarea(int $idUsuario, int $idProtagonista, int $idTareaEtapa, ?string $fecha = null): void
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Responsable);
        $this->tareaDeRama($idTareaEtapa, $rama);
        $fecha = $this->fecha($fecha);

        if ($this->tareasCumplidas->buscar($idProtagonista, $idTareaEtapa) === null) {
            $this->tareasCumplidas->insertar(new TareaEtapaCumplida($idProtagonista, $idTareaEtapa, $fecha));
        }
    }

    /** Destilda una tarea de etapa. Si no estaba tildada no hace nada. */
    public function desmarcarTarea(int $idUsuario, int $idProtagonista, int $idTareaEtapa): void
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Responsable);
        $this->tareaDeRama($idTareaEtapa, $rama);
        $this->tareasCumplidas->eliminar($idProtagonista, $idTareaEtapa);
    }

    // =================================================================
    // Etapas
    // =================================================================

    /**
     * Registra que el protagonista completó la etapa.
     *
     *  - Bloquea (no se saltea): que la etapa anterior no esté lograda.
     *  - Avisa y se puede saltear: el cupo de competencias desarrolladas y las tareas.
     *  - Elemento: obligatorio en Caminantes; en las demás ramas tiene que ser null.
     *
     * La insignia no se toca: se registra aparte con otorgarInsignia().
     *
     * @throws RequisitosPendientesException si falta algo y no se pasó $confirmarIgual
     * @throws ReglaNegocioException
     */
    public function confirmarEtapa(
        int $idUsuario,
        int $idProtagonista,
        int $idEtapa,
        ?Elemento $elemento = null,
        bool $confirmarIgual = false,
        ?string $fecha = null,
    ): void {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Responsable);
        $etapa = $this->etapaDeRama($idEtapa, $rama);
        $fecha = $this->fecha($fecha);

        if ($this->etapasLogradas->buscar($idProtagonista, $idEtapa) !== null) {
            throw new ReglaNegocioException("La etapa {$etapa->titulo} ya está lograda.");
        }

        // Lo único que bloquea: la etapa anterior tiene que estar lograda.
        foreach ($this->etapas->listarPorRama($rama->id) as $otra) {
            if ($otra->numero === $etapa->numero - 1 && $this->etapasLogradas->buscar($idProtagonista, $otra->id) === null) {
                throw new ReglaNegocioException("Primero hay que confirmar la etapa {$otra->titulo}.");
            }
        }

        if ($rama->id === self::ID_RAMA_CAMINANTES && $elemento === null) {
            throw new ReglaNegocioException('En Caminantes hay que elegir el elemento de la etapa.');
        }
        if ($rama->id !== self::ID_RAMA_CAMINANTES && $elemento !== null) {
            throw new ReglaNegocioException('Solo en Caminantes se elige un elemento.');
        }

        $this->exigirConfirmacion($this->adeudado($idProtagonista, $etapa), $confirmarIgual);

        $this->etapasLogradas->insertar(new EtapaLograda($idProtagonista, $idEtapa, $elemento, $fecha));
    }

    /**
     * Quita una etapa lograda. Solo el Jefe, y solo la última lograda, para que
     * nunca quede una etapa lograda sin su anterior. La insignia otorgada no se toca.
     */
    public function quitarEtapa(int $idUsuario, int $idProtagonista, int $idEtapa): void
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Jefe);
        $etapa = $this->etapaDeRama($idEtapa, $rama);

        if ($this->etapasLogradas->buscar($idProtagonista, $idEtapa) === null) {
            throw new ReglaNegocioException("La etapa {$etapa->titulo} no está lograda.");
        }

        [$actual] = $this->situacionEtapas($idProtagonista, $rama);
        if ($actual?->id !== $idEtapa) {
            throw new ReglaNegocioException("Solo se puede quitar la última etapa lograda ({$actual?->titulo}).");
        }

        $this->etapasLogradas->eliminar($idProtagonista, $idEtapa);
    }

    // =================================================================
    // Insignias (registro manual, con la fecha de la entrega física)
    // =================================================================

    /**
     * Registra la entrega de una insignia. Tiene que ser de la rama de la
     * sección del protagonista; las insignias sin rama (id_rama NULL) sirven
     * para cualquiera.
     */
    public function otorgarInsignia(int $idUsuario, int $idProtagonista, int $idInsignia, ?string $fecha = null): void
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Responsable);
        $insignia = $this->insignias->buscarPorId($idInsignia)
            ?? throw new NoEncontradoException('La insignia no existe.');

        if ($insignia->idRama !== null && $insignia->idRama !== $rama->id) {
            throw new ReglaNegocioException("La insignia {$insignia->titulo} no es de {$rama->titulo}.");
        }

        $fecha = $this->fecha($fecha);

        if ($this->insigniasOtorgadas->buscar($idProtagonista, $idInsignia) !== null) {
            throw new ReglaNegocioException("Ya recibió la insignia {$insignia->titulo}.");
        }

        $this->insigniasOtorgadas->insertar(new InsigniaOtorgada($idProtagonista, $idInsignia, $fecha));
    }

    /** Quita una insignia otorgada. Solo el Jefe. */
    public function quitarInsignia(int $idUsuario, int $idProtagonista, int $idInsignia): void
    {
        $this->cargar($idUsuario, $idProtagonista, NivelRol::Jefe);

        if ($this->insigniasOtorgadas->buscar($idProtagonista, $idInsignia) === null) {
            throw new ReglaNegocioException('El protagonista no tiene esa insignia.');
        }

        $this->insigniasOtorgadas->eliminar($idProtagonista, $idInsignia);
    }

    // =================================================================
    // Especialidades (solo ramas con usa_especialidades; sin requisitos)
    // =================================================================

    /** Pendiente → En proceso → Desarrollada, igual que las competencias pero sin avisos. */
    public function avanzarEspecialidad(int $idUsuario, int $idProtagonista, int $idEspecialidad, ?string $fecha = null): void
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Responsable);
        $this->especialidadDeRama($idEspecialidad, $rama);
        $fecha = $this->fecha($fecha);
        $actual = $this->especialidadesProtagonista->buscar($idProtagonista, $idEspecialidad);

        if ($actual === null) {
            $this->especialidadesProtagonista->insertar(
                new EspecialidadProtagonista($idProtagonista, $idEspecialidad, EstadoProgresion::EnProceso, $fecha)
            );
            return;
        }

        if ($actual->estado === EstadoProgresion::Desarrollada) {
            throw new ReglaNegocioException('La especialidad ya está desarrollada.');
        }

        $actual->estado = EstadoProgresion::Desarrollada;
        $actual->fecha = $fecha;
        $this->especialidadesProtagonista->actualizar($actual);
    }

    /** Desarrollada → En proceso → Pendiente (borra la fila). */
    public function retrocederEspecialidad(int $idUsuario, int $idProtagonista, int $idEspecialidad): void
    {
        $rama = $this->cargar($idUsuario, $idProtagonista, NivelRol::Responsable);
        $this->especialidadDeRama($idEspecialidad, $rama);
        $actual = $this->especialidadesProtagonista->buscar($idProtagonista, $idEspecialidad)
            ?? throw new ReglaNegocioException('La especialidad ya está pendiente.');

        if ($actual->estado === EstadoProgresion::Desarrollada) {
            $actual->estado = EstadoProgresion::EnProceso;
            $actual->fecha = $this->fecha(null);
            $this->especialidadesProtagonista->actualizar($actual);
            return;
        }

        $this->especialidadesProtagonista->eliminar($idProtagonista, $idEspecialidad);
    }

    // =================================================================
    // Pasos internos de avanzarCompetencia
    // =================================================================

    /** Pendiente → En proceso. */
    private function elegirCompetencia(
        int $idProtagonista,
        Competencia $competencia,
        Rama $rama,
        bool $confirmarIgual,
        string $fecha,
    ): void {
        [, $enCurso, $todas] = $this->situacionEtapas($idProtagonista, $rama);
        $avisos = [];

        if ($enCurso === null) {
            // Ya logró todas las etapas: se suma a la última, por encima de su cupo.
            $destino = end($todas) ?: throw new \LogicException("La rama {$rama->id} no tiene etapas.");
            $avisos[] = new Aviso(
                TipoAviso::SinEtapaEnCurso,
                "Ya logró todas las etapas. La competencia se va a sumar a {$destino->titulo}, por encima de su cupo.",
            );
        } else {
            $destino = $enCurso;
            $elegidas = $this->contarEnEtapa($this->competenciasPorId($idProtagonista), $destino->id, false);

            if ($elegidas >= $destino->competenciasEtapa) {
                $avisos[] = new Aviso(
                    TipoAviso::CupoExcedido,
                    "La etapa {$destino->titulo} ya tiene {$elegidas} de {$destino->competenciasEtapa} competencias elegidas.",
                );
            }
        }

        $this->exigirConfirmacion($avisos, $confirmarIgual);

        $this->competenciasProtagonista->insertar(new CompetenciaProtagonista(
            idProtagonista: $idProtagonista,
            idCompetencia: $competencia->id,
            idEtapa: $destino->id,
            estado: EstadoProgresion::EnProceso,
            fecha: $fecha,
        ));
    }

    /** En proceso → Desarrollada. */
    private function desarrollarCompetencia(
        int $idProtagonista,
        Competencia $competencia,
        CompetenciaProtagonista $elegida,
        Rama $rama,
        bool $confirmarIgual,
        string $fecha,
    ): ResultadoProgresion {
        $faltantes = $this->accionesRequeridasFaltantes($idProtagonista, $competencia, $rama);

        if ($faltantes !== []) {
            $numeros = implode(', ', array_map(fn (Accion $a) => $a->numero, $faltantes));
            $this->exigirConfirmacion([new Aviso(
                TipoAviso::AccionesPendientes,
                'Le faltan ' . count($faltantes) . " acciones requeridas (n.º {$numeros}).",
            )], $confirmarIgual);
        }

        $elegida->estado = EstadoProgresion::Desarrollada;
        $elegida->fecha = $fecha;
        $this->competenciasProtagonista->actualizar($elegida);

        // Sugerencia: si con esta se completó el cupo de la etapa en curso.
        $sugerencias = [];
        [, $enCurso] = $this->situacionEtapas($idProtagonista, $rama);

        if ($enCurso !== null && $elegida->idEtapa === $enCurso->id) {
            $desarrolladas = $this->contarEnEtapa($this->competenciasPorId($idProtagonista), $enCurso->id, true);

            if ($desarrolladas >= $enCurso->competenciasEtapa) {
                $sugerencias[] = new Aviso(
                    TipoAviso::EtapaLista,
                    "Completó las {$enCurso->competenciasEtapa} competencias de la etapa {$enCurso->titulo}. ¿Confirmar la etapa?",
                );
            }
        }

        return new ResultadoProgresion($sugerencias);
    }

    // =================================================================
    // Cálculos
    // =================================================================

    /**
     * Etapa actual (última lograda en la rama), etapa en curso (la siguiente,
     * o la primera) y todas las etapas de la rama en orden.
     *
     * @return array{0: ?Etapa, 1: ?Etapa, 2: Etapa[]}
     */
    private function situacionEtapas(int $idProtagonista, Rama $rama): array
    {
        $todas = $this->etapas->listarPorRama($rama->id);
        $lograda = $this->etapasLogradas->buscarActual($idProtagonista, $rama->id);

        $actual = null;
        $enCurso = null;

        foreach ($todas as $etapa) {
            if ($lograda !== null && $etapa->id === $lograda->idEtapa) {
                $actual = $etapa;
            }
        }

        $numeroActual = $actual?->numero ?? 0;
        foreach ($todas as $etapa) {
            if ($etapa->numero > $numeroActual) {
                $enCurso = $etapa;
                break;
            }
        }

        return [$actual, $enCurso, $todas];
    }

    /**
     * Lo que le falta para confirmar la etapa (vacío = está lista):
     * competencias desarrolladas hasta el cupo y todas las tareas.
     *
     * @return Aviso[]
     */
    private function adeudado(int $idProtagonista, Etapa $etapa): array
    {
        $avisos = [];

        $desarrolladas = $this->contarEnEtapa($this->competenciasPorId($idProtagonista), $etapa->id, true);
        if ($desarrolladas < $etapa->competenciasEtapa) {
            $avisos[] = new Aviso(
                TipoAviso::CupoIncompleto,
                "Tiene {$desarrolladas} de {$etapa->competenciasEtapa} competencias desarrolladas en la etapa {$etapa->titulo}.",
            );
        }

        $cumplidas = [];
        foreach ($this->tareasCumplidas->listarPorProtagonistaYEtapa($idProtagonista, $etapa->id) as $tc) {
            $cumplidas[$tc->idTareaEtapa] = true;
        }

        foreach ($this->tareas->listarPorEtapa($etapa->id) as $tarea) {
            if (!isset($cumplidas[$tarea->id])) {
                $avisos[] = new Aviso(TipoAviso::TareaPendiente, "Falta la tarea {$tarea->numero}: {$tarea->descripcion}");
            }
        }

        return $avisos;
    }

    /**
     * Acciones requeridas de la competencia que todavía no cumplió.
     * Requerida = es_opcional = 0: en LyL son las no opcionales, y en Scouts y
     * Caminantes todas lo son (no tienen opcionales). Rovers no usa acciones.
     *
     * @return Accion[]
     */
    private function accionesRequeridasFaltantes(int $idProtagonista, Competencia $competencia, Rama $rama): array
    {
        if (!$rama->usaAcciones) {
            return [];
        }

        $cumplidas = [];
        foreach ($this->accionesCumplidas->listarPorProtagonistaYCompetencia($idProtagonista, $competencia->id) as $ac) {
            $cumplidas[$ac->idAccion] = true;
        }

        return array_values(array_filter(
            $this->acciones->listarPorCompetencia($competencia->id),
            fn (Accion $a) => !$a->esOpcional && !isset($cumplidas[$a->id]),
        ));
    }

    /**
     * Competencias elegidas por el protagonista, por id de competencia.
     *
     * @return array<int, CompetenciaProtagonista>
     */
    private function competenciasPorId(int $idProtagonista): array
    {
        $porId = [];
        foreach ($this->competenciasProtagonista->listarPorProtagonista($idProtagonista) as $cp) {
            $porId[$cp->idCompetencia] = $cp;
        }

        return $porId;
    }

    /**
     * Cuántas competencias elegidas tienen ese id_etapa. Como una etapa es de
     * una sola rama, no hace falta filtrar por rama.
     *
     * @param array<int, CompetenciaProtagonista> $elegidas
     * @param bool $soloDesarrolladas false = en proceso + desarrolladas
     */
    private function contarEnEtapa(array $elegidas, int $idEtapa, bool $soloDesarrolladas): int
    {
        return count(array_filter(
            $elegidas,
            fn (CompetenciaProtagonista $cp) => $cp->idEtapa === $idEtapa
                && (!$soloDesarrolladas || $cp->estado === EstadoProgresion::Desarrollada),
        ));
    }

    // =================================================================
    // Validaciones comunes
    // =================================================================

    /**
     * Busca el protagonista, controla el permiso en su sección y devuelve la
     * rama de esa sección (por JOIN de ids, nunca deduciéndola del nombre).
     */
    private function cargar(int $idUsuario, int $idProtagonista, NivelRol $minimo): Rama
    {
        return $this->cargarConNivel($idUsuario, $idProtagonista, $minimo)[0];
    }

    /**
     * Como cargar(), y además devuelve el rol del usuario en la sección.
     *
     * Sin ningún acceso a la sección: 404, igual que si el protagonista no
     * existiera (no se confirma que existe, como en documentos y eventos).
     * Con acceso pero sin el rol pedido: 403 (al protagonista ya lo puede ver).
     *
     * @return array{0: Rama, 1: NivelRol}
     */
    private function cargarConNivel(int $idUsuario, int $idProtagonista, NivelRol $minimo): array
    {
        $protagonista = $this->protagonistas->buscarPorId($idProtagonista)
            ?? throw new NoEncontradoException('El protagonista no existe.');

        $nivel = $this->permisos->nivelEnSeccion($idUsuario, $protagonista->idSeccion)
            ?? throw new NoEncontradoException('El protagonista no existe.');

        if ($nivel->value < $minimo->value) {
            throw new AccesoDenegadoException('Necesitás el rol ' . $minimo->etiqueta() . ' en esta sección para hacer esto.');
        }

        $seccion = $this->secciones->buscarPorId($protagonista->idSeccion)
            ?? throw new \LogicException("La sección {$protagonista->idSeccion} no existe.");

        $rama = $this->ramas->buscarPorId($seccion->idRama)
            ?? throw new \LogicException("La rama {$seccion->idRama} no existe.");

        return [$rama, $nivel];
    }

    /** Si hay avisos y el educador todavía no confirmó, corta sin guardar nada. */
    private function exigirConfirmacion(array $avisos, bool $confirmarIgual): void
    {
        if ($avisos !== [] && !$confirmarIgual) {
            throw new RequisitosPendientesException($avisos);
        }
    }

    private function competenciaDeRama(int $idCompetencia, Rama $rama): Competencia
    {
        $competencia = $this->competencias->buscarPorId($idCompetencia)
            ?? throw new NoEncontradoException('La competencia no existe.');

        if ($competencia->idRama !== $rama->id) {
            throw new ReglaNegocioException("La competencia no es de {$rama->titulo}.");
        }

        return $competencia;
    }

    private function accionDeRama(int $idAccion, Rama $rama): Accion
    {
        if (!$rama->usaAcciones) {
            throw new ReglaNegocioException("{$rama->titulo} no usa acciones.");
        }

        $accion = $this->acciones->buscarPorId($idAccion)
            ?? throw new NoEncontradoException('La acción no existe.');
        $this->competenciaDeRama($accion->idCompetencia, $rama);

        return $accion;
    }

    private function etapaDeRama(int $idEtapa, Rama $rama): Etapa
    {
        $etapa = $this->etapas->buscarPorId($idEtapa)
            ?? throw new NoEncontradoException('La etapa no existe.');

        if ($etapa->idRama !== $rama->id) {
            throw new ReglaNegocioException("La etapa no es de {$rama->titulo}.");
        }

        return $etapa;
    }

    private function tareaDeRama(int $idTareaEtapa, Rama $rama): void
    {
        $tarea = $this->tareas->buscarPorId($idTareaEtapa)
            ?? throw new NoEncontradoException('La tarea no existe.');
        $this->etapaDeRama($tarea->idEtapa, $rama);
    }

    private function especialidadDeRama(int $idEspecialidad, Rama $rama): Especialidad
    {
        if (!$rama->usaEspecialidades) {
            throw new ReglaNegocioException("{$rama->titulo} no usa especialidades.");
        }

        return $this->especialidades->buscarPorId($idEspecialidad)
            ?? throw new NoEncontradoException('La especialidad no existe.');
    }

    /**
     * La fecha a guardar: la de hoy si no se pasa ninguna. Si se pasa, tiene
     * que ser una fecha real en formato 'AAAA-MM-DD' y no futura (el educador
     * puede cargar una anterior, ej. una etapa lograda en el campamento del
     * mes pasado, pero no algo que todavía no pasó). "Hoy" usa ZONA_HORARIA.
     */
    private function fecha(?string $fecha): string
    {
        $hoy = date('Y-m-d');
        if ($fecha === null) {
            return $hoy;
        }

        $fecha = Validacion::fecha($fecha);
        if ($fecha > $hoy) {
            throw new ReglaNegocioException('La fecha no puede ser posterior a hoy.');
        }
        return $fecha;
    }
}
