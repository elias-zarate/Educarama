<?php
// views/pages/gestionar_competencias.php
//
// Las competencias de un protagonista (reglas del proyecto, "Pantalla de competencias"):
//  - Una pestaña por área, con TODAS las competencias de la rama (sin fila = pendiente).
//  - El botón de cada una solo avanza: Pendiente → En proceso → Desarrollada.
//  - ⋮ → "Corregir" activa los botones para volver atrás (Responsable en adelante).
//  - Acciones: casillas dentro de cada competencia, en un desplegable (no en Rovers).
//  - Contadores arriba (etapa en curso y total) y en cada pestaña; filtro por estado.
//  - Cada clic se guarda en el momento (public/js/competencias.js); si hay un
//    aviso (cupo, acciones sin cumplir), primero se pide confirmación.

declare(strict_types=1);

$app = require_once __DIR__ . '/../../src/main.php';

use Educarama\Controller\Pagina;
use Educarama\Controller\ProgresionController;
use Educarama\Helper\Vista;
use Educarama\Model\Enum\EstadoProgresion;

$datos = Pagina::cargar(fn () => $app->obtener(ProgresionController::class)->paginaCompetencias($_GET));
$ficha = $datos['ficha'];
$pantalla = $datos['pantalla'];
$resumen = $pantalla->resumen;
$rama = $resumen->rama;
$id = (int) $ficha->protagonista->id;
$nombre = $ficha->persona->nombre . ' ' . $ficha->persona->apellido;
$termino = $pantalla->terminos['competencias'] ?? 'Competencias';
$editable = $pantalla->puedeEditar;
$nombresEtapas = $pantalla->nombresEtapas;

/** El botón que solo avanza, según el estado. */
$textoAvanzar = static fn (?EstadoProgresion $estado): ?string => match ($estado) {
    null                         => 'Elegir',
    EstadoProgresion::EnProceso  => 'Desarrollada',
    EstadoProgresion::Desarrollada => null,
};

$paginaActual = 'competencias';
$idProtagonista = $id;

$tituloPagina = 'Competencias de ' . $nombre;
$claseRama = Vista::claseRama($rama->id);
$scriptsPagina = ['public/js/competencias.js'];
require __DIR__ . '/../includes/cabecera.php';
?>
<section class="barra-superior-inst mb-4 d-flex justify-content-between align-items-center flex-wrap gap-3">
    <div>
        <h1 class="fw-bold mb-1"><?= e($nombre) ?></h1>
        <p class="mb-0"><?= e($termino) ?> · <?= e($rama->titulo) ?></p>
    </div>
</section>

<?php require __DIR__ . '/../includes/nav_protagonista.php'; ?>

<div id="competencias"
     data-id-protagonista="<?= $id ?>"
     data-url-etapas="<?= e(url('views/pages/progresion.php?id=' . $id)) ?>"
     data-etapas='<?= e(json_encode($nombresEtapas, JSON_UNESCAPED_UNICODE)) ?>'
     data-ultima-etapa="<?= (int) array_key_last($nombresEtapas) ?>">

    <!-- Contadores -->
    <div class="row g-3 mb-3">
        <div class="col-sm-6">
            <div class="contador-inst">
                <p class="dato-etiqueta">Etapa en curso<?php if ($resumen->etapaEnCurso !== null): ?> · <span data-etapa-en-curso><?= e($resumen->etapaEnCurso->titulo) ?></span><?php endif; ?></p>
                <?php if ($resumen->etapaEnCurso !== null): ?>
                    <p class="numero mb-0"><span data-contador="enCurso"><?= $resumen->enCursoDesarrolladas ?></span>/<span data-contador="cupo"><?= $resumen->cupoEnCurso() ?></span></p>
                    <p class="small text-muted mb-0">desarrolladas · <span data-contador="elegidas"><?= $resumen->enCursoElegidas ?></span> elegidas</p>
                <?php else: ?>
                    <p class="numero mb-0">—</p>
                    <p class="small text-muted mb-0">Logró todas las etapas de la rama.</p>
                <?php endif; ?>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="contador-inst">
                <p class="dato-etiqueta">Total de la rama</p>
                <p class="numero mb-1"><span data-contador="total"><?= $resumen->desarrolladasTotal ?></span>/<?= $resumen->competenciasTotal ?></p>
                <?php $porcentaje = $resumen->competenciasTotal > 0 ? (int) round(100 * $resumen->desarrolladasTotal / $resumen->competenciasTotal) : 0; ?>
                <div class="barra-progreso"><div class="progreso-actual" data-barra-total data-de="<?= $resumen->competenciasTotal ?>" style="width: <?= $porcentaje ?>%"></div></div>
            </div>
        </div>
    </div>

    <!-- Avisos: insignia pendiente y etapa lista (los repinta el JS después de cada clic) -->
    <div id="avisosResumen" class="mb-3" aria-live="polite">
        <?php if ($resumen->insigniaPendiente !== null): ?>
            <div class="alert alert-warning py-2 small mb-2"><i class="bi bi-award me-1" aria-hidden="true"></i>Tiene la etapa <?= e($resumen->etapaEnCurso?->titulo ?? '') ?> en curso y todavía no recibió su insignia.</div>
        <?php endif; ?>
        <?php if ($resumen->etapaEnCurso !== null && $resumen->etapaLista()): ?>
            <div class="alert alert-success py-2 small mb-2"><i class="bi bi-check2-circle me-1" aria-hidden="true"></i>Cumplió todo para confirmar la etapa <?= e($resumen->etapaEnCurso->titulo) ?>. <a href="<?= e(url('views/pages/progresion.php?id=' . $id)) ?>">Ir a confirmarla</a></div>
        <?php endif; ?>
    </div>

    <!-- Filtro y ⋮ Corregir -->
    <div class="d-flex justify-content-between align-items-center flex-wrap gap-2 mb-3">
        <div class="btn-group btn-group-sm flex-wrap" role="group" aria-label="Filtrar por estado">
            <?php foreach (['todas' => 'Todas', 'pendiente' => 'Pendientes', 'en_proceso' => 'En proceso', 'desarrollada' => 'Desarrolladas'] as $valor => $texto): ?>
                <input type="radio" class="btn-check" name="filtro" id="filtro-<?= $valor ?>" value="<?= $valor ?>" autocomplete="off"<?= $valor === 'todas' ? ' checked' : '' ?>>
                <label class="btn btn-outline-secondary" for="filtro-<?= $valor ?>"><?= $texto ?></label>
            <?php endforeach; ?>
        </div>
        <?php if ($editable): ?>
            <div class="dropdown">
                <button class="btn btn-light btn-sm" type="button" data-bs-toggle="dropdown" aria-expanded="false" aria-label="Más opciones">
                    <i class="bi bi-three-dots-vertical" aria-hidden="true"></i>
                </button>
                <ul class="dropdown-menu dropdown-menu-end">
                    <li><button class="dropdown-item" type="button" id="botonCorregir"><i class="bi bi-arrow-counterclockwise me-2" aria-hidden="true"></i><span>Corregir</span></button></li>
                </ul>
            </div>
        <?php endif; ?>
    </div>
    <div class="alert alert-info py-2 small aviso-corregir">
        <i class="bi bi-arrow-counterclockwise me-1" aria-hidden="true"></i>
        Modo Corregir: cada competencia tiene un botón para volver un paso atrás. Volver a Pendiente conserva sus acciones cumplidas.
        <button type="button" class="btn btn-link btn-sm p-0 ms-1 align-baseline" id="salirCorregir">Terminar</button>
    </div>

    <!-- Pestañas por área -->
    <ul class="nav nav-tabs tabs-inst mb-3" role="tablist">
        <?php foreach ($pantalla->areas as $i => $area): ?>
            <?php $idArea = $area->contador->idArea; ?>
            <li class="nav-item" role="presentation">
                <button class="nav-link<?= $i === 0 ? ' active' : '' ?>" id="pestania-area-<?= $idArea ?>" data-bs-toggle="tab" data-bs-target="#area-<?= $idArea ?>"
                        type="button" role="tab" aria-controls="area-<?= $idArea ?>" aria-selected="<?= $i === 0 ? 'true' : 'false' ?>">
                    <?php $iconoArea = Vista::iconoAreaOficial($idArea); ?>
                    <?php if ($iconoArea !== null): ?>
                        <img class="icono-oficial me-1" src="<?= e($iconoArea) ?>" alt="">
                    <?php else: ?>
                        <i class="bi <?= e(Vista::iconoArea($idArea)) ?> me-1" aria-hidden="true"></i>
                    <?php endif; ?>
                    <?= e($area->contador->titulo) ?>
                    <span class="badge text-bg-secondary ms-1"><span data-area-desarrolladas="<?= $idArea ?>"><?= $area->contador->desarrolladas ?></span>/<?= $area->contador->total ?></span>
                </button>
            </li>
        <?php endforeach; ?>
    </ul>

    <div class="tab-content">
        <?php foreach ($pantalla->areas as $i => $area): ?>
            <?php $idArea = $area->contador->idArea; ?>
            <div class="tab-pane fade<?= $i === 0 ? ' show active' : '' ?>" id="area-<?= $idArea ?>" role="tabpanel" aria-labelledby="pestania-area-<?= $idArea ?>" tabindex="0">
                <h2 class="h5 texto-violeta fw-bold mb-3"><?= e($area->contador->titulo) ?></h2>
                <p class="text-muted small sin-resultados d-none">No hay competencias con ese estado en esta área.</p>

                <div class="list-group">
                    <?php foreach ($area->competencias as $c): ?>
                        <?php
                        $idComp = (int) $c->competencia->id;
                        $estado = $c->estado;
                        $cumplidas = count(array_filter($c->acciones, fn ($a) => $a->cumplida));
                        $avanzar = $textoAvanzar($estado);
                        ?>
                        <div class="list-group-item competencia" data-id="<?= $idComp ?>" data-area="<?= $idArea ?>" data-estado="<?= e($estado?->value ?? 'pendiente') ?>">
                            <div class="d-flex gap-3 align-items-start flex-wrap flex-md-nowrap">
                                <span class="badge text-bg-light border codigo"><?= (int) $c->competencia->numero ?></span>
                                <div class="flex-grow-1">
                                    <p class="mb-1"><?= e($c->competencia->descripcion) ?></p>
                                    <p class="small mb-0">
                                        <span class="badge text-bg-<?= e(Vista::colorEstadoProgresion($estado)) ?>" data-badge-estado><?= e(Vista::estado($estado)) ?></span>
                                        <span class="text-muted ms-1" data-etiqueta-etapa><?= $c->idEtapa !== null ? 'Elegida en ' . e($nombresEtapas[$c->idEtapa] ?? '') : '' ?></span>
                                    </p>

                                    <?php if ($c->acciones !== []): ?>
                                        <button type="button" class="btn btn-link btn-sm px-0 mt-1" data-bs-toggle="collapse" data-bs-target="#acciones-<?= $idComp ?>" aria-expanded="false" aria-controls="acciones-<?= $idComp ?>">
                                            Acciones (<span data-acciones-cumplidas><?= $cumplidas ?></span>/<?= count($c->acciones) ?>)
                                        </button>
                                        <div class="collapse lista-acciones" id="acciones-<?= $idComp ?>">
                                            <?php foreach ($c->acciones as $a): ?>
                                                <?php $idAccion = (int) $a->accion->id; ?>
                                                <div class="form-check">
                                                    <input class="form-check-input casilla-accion" type="checkbox" id="accion-<?= $idAccion ?>" data-id-accion="<?= $idAccion ?>"
                                                           <?= $a->cumplida ? 'checked' : '' ?> <?= !$editable || $estado === null ? 'disabled' : '' ?>>
                                                    <label class="form-check-label small" for="accion-<?= $idAccion ?>">
                                                        <?= e($a->accion->descripcion) ?>
                                                        <?php if ($a->accion->esOpcional): /* solo existe en Lobatos y Lobeznas */ ?><span class="text-muted">(opcional)</span><?php endif; ?>
                                                    </label>
                                                </div>
                                            <?php endforeach; ?>
                                            <?php if ($editable): ?>
                                                <p class="small text-muted mb-1 aviso-pendiente<?= $estado === null ? '' : ' d-none' ?>">Para tildar acciones, primero hay que elegir la competencia.</p>
                                            <?php endif; ?>
                                        </div>
                                    <?php endif; ?>
                                </div>

                                <?php if ($editable): ?>
                                    <div class="d-flex flex-column gap-2 align-items-stretch">
                                        <button type="button" class="btn btn-sm boton-primario btn-avanzar<?= $avanzar === null ? ' d-none' : '' ?>">
                                            <i class="bi bi-arrow-right-circle me-1" aria-hidden="true"></i><span><?= e($avanzar ?? '') ?></span>
                                        </button>
                                        <button type="button" class="btn btn-sm btn-outline-secondary btn-retroceder<?= $estado === null ? ' invisible' : '' ?>">
                                            <i class="bi bi-arrow-counterclockwise me-1" aria-hidden="true"></i>Volver atrás
                                        </button>
                                    </div>
                                <?php endif; ?>
                            </div>
                        </div>
                    <?php endforeach; ?>
                </div>
            </div>
        <?php endforeach; ?>
    </div>
</div>
<?php require __DIR__ . '/../includes/pie.php'; ?>
