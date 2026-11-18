<?php
// views/pages/progresion.php
//
// "Etapas e insignias" de un protagonista (el submenú propio que piden las reglas del proyecto):
//  - Etapas de la rama: las logradas (fecha y, en Caminantes, elemento; la
//    última se puede quitar, solo Jefe), la en curso con sus tareas tildables y
//    "Confirmar etapa" (con fecha y elemento), y las que vienen.
//  - Insignias de la rama y generales: entregadas con su fecha, o para registrar la entrega.
//  - Especialidades (solo LyL y Scouts): las suyas arriba y el resto para empezar.
// Cada acción se guarda por fetch y recarga la página (data-recargar).

declare(strict_types=1);

$app = require_once __DIR__ . '/../../src/main.php';

use Educarama\Controller\Pagina;
use Educarama\Controller\ProgresionController;
use Educarama\Helper\Csrf;
use Educarama\Helper\Vista;
use Educarama\Model\Enum\Elemento;
use Educarama\Model\Enum\EstadoProgresion;

$datos = Pagina::cargar(fn () => $app->obtener(ProgresionController::class)->paginaEtapas($_GET));
$ficha = $datos['ficha'];
$pantalla = $datos['pantalla'];
$resumen = $pantalla->resumen;
$id = (int) $ficha->protagonista->id;
$nombre = $ficha->persona->nombre . ' ' . $ficha->persona->apellido;
$hoy = date('Y-m-d');
$editable = $pantalla->puedeEditar;

/** data-datos de un botón: siempre con el protagonista. */
$datosBoton = static fn (array $extra): string => e(json_encode(['id_protagonista' => $id] + $extra));

$paginaActual = 'etapas';
$idProtagonista = $id;

$tituloPagina = 'Etapas e insignias de ' . $nombre;
$claseRama = Vista::claseRama($resumen->rama->id);
require __DIR__ . '/../includes/cabecera.php';
?>
<section class="barra-superior-inst mb-4 d-flex justify-content-between align-items-center flex-wrap gap-3">
    <div>
        <h1 class="fw-bold mb-1"><?= e($nombre) ?></h1>
        <p class="mb-0">Etapas e insignias · <?= e($resumen->rama->titulo) ?></p>
    </div>
</section>

<?php require __DIR__ . '/../includes/nav_protagonista.php'; ?>

<!-- ================================================================ PESTAÑAS -->
<ul class="nav nav-tabs mb-4" role="tablist">
    <li class="nav-item" role="presentation">
        <button class="nav-link active" id="tab-etapas" data-bs-toggle="tab" data-bs-target="#panel-etapas" type="button" role="tab" aria-controls="panel-etapas" aria-selected="true">
            <i class="bi bi-layers me-1" aria-hidden="true"></i>Etapas
        </button>
    </li>
    <li class="nav-item" role="presentation">
        <button class="nav-link" id="tab-insignias" data-bs-toggle="tab" data-bs-target="#panel-insignias" type="button" role="tab" aria-controls="panel-insignias" aria-selected="false">
            <i class="bi bi-award me-1" aria-hidden="true"></i>Insignias
            <?php if ($resumen->insigniaPendiente !== null): ?>
                <span class="badge text-bg-warning ms-1">1 pendiente</span>
            <?php endif; ?>
        </button>
    </li>
</ul>

<div class="tab-content">
<div class="tab-pane fade show active" id="panel-etapas" role="tabpanel" aria-labelledby="tab-etapas">
<div class="d-grid gap-3 mb-4">
    <?php foreach ($pantalla->etapas as $e): ?>
        <?php
        $etapa = $e->etapa;
        $idEtapa = (int) $etapa->id;
        $clase = $e->lograda !== null ? 'lograda' : ($e->enCurso ? 'en-curso' : '');
        ?>
        <article class="tarjeta-blanca-borde etapa-card p-4 <?= $clase ?>">
            <div class="d-flex justify-content-between align-items-start flex-wrap gap-2 mb-2">
                <div>
                    <h3 class="h5 fw-bold texto-violeta mb-1">Etapa <?= (int) $etapa->numero ?> · <?= e($etapa->titulo) ?></h3>
                    <?php if ($e->lograda !== null): ?>
                        <span class="badge text-bg-success"><i class="bi bi-check-lg me-1" aria-hidden="true"></i>Lograda el <?= e(Vista::fecha($e->lograda->fecha)) ?></span>
                        <?php if ($e->lograda->elemento !== null): ?>
                            <span class="badge text-bg-info">Elemento: <?= e($e->lograda->elemento->etiqueta()) ?></span>
                        <?php endif; ?>
                    <?php elseif ($e->enCurso): ?>
                        <span class="badge text-bg-warning">En curso</span>
                        <span class="small text-muted ms-1"><?= $resumen->enCursoDesarrolladas ?>/<?= (int) $etapa->competenciasEtapa ?> competencias desarrolladas</span>
                    <?php else: ?>
                        <span class="badge text-bg-light border">Por venir</span>
                    <?php endif; ?>
                </div>

                <?php if ($e->esLaActual && $pantalla->puedeQuitar): ?>
                    <button type="button" class="btn btn-outline-danger btn-sm" data-accion="progresion.quitarEtapa"
                            data-datos="<?= $datosBoton(['id_etapa' => $idEtapa]) ?>"
                            data-confirmar="¿Quitar la etapa <?= e($etapa->titulo) ?>?" data-texto="Deja de figurar como lograda. La insignia entregada no se toca."
                            data-boton="Sí, quitarla" data-peligro data-recargar>
                        <i class="bi bi-x-circle me-1" aria-hidden="true"></i>Quitar
                    </button>
                <?php endif; ?>
            </div>

            <?php if ($etapa->habilitaciones !== null): ?>
                <p class="small text-muted mb-2"><i class="bi bi-unlock me-1" aria-hidden="true"></i><?= e($etapa->habilitaciones) ?></p>
            <?php endif; ?>

            <?php if ($e->tareas !== []): ?>
                <p class="dato-etiqueta mt-2 mb-1">Tareas de la etapa</p>
                <?php foreach ($e->tareas as $t): ?>
                    <?php
                    $idTarea = (int) $t->tarea->id;
                    $tildable = $e->enCurso && $editable;
                    ?>
                    <div class="form-check">
                        <input class="form-check-input" type="checkbox" id="tarea-<?= $idTarea ?>" <?= $t->cumplida() ? 'checked' : '' ?> <?= $tildable ? '' : 'disabled' ?>
                            <?php if ($tildable): ?>
                                data-accion="<?= $t->cumplida() ? 'progresion.desmarcarTarea' : 'progresion.marcarTarea' ?>"
                                data-datos="<?= $datosBoton(['id_tarea_etapa' => $idTarea]) ?>" data-recargar
                            <?php endif; ?>>
                        <label class="form-check-label small" for="tarea-<?= $idTarea ?>">
                            <?= e($t->tarea->descripcion) ?>
                            <?php if ($t->cumplida()): ?><span class="text-muted">· <?= e(Vista::fecha($t->fechaCumplida)) ?></span><?php endif; ?>
                        </label>
                    </div>
                <?php endforeach; ?>
            <?php endif; ?>

            <?php if ($e->enCurso): ?>
                <?php if ($resumen->adeudado !== []): ?>
                    <div class="alert alert-light border small mt-3 mb-0">
                        <p class="fw-bold mb-1">Para confirmar la etapa le falta:</p>
                        <ul class="mb-0">
                            <?php foreach ($resumen->adeudado as $aviso): ?><li><?= e($aviso->mensaje) ?></li><?php endforeach; ?>
                        </ul>
                        <p class="mb-0 mt-1 text-muted">Se puede confirmar igual: se pide confirmación mostrando lo que falta.</p>
                    </div>
                <?php endif; ?>

                <?php if ($editable): ?>
                    <form class="row g-2 align-items-end mt-3" data-accion="progresion.confirmarEtapa" data-recargar>
                        <?= Csrf::campo() ?>
                        <input type="hidden" name="id_protagonista" value="<?= $id ?>">
                        <input type="hidden" name="id_etapa" value="<?= $idEtapa ?>">
                        <div class="col-sm-auto">
                            <label class="form-label small" for="fecha-etapa">Fecha</label>
                            <input class="form-control" type="date" id="fecha-etapa" name="fecha" value="<?= $hoy ?>" max="<?= $hoy ?>">
                        </div>
                        <?php if ($pantalla->pideElemento): ?>
                            <div class="col-sm-auto">
                                <label class="form-label small" for="elemento">Elemento</label>
                                <select class="form-select" id="elemento" name="elemento" required>
                                    <option value="">Elegí…</option>
                                    <?php foreach (Elemento::cases() as $el): ?>
                                        <option value="<?= e($el->value) ?>"><?= e($el->etiqueta()) ?></option>
                                    <?php endforeach; ?>
                                </select>
                            </div>
                        <?php endif; ?>
                        <div class="col-sm-auto">
                            <button type="submit" class="boton-primario px-3 py-2 fw-bold"><i class="bi bi-check2-circle me-1" aria-hidden="true"></i>Confirmar etapa</button>
                        </div>
                    </form>
                <?php endif; ?>
            <?php endif; ?>
        </article>
    <?php endforeach; ?>
    <?php if ($resumen->etapaEnCurso === null): ?>
        <div class="alert alert-success mb-0"><i class="bi bi-trophy me-1" aria-hidden="true"></i>Logró todas las etapas de la rama.</div>
    <?php endif; ?>
</div>
</div><!-- /panel-etapas -->

<div class="tab-pane fade" id="panel-insignias" role="tabpanel" aria-labelledby="tab-insignias">
<p class="small text-muted mb-3">Se registra la entrega física, con su fecha. La de una etapa se entrega cuando la etapa empieza.</p>
<?php
// Elemento elegido en cada etapa lograda (solo Caminantes): decide la imagen de su insignia.
$elementoPorEtapa = [];
foreach ($pantalla->etapas as $e) {
    if ($e->lograda?->elemento !== null) {
        $elementoPorEtapa[(int) $e->etapa->id] = $e->lograda->elemento;
    }
}
?>
<div class="row g-3 mb-4">
    <?php foreach ($pantalla->insignias as $i): ?>
        <?php
        $idInsignia = (int) $i->insignia->id;
        $pendiente = $resumen->insigniaPendiente?->id === $idInsignia;
        // Imagen guardada en la fila; si no hay, la de Caminantes según el elemento elegido.
        $idEtapaInsignia = $i->insignia->idEtapa;
        $imagenInsignia = $i->insignia->imagen !== null
            ? Vista::imagenInsignia($i->insignia->imagen)
            : ($idEtapaInsignia !== null
                ? Vista::imagenInsigniaCaminantes($elementoPorEtapa[(int) $idEtapaInsignia] ?? null)
                : null);
        ?>
        <div class="col-md-6">
            <div class="tarjeta-blanca-borde p-3 h-100 d-flex gap-3 align-items-start<?= $pendiente ? ' border-warning border-2' : '' ?>">
                <div class="insignia-icono<?= $i->otorgada() ? '' : ' pendiente' ?><?= $imagenInsignia !== null ? ' con-imagen' : '' ?>" aria-hidden="true">
                    <?php if ($imagenInsignia !== null): ?>
                        <img src="<?= e($imagenInsignia) ?>" alt="">
                    <?php else: ?>
                        <i class="bi bi-award"></i>
                    <?php endif; ?>
                </div>
                <div class="flex-grow-1">
                    <p class="fw-bold mb-1"><?= e($i->insignia->titulo) ?></p>
                    <?php if ($i->otorgada()): ?>
                        <p class="small mb-2"><span class="badge text-bg-success">Entregada el <?= e(Vista::fecha($i->fechaOtorgada)) ?></span></p>
                        <?php if ($pantalla->puedeQuitar): ?>
                            <button type="button" class="btn btn-outline-danger btn-sm" data-accion="progresion.quitarInsignia"
                                    data-datos="<?= $datosBoton(['id_insignia' => $idInsignia]) ?>"
                                    data-confirmar="¿Quitar la insignia <?= e($i->insignia->titulo) ?>?" data-boton="Sí, quitarla" data-peligro data-recargar>
                                Quitar
                            </button>
                        <?php endif; ?>
                    <?php else: ?>
                        <p class="small mb-2">
                            <?= $pendiente ? '<span class="badge text-bg-warning">Le corresponde: tiene la etapa en curso</span>' : '<span class="text-muted">Sin entregar</span>' ?>
                        </p>
                        <?php if ($editable): ?>
                            <form class="d-flex flex-wrap gap-2 align-items-center" data-accion="progresion.otorgarInsignia" data-recargar>
                                <?= Csrf::campo() ?>
                                <input type="hidden" name="id_protagonista" value="<?= $id ?>">
                                <input type="hidden" name="id_insignia" value="<?= $idInsignia ?>">
                                <label class="visually-hidden" for="fecha-insignia-<?= $idInsignia ?>">Fecha de entrega</label>
                                <input class="form-control form-control-sm w-auto" type="date" id="fecha-insignia-<?= $idInsignia ?>" name="fecha" value="<?= $hoy ?>" max="<?= $hoy ?>">
                                <button type="submit" class="btn btn-sm boton-primario">Registrar entrega</button>
                            </form>
                        <?php endif; ?>
                    <?php endif; ?>
                </div>
            </div>
        </div>
    <?php endforeach; ?>
</div>
</div><!-- /panel-insignias -->
</div><!-- /tab-content -->

<!-- ================================================================ ESPECIALIDADES -->
<?php if ($pantalla->especialidades !== []): ?>
    <?php
    $suyas = [];
    foreach ($pantalla->especialidades as $grupoArea) {
        foreach ($grupoArea['especialidades'] as $esp) {
            if ($esp->estado !== null) {
                $suyas[] = $esp;
            }
        }
    }

    /** Botones de una especialidad: avanzar (Empezar / Desarrollada) y, si ya la empezó, volver atrás. */
    $botonesEspecialidad = function ($esp) use ($editable, $datosBoton): void {
        if (!$editable) {
            return;
        }
        $idEsp = (int) $esp->especialidad->id;
        if ($esp->estado !== EstadoProgresion::Desarrollada) {
            printf(
                '<button type="button" class="btn btn-sm boton-primario" data-accion="progresion.avanzarEspecialidad" data-datos="%s" data-recargar>%s</button> ',
                $datosBoton(['id_especialidad' => $idEsp]),
                $esp->estado === null ? 'Empezar' : 'Desarrollada',
            );
        }
        if ($esp->estado !== null) {
            printf(
                '<button type="button" class="btn btn-sm btn-outline-secondary" data-accion="progresion.retrocederEspecialidad" data-datos="%s" data-recargar aria-label="Volver atrás"><i class="bi bi-arrow-counterclockwise" aria-hidden="true"></i></button>',
                $datosBoton(['id_especialidad' => $idEsp]),
            );
        }
    };
    ?>
    <h2 class="home-section-title">Especialidades</h2>

    <section class="tarjeta-blanca-borde p-4 mb-3">
        <h3 class="h6 fw-bold texto-violeta">Las suyas</h3>
        <?php if ($suyas === []): ?>
            <p class="text-muted small mb-0">Todavía no empezó ninguna.</p>
        <?php else: ?>
            <ul class="list-group">
                <?php foreach ($suyas as $esp): ?>
                    <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap gap-2">
                        <span>
                            <?= e($esp->especialidad->titulo) ?>
                            <span class="badge text-bg-<?= e(Vista::colorEstadoProgresion($esp->estado)) ?> ms-1"><?= e(Vista::estado($esp->estado)) ?></span>
                            <span class="small text-muted">· <?= e(Vista::fecha($esp->fecha)) ?></span>
                        </span>
                        <span class="d-flex gap-2"><?php $botonesEspecialidad($esp); ?></span>
                    </li>
                <?php endforeach; ?>
            </ul>
        <?php endif; ?>
    </section>

    <div class="accordion mb-4" id="acordeonEspecialidades">
        <?php foreach ($pantalla->especialidades as $grupoArea): ?>
            <?php
            $area = $grupoArea['area'];
            $sinEmpezar = array_filter($grupoArea['especialidades'], fn ($esp) => $esp->estado === null);
            if ($sinEmpezar === []) {
                continue;
            }
            $idPanel = 'especialidades-area-' . (int) $area->id;
            ?>
            <div class="accordion-item">
                <h3 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#<?= $idPanel ?>" aria-expanded="false" aria-controls="<?= $idPanel ?>">
                        <?php $iconoArea = Vista::iconoAreaOficial((int) $area->id); ?>
                        <?php if ($iconoArea !== null): ?>
                            <img class="icono-oficial me-2" src="<?= e($iconoArea) ?>" alt="">
                        <?php else: ?>
                            <i class="bi <?= e(Vista::iconoArea((int) $area->id)) ?> me-2" aria-hidden="true"></i>
                        <?php endif; ?>
                        <?= e($area->titulo) ?>
                        <span class="badge text-bg-light border ms-2"><?= count($sinEmpezar) ?> para empezar</span>
                    </button>
                </h3>
                <div id="<?= $idPanel ?>" class="accordion-collapse collapse" data-bs-parent="#acordeonEspecialidades">
                    <ul class="list-group list-group-flush">
                        <?php foreach ($sinEmpezar as $esp): ?>
                            <li class="list-group-item d-flex justify-content-between align-items-center gap-2">
                                <span><?= e($esp->especialidad->titulo) ?></span>
                                <span><?php $botonesEspecialidad($esp); ?></span>
                            </li>
                        <?php endforeach; ?>
                    </ul>
                </div>
            </div>
        <?php endforeach; ?>
    </div>
<?php endif; ?>
<?php require __DIR__ . '/../includes/pie.php'; ?>
