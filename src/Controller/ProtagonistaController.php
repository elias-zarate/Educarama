<?php
// src/Controller/ProtagonistaController.php

declare(strict_types=1);

namespace Educarama\Controller;

use Educarama\Helper\Flash;
use Educarama\Model\Persona;
use Educarama\Model\Protagonista;
use Educarama\Service\DocumentoService;
use Educarama\Service\Excepcion\AccesoDenegadoException;
use Educarama\Service\Excepcion\ReglaNegocioException;
use Educarama\Service\FamiliarService;
use Educarama\Service\PatrullaService;
use Educarama\Service\ProgresionService;
use Educarama\Service\ProtagonistaService;
use Educarama\Service\SeccionService;
use Educarama\Service\UsuarioService;

final class ProtagonistaController extends Controlador
{
    public function __construct(
        UsuarioService $usuarioService,
        private readonly ProtagonistaService $protagonistas,
        private readonly SeccionService $secciones,
        private readonly PatrullaService $patrullas,
        private readonly ProgresionService $progresion,
        private readonly FamiliarService $familiares,
        private readonly DocumentoService $documentos
    ) {
        parent::__construct($usuarioService);
    }

    public function paginaNomina(): Respuesta
    {
        return Respuesta::pagina([
            'nomina' => $this->protagonistas->nomina($this->usuario()->id),
        ]);
    }

    public function paginaListado(array $get): Respuesta
    {
        $idUsuario = $this->usuario()->id;
        $idSeccion = self::entero($get, 'id_seccion');

        $seccion = $this->secciones->encabezado($idUsuario, $idSeccion);

        return Respuesta::pagina([
            'seccion'       => $seccion,
            'protagonistas' => $this->protagonistas->listarPorSeccion($idUsuario, $idSeccion),
            'patrullas'     => $this->patrullas->listarPorSeccion($idUsuario, $idSeccion),
            'nomina'        => $this->protagonistas->nominaPorSeccion($idUsuario, $idSeccion),
        ]);
    }

    public function paginaPerfil(array $get): Respuesta
    {
        $idUsuario = $this->usuario()->id;
        $idProtagonista = self::entero($get, 'id');

        $ficha = $this->protagonistas->buscarFicha($idUsuario, $idProtagonista);

        return Respuesta::pagina([
            'ficha'            => $ficha,
            'seccion'          => $this->secciones->encabezado($idUsuario, $ficha->protagonista->idSeccion),
            'resumen'          => $this->progresion->resumen($idUsuario, $idProtagonista),
            'insignias'        => $this->progresion->insigniasParaPerfil($idUsuario, $idProtagonista),
            'familiares'       => $this->familiares->listarPorProtagonista($idUsuario, $idProtagonista),
            'documentos'       => $this->documentos->listarPorProtagonista($idUsuario, $idProtagonista),
            'seccionesDestino' => $this->protagonistas->seccionesDestino($idUsuario, $idProtagonista),
        ]);
    }

    public function paginaFormulario(array $get): Respuesta
    {
        $idUsuario = $this->usuario()->id;
        $idProtagonista = self::enteroOpcional($get, 'id');

        $ficha = $idProtagonista === null ? null : $this->protagonistas->buscarFicha($idUsuario, $idProtagonista);
        $idSeccion = $ficha?->protagonista->idSeccion ?? self::entero($get, 'id_seccion');
        $seccion = $this->secciones->encabezado($idUsuario, $idSeccion);

        if (!$seccion->puedeEditar()) {
            throw new AccesoDenegadoException('Necesitás el rol Responsable en esta sección para hacer esto.');
        }

        return Respuesta::pagina([
            'seccion'   => $seccion,
            'patrullas' => $this->patrullas->listarPorSeccion($idUsuario, $idSeccion),
            'ficha'     => $ficha,
        ]);
    }

    public function guardar(array $post): Respuesta
    {
        $idUsuario = $this->usuario()->id;
        $id = self::enteroOpcional($post, 'id');

        $idSeccion = $id === null
            ? self::entero($post, 'id_seccion')
            : $this->protagonistas->buscarFicha($idUsuario, $id)->protagonista->idSeccion;

        $persona = new Persona(
            id: null,
            dni: self::dni($post),
            nombre: self::texto($post, 'nombre'),
            apellido: self::texto($post, 'apellido'),
            fechaNacimiento: self::fechaOpcional($post, 'fecha_nacimiento'),
        );
        $protagonista = new Protagonista(
            id: $id,
            idPersona: 0,
            idSeccion: $idSeccion,
            idPatrulla: self::enteroOpcional($post, 'id_patrulla'),
            promesa: self::booleano($post, 'promesa'),
            anioIngreso: self::anio($post, 'anio_ingreso'),
        );

        if ($id === null) {
            $id = $this->protagonistas->alta($idUsuario, $persona, $protagonista);
            Flash::guardar(Flash::EXITO, 'Protagonista dado de alta.');
        } else {
            $this->protagonistas->actualizar($idUsuario, $persona, $protagonista);
            Flash::guardar(Flash::EXITO, 'Cambios guardados.');
        }

        return Respuesta::exito(['id' => $id], redireccion: url('views/pages/perfil_protagonista.php?id=' . $id));
    }

    public function cambiarSeccion(array $post): Respuesta
    {
        $idUsuario = $this->usuario()->id;
        $idProtagonista = self::entero($post, 'id_protagonista');
        $idSeccion = self::entero($post, 'id_seccion');

        $this->protagonistas->cambiarSeccion($idUsuario, $idProtagonista, $idSeccion);

        $destino = $this->secciones->encabezado($idUsuario, $idSeccion);
        $equipo = $destino->pequenioEquipo();
        Flash::guardar(Flash::EXITO, "Pasó a {$destino->seccion->nombre}." . ($equipo === null ? '' : ' Quedó sin ' . mb_strtolower($equipo) . '.'));

        return Respuesta::exito(redireccion: url('views/pages/perfil_protagonista.php?id=' . $idProtagonista));
    }

    public function eliminar(array $post): Respuesta
    {
        $idUsuario = $this->usuario()->id;
        $idProtagonista = self::entero($post, 'id_protagonista');

        $ficha = $this->protagonistas->buscarFicha($idUsuario, $idProtagonista);
        $this->protagonistas->eliminar($idUsuario, $idProtagonista);

        Flash::guardar(Flash::EXITO, "Se eliminó a {$ficha->persona->nombre} {$ficha->persona->apellido}.");
        return Respuesta::exito(redireccion: url('views/pages/protagonistas.php?id_seccion=' . $ficha->protagonista->idSeccion));
    }

    private static function anio(array $post, string $clave): ?int
    {
        $texto = trim(self::texto($post, $clave));
        if ($texto === '') {
            return null;
        }
        if (!ctype_digit($texto) || strlen($texto) > 4) {
            throw new ReglaNegocioException('El año de ingreso no es válido.');
        }
        return (int) $texto;
    }
}