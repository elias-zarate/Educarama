<?php
// views/pages/perfil_protagonista.php
//
// La página central del protagonista, con dos pestañas:
//  - Perfil: sus datos, el resumen de su progresión (con los links a
//    Competencias y a Etapas e insignias) y sus familiares.
//  - Documentos: ver, descargar, subir, editar y eliminar.
// El menú ⋮ del encabezado tiene Editar, Cambiar de sección y Eliminar, según el rol.

declare(strict_types=1);

$app = require_once __DIR__ . '/../../src/main.php';

use Educarama\Controller\Pagina;
use Educarama\Controller\ProtagonistaController;
use Educarama\Helper\Csrf;
use Educarama\Helper\Vista;

$datos = Pagina::cargar(fn () => $app->obtener(ProtagonistaController::class)->paginaPerfil($_GET));
$ficha = $datos['ficha'];
$seccion = $datos['seccion'];
$resumen = $datos['resumen'];
$persona = $ficha->persona;
$protagonista = $ficha->protagonista;
$id = (int) $protagonista->id;
$edad = Vista::edad($persona->fechaNacimiento);
$equipo = $seccion->pequenioEquipo();
$nombre = $persona->nombre . ' ' . $persona->apellido;

$paginaActual = 'perfil';
$idProtagonista = $id;

$tituloPagina = $nombre;
$claseRama = Vista::claseRama($seccion->rama->id);
require __DIR__ . '/../includes/cabecera.php';
?>
<section class="barra-superior-inst mb-3 d-flex justify-content-between align-items-start gap-3">
    <div>
        <h1 class="fw-bold mb-1"><?= e($nombre) ?><?= $edad !== null ? ', ' . $edad . ' años' : '' ?></h1>
        <p class="mb-0 fw-bold">
            <a class="text-reset" href="<?= e(url('views/pages/perfil_seccion.php?id=' . $seccion->seccion->id)) ?>"><?= e(Vista::nombreCompletoSeccion($seccion->seccion->nombre, $seccion->rama->id)) ?></a>
            · <?= e($seccion->rama->titulo) ?>
        </p>
    </div>

    <?php if ($seccion->puedeEditar() || $seccion->puedeEliminar() || $datos['seccionesDestino'] !== []): ?>
        <div class="dropdown">
            <button class="btn btn-light" type="button" data-bs-toggle="dropdown" aria-expanded="false" aria-label="Más acciones">
                <i class="bi bi-three-dots-vertical" aria-hidden="true"></i>
            </button>
            <ul class="dropdown-menu dropdown-menu-end">
                <?php if ($seccion->puedeEditar()): ?>
                    <li><a class="dropdown-item" href="<?= e(url('views/pages/protagonista_form.php?id=' . $id)) ?>"><i class="bi bi-pencil me-2" aria-hidden="true"></i>Editar datos</a></li>
                <?php endif; ?>
                <?php if ($datos['seccionesDestino'] !== []): ?>
                    <li><button class="dropdown-item" type="button" data-bs-toggle="modal" data-bs-target="#modalPase"><i class="bi bi-arrow-left-right me-2" aria-hidden="true"></i>Cambiar de sección</button></li>
                <?php endif; ?>
                <?php if ($seccion->puedeEliminar()): ?>
                    <li><hr class="dropdown-divider"></li>
                    <li>
                        <button class="dropdown-item text-danger" type="button" data-accion="protagonista.eliminar"
                                data-datos='<?= e(json_encode(['id_protagonista' => $id])) ?>'
                                data-confirmar="¿Eliminar a <?= e($nombre) ?>?"
                                data-texto="Se borran su progresión, sus documentos y sus vínculos con familiares. No se puede deshacer."
                                data-boton="Sí, eliminar" data-peligro>
                            <i class="bi bi-trash me-2" aria-hidden="true"></i>Eliminar
                        </button>
                    </li>
                <?php endif; ?>
            </ul>
        </div>
    <?php endif; ?>
</section>

<?php require __DIR__ . '/../includes/nav_protagonista.php'; ?>

<ul class="nav nav-tabs tabs-inst mb-4" role="tablist">
    <li class="nav-item" role="presentation">
        <button class="nav-link active" id="pestania-perfil" data-bs-toggle="tab" data-bs-target="#perfil" type="button" role="tab" aria-controls="perfil" aria-selected="true">
            <i class="bi bi-person me-1" aria-hidden="true"></i>Perfil
        </button>
    </li>
    <li class="nav-item" role="presentation">
        <button class="nav-link" id="pestania-documentos" data-bs-toggle="tab" data-bs-target="#documentos" type="button" role="tab" aria-controls="documentos" aria-selected="false">
            <i class="bi bi-folder2-open me-1" aria-hidden="true"></i>Documentos <span class="badge text-bg-secondary"><?= count($datos['documentos']) ?></span>
        </button>
    </li>
</ul>

<div class="tab-content">
    <!-- ================================================================ PERFIL -->
    <div class="tab-pane fade show active" id="perfil" role="tabpanel" aria-labelledby="pestania-perfil" tabindex="0">
        <div class="row g-4 mb-4">
            <div class="col-lg-5">
                <section class="tarjeta-blanca-borde p-4 h-100">
                    <h2 class="h5 texto-violeta fw-bold mb-3">Datos</h2>
                    <p class="dato-etiqueta">DNI</p>
                    <p class="dato-valor"><?= e(number_format($persona->dni, 0, ',', '.')) ?></p>
                    <p class="dato-etiqueta">Fecha de nacimiento</p>
                    <p class="dato-valor"><?= $persona->fechaNacimiento !== null ? e(Vista::fecha($persona->fechaNacimiento)) : '<span class="text-muted">Sin dato</span>' ?></p>
                    <?php if ($equipo !== null): ?>
                        <p class="dato-etiqueta"><?= e($equipo) ?></p>
                        <p class="dato-valor">
                            <?php if ($ficha->nombrePatrulla !== null): ?>
                                <a href="<?= e(url('views/pages/perfil_patrulla.php?id=' . $protagonista->idPatrulla)) ?>"><?= e($ficha->nombrePatrulla) ?></a>
                            <?php else: ?>
                                <span class="text-muted">Sin <?= e(mb_strtolower($equipo)) ?></span>
                            <?php endif; ?>
                        </p>
                    <?php endif; ?>
                    <p class="dato-etiqueta">Año de ingreso</p>
                    <p class="dato-valor"><?= $protagonista->anioIngreso !== null ? (int) $protagonista->anioIngreso : '<span class="text-muted">Sin dato</span>' ?></p>
                    <p class="dato-etiqueta">Promesa</p>
                    <p class="dato-valor mb-0">
                        <?= $protagonista->promesa
                            ? '<span class="badge text-bg-success"><i class="bi bi-check-lg me-1" aria-hidden="true"></i>Hizo la Promesa</span>'
                            : '<span class="text-muted">Todavía no</span>' ?>
                    </p>
                </section>
            </div>

            <div class="col-lg-7">
                <section class="tarjeta-blanca-borde p-4 h-100">
                    <h2 class="h5 texto-violeta fw-bold mb-3">Progresión personal</h2>

                    <div class="row g-3 mb-3">
                        <div class="col-sm-6">
                            <p class="dato-etiqueta">Etapa actual</p>
                            <p class="dato-valor mb-0"><?= $resumen->etapaActual !== null ? e($resumen->etapaActual->titulo) : '<span class="text-muted">Ninguna todavía</span>' ?></p>
                        </div>
                        <div class="col-sm-6">
                            <p class="dato-etiqueta">Etapa en curso</p>
                            <p class="dato-valor mb-0">
                                <?php if ($resumen->etapaEnCurso !== null): ?>
                                    <?= e($resumen->etapaEnCurso->titulo) ?>
                                    <span class="text-muted small">(<?= $resumen->enCursoDesarrolladas ?>/<?= $resumen->cupoEnCurso() ?> desarrolladas)</span>
                                <?php else: ?>
                                    <span class="text-muted">Logró todas las etapas</span>
                                <?php endif; ?>
                            </p>
                        </div>
                    </div>

                    <?php $porcentaje = $resumen->competenciasTotal > 0 ? (int) round(100 * $resumen->desarrolladasTotal / $resumen->competenciasTotal) : 0; ?>
                    <div class="barra-progreso mb-1" role="progressbar" aria-label="Competencias desarrolladas" aria-valuenow="<?= $porcentaje ?>" aria-valuemin="0" aria-valuemax="100">
                        <div class="progreso-actual" style="width: <?= $porcentaje ?>%"></div>
                    </div>
                    <p class="small fw-bold texto-violeta mb-3">Competencias desarrolladas: <?= $resumen->desarrolladasTotal ?> / <?= $resumen->competenciasTotal ?></p>

                    <?php if ($resumen->insigniaPendiente !== null): ?>
                        <div class="alert alert-warning py-2 small">
                            <i class="bi bi-award me-1" aria-hidden="true"></i>
                            Tiene la etapa <?= e($resumen->etapaEnCurso?->titulo ?? '') ?> en curso y todavía no recibió su insignia.
                        </div>
                    <?php endif; ?>
                    <?php if ($resumen->etapaEnCurso !== null && $resumen->etapaLista()): ?>
                        <div class="alert alert-success py-2 small">
                            <i class="bi bi-check2-circle me-1" aria-hidden="true"></i>
                            Cumplió todo para confirmar la etapa <?= e($resumen->etapaEnCurso->titulo) ?>.
                        </div>
                    <?php endif; ?>

                    <?php
                    $elementoPorEtapa = $datos['insignias']['elementoPorEtapa'];
                    $numeroPorEtapa = $datos['insignias']['numeroPorEtapa'];
                    $idRamaPerfil = $datos['insignias']['idRama'];
                    $todasInsignias = $datos['insignias']['insignias'];

                    $claseRompecabezas = match ($idRamaPerfil) {
                        1 => 'puzzle-lobatos',
                        2 => 'puzzle-scouts',
                        3 => 'puzzle-caminantes',
                        4 => 'puzzle-rovers',
                        default => '',
                    };

                    if ($idRamaPerfil === 3) {
                        // Caminantes: posición fija por elemento, no por etapa.
                        // Tierra arriba, Agua izquierda, Aire derecha, Fuego abajo.
                        $insigPorElemento = [];
                        foreach ($todasInsignias as $i) {
                            if ($i->insignia->idEtapa === null) {
                                continue;
                            }
                            $elem = $elementoPorEtapa[$i->insignia->idEtapa] ?? null;
                            if ($elem !== null) {
                                $insigPorElemento[$elem->value] = $i;
                            }
                        }
                        $piezas = [];
                        foreach (['tierra', 'agua', 'aire', 'fuego'] as $elem) {
                            $img = Vista::imagenInsignia("caminantes-$elem.png");
                            $ins = $insigPorElemento[$elem] ?? null;
                            $otorgada = $ins !== null && $ins->otorgada();
                            $titulo = $ins !== null ? $ins->insignia->titulo : ucfirst($elem);
                            $piezas[$elem] = ['otorgada' => $otorgada, 'imagen' => $img, 'titulo' => $titulo];
                        }
                    } else {
                        // LyL, Scouts y Rovers: posición por número de etapa.
                        $piezas = [];
                        foreach ($todasInsignias as $i) {
                            if ($i->insignia->idEtapa === null || $i->insignia->imagen === null) {
                                continue;
                            }
                            $num = $numeroPorEtapa[$i->insignia->idEtapa] ?? 0;
                            $piezas[$num] = ['otorgada' => $i->otorgada(), 'imagen' => Vista::imagenInsignia($i->insignia->imagen), 'titulo' => $i->insignia->titulo];
                        }
                    }
                    ?>
                    <?php if ($piezas !== []): ?>
                        <div class="puzzle-insignias <?= $claseRompecabezas ?> mt-3 mb-3">
                            <?php foreach ($piezas as $clave => $iv): ?>
                                <div class="puzzle-pieza pieza-<?= e((string) $clave) ?> <?= $iv['otorgada'] ? 'entregada' : 'pendiente' ?>" title="<?= e($iv['titulo'] . ($iv['otorgada'] ? ' ✓' : '')) ?>">
                                    <img src="<?= e($iv['imagen']) ?>" alt="<?= e($iv['titulo']) ?>">
                                </div>
                            <?php endforeach; ?>
                        </div>
                    <?php endif; ?>

                    <div class="d-flex flex-wrap gap-2 justify-content-center">
                        <a class="boton-primario px-3 py-2 fw-bold text-decoration-none" href="<?= e(url('views/pages/gestionar_competencias.php?id=' . $id)) ?>">
                            <i class="bi bi-list-check me-1" aria-hidden="true"></i>Competencias
                        </a>
                        <a class="btn btn-outline-secondary" href="<?= e(url('views/pages/progresion.php?id=' . $id)) ?>">
                            <i class="bi bi-layers me-1" aria-hidden="true"></i>Etapas
                        </a>
                        <a class="btn btn-outline-secondary" href="<?= e(url('views/pages/progresion.php?id=' . $id . '#panel-insignias')) ?>">
                            <i class="bi bi-award me-1" aria-hidden="true"></i>Insignias
                        </a>
                    </div>
                </section>
            </div>
        </div>

        <section class="tarjeta-blanca-borde p-4">
            <div class="d-flex justify-content-between align-items-center flex-wrap gap-2 mb-3">
                <h2 class="h5 texto-violeta fw-bold mb-0">Familiares</h2>
                <?php if ($seccion->puedeEditar()): ?>
                    <a class="btn btn-outline-secondary btn-sm" href="<?= e(url('views/pages/familiar_form.php?id_protagonista=' . $id)) ?>">
                        <i class="bi bi-person-plus me-1" aria-hidden="true"></i>Agregar familiar
                    </a>
                <?php endif; ?>
            </div>

            <?php if ($datos['familiares'] === []): ?>
                <p class="text-muted mb-0">No tiene familiares cargados.</p>
            <?php else: ?>
                <div class="list-group">
                    <?php foreach ($datos['familiares'] as $f): ?>
                        <?php $nombreFamiliar = $f->persona->nombre . ' ' . $f->persona->apellido; ?>
                        <div class="list-group-item d-flex justify-content-between align-items-start flex-wrap gap-2">
                            <div>
                                <p class="fw-bold mb-1">
                                    <?= e($nombreFamiliar) ?>
                                    <?php if ($f->vinculo->vinculo !== null): ?><span class="text-muted fw-normal">· <?= e($f->vinculo->vinculo) ?></span><?php endif; ?>
                                    <?php if ($f->vinculo->esContactoDeEmergencia): ?>
                                        <span class="badge text-bg-danger ms-1"><i class="bi bi-telephone-plus me-1" aria-hidden="true"></i>Emergencia</span>
                                    <?php endif; ?>
                                </p>
                                <p class="small mb-0">
                                    <?php if ($f->familiar->telefonoContacto !== null): ?>
                                        <i class="bi bi-telephone me-1" aria-hidden="true"></i><a href="tel:<?= e(preg_replace('/[^0-9+]/', '', $f->familiar->telefonoContacto)) ?>"><?= e($f->familiar->telefonoContacto) ?></a>
                                    <?php endif; ?>
                                    <?php if ($f->familiar->emailContacto !== null): ?>
                                        <span class="ms-2"><i class="bi bi-envelope me-1" aria-hidden="true"></i><a href="mailto:<?= e($f->familiar->emailContacto) ?>"><?= e($f->familiar->emailContacto) ?></a></span>
                                    <?php endif; ?>
                                    <?php if ($f->familiar->telefonoContacto === null && $f->familiar->emailContacto === null): ?>
                                        <span class="text-muted">Sin datos de contacto</span>
                                    <?php endif; ?>
                                </p>
                            </div>
                            <?php if ($seccion->puedeEditar()): ?>
                                <div class="d-flex gap-2">
                                    <a class="btn btn-outline-secondary btn-sm" href="<?= e(url('views/pages/familiar_form.php?id_protagonista=' . $id . '&id_familiar=' . $f->familiar->id)) ?>">
                                        <i class="bi bi-pencil" aria-hidden="true"></i><span class="visually-hidden">Editar a <?= e($nombreFamiliar) ?></span>
                                    </a>
                                    <button type="button" class="btn btn-outline-danger btn-sm" data-accion="familiar.desvincular"
                                            data-datos='<?= e(json_encode(['id_protagonista' => $id, 'id_familiar' => $f->familiar->id])) ?>'
                                            data-confirmar="¿Desvincular a <?= e($nombreFamiliar) ?>?" data-boton="Sí, desvincular" data-peligro>
                                        <i class="bi bi-x-lg" aria-hidden="true"></i><span class="visually-hidden">Desvincular a <?= e($nombreFamiliar) ?></span>
                                    </button>
                                </div>
                            <?php endif; ?>
                        </div>
                    <?php endforeach; ?>
                </div>
            <?php endif; ?>
        </section>
    </div>

    <!-- ================================================================ DOCUMENTOS -->
    <div class="tab-pane fade" id="documentos" role="tabpanel" aria-labelledby="pestania-documentos" tabindex="0">
        <section class="tarjeta-blanca-borde p-4">
            <div class="d-flex justify-content-between align-items-center flex-wrap gap-2 mb-3">
                <h2 class="h5 texto-violeta fw-bold mb-0">Documentos</h2>
                <?php if ($seccion->puedeEditar()): ?>
                    <button type="button" class="boton-primario px-3 py-2 fw-bold" data-bs-toggle="modal" data-bs-target="#modalSubir">
                        <i class="bi bi-upload me-1" aria-hidden="true"></i>Subir documento
                    </button>
                <?php endif; ?>
            </div>

            <?php if ($datos['documentos'] === []): ?>
                <p class="text-muted mb-0"><i class="bi bi-info-circle me-1" aria-hidden="true"></i>No hay documentos para este protagonista.</p>
            <?php else: ?>
                <div class="list-group">
                    <?php foreach ($datos['documentos'] as $d): ?>
                        <?php
                        $idDoc = (int) $d->id;
                        $esImagen = str_starts_with($d->tipoMime, 'image/');
                        $urlDescarga = url('public/descargar.php?id=' . $idDoc);
                        ?>
                        <div class="list-group-item">
                            <div class="d-flex justify-content-between align-items-start flex-wrap gap-2">
                                <div>
                                    <p class="fw-bold mb-1">
                                        <img class="icono-oficial me-1" src="<?= e(Vista::iconoOficial($esImagen ? 'png' : 'pdf')) ?>" alt="">
                                        <?= e($d->nombreOriginal) ?>
                                        <?php if ($d->tipoDocumento !== null): ?><span class="badge text-bg-light border ms-1"><?= e($d->tipoDocumento) ?></span><?php endif; ?>
                                    </p>
                                    <?php if ($d->descripcion !== null): ?><p class="small mb-1"><?= nl2br(e($d->descripcion)) ?></p><?php endif; ?>
                                    <p class="small text-muted mb-0">Subido el <?= e(Vista::fechaDeFechaHora($d->fechaSubida)) ?> · <?= e(Vista::tamanio($d->tamanioBytes)) ?></p>
                                </div>
                                <div class="d-flex flex-wrap gap-2">
                                    <?php if ($esImagen): ?>
                                        <a class="btn btn-outline-secondary btn-sm" href="<?= e($urlDescarga . '&ver=1') ?>" target="_blank" rel="noopener">
                                            <i class="bi bi-eye me-1" aria-hidden="true"></i>Ver
                                        </a>
                                    <?php endif; ?>
                                    <a class="btn btn-outline-secondary btn-sm" href="<?= e($urlDescarga) ?>">
                                        <i class="bi bi-download me-1" aria-hidden="true"></i>Descargar
                                    </a>
                                    <?php if ($seccion->puedeEditar()): ?>
                                        <button type="button" class="btn btn-outline-secondary btn-sm" data-bs-toggle="collapse" data-bs-target="#editar-doc-<?= $idDoc ?>" aria-expanded="false">
                                            <i class="bi bi-pencil" aria-hidden="true"></i><span class="visually-hidden">Editar</span>
                                        </button>
                                    <?php endif; ?>
                                    <?php if ($seccion->puedeEliminar()): ?>
                                        <button type="button" class="btn btn-outline-danger btn-sm" data-accion="documento.eliminar"
                                                data-datos='<?= e(json_encode(['id_documento' => $idDoc])) ?>'
                                                data-confirmar="¿Eliminar «<?= e($d->nombreOriginal) ?>»?" data-texto="El archivo se borra y no se puede recuperar."
                                                data-boton="Sí, eliminar" data-peligro>
                                            <i class="bi bi-trash" aria-hidden="true"></i><span class="visually-hidden">Eliminar</span>
                                        </button>
                                    <?php endif; ?>
                                </div>
                            </div>

                            <?php if ($seccion->puedeEditar()): ?>
                                <div class="collapse mt-3" id="editar-doc-<?= $idDoc ?>">
                                    <form class="row g-2" data-accion="documento.actualizar" data-recargar>
                                        <?= Csrf::campo() ?>
                                        <input type="hidden" name="id_documento" value="<?= $idDoc ?>">
                                        <div class="col-md-4">
                                            <label class="form-label small" for="tipo-<?= $idDoc ?>">Tipo</label>
                                            <input class="form-control form-control-sm" id="tipo-<?= $idDoc ?>" name="tipo_documento" maxlength="50" value="<?= e($d->tipoDocumento) ?>">
                                        </div>
                                        <div class="col-md-8">
                                            <label class="form-label small" for="desc-<?= $idDoc ?>">Descripción</label>
                                            <input class="form-control form-control-sm" id="desc-<?= $idDoc ?>" name="descripcion" value="<?= e($d->descripcion) ?>">
                                        </div>
                                        <div class="col-12"><button type="submit" class="btn btn-sm boton-primario">Guardar</button></div>
                                    </form>
                                </div>
                            <?php endif; ?>
                        </div>
                    <?php endforeach; ?>
                </div>
            <?php endif; ?>
        </section>
    </div>
</div>

<?php if ($seccion->puedeEditar()): ?>
    <!-- Subir un documento -->
    <div class="modal fade" id="modalSubir" tabindex="-1" aria-labelledby="tituloSubir" aria-hidden="true">
        <div class="modal-dialog">
            <form class="modal-content" data-accion="documento.subir">
                <?= Csrf::campo() ?>
                <input type="hidden" name="id_protagonista" value="<?= $id ?>">
                <div class="modal-header">
                    <h2 class="modal-title h5" id="tituloSubir">Subir un documento</h2>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="archivo" class="form-label">Archivo</label>
                        <input type="file" class="form-control" id="archivo" name="archivo" accept=".pdf,.jpg,.jpeg,.png,application/pdf,image/jpeg,image/png" required>
                        <div class="form-text">PDF, JPG o PNG, hasta 5 MB.</div>
                    </div>
                    <div class="mb-3">
                        <label for="tipo_documento" class="form-label">Tipo <span class="text-muted">(opcional)</span></label>
                        <input type="text" class="form-control" id="tipo_documento" name="tipo_documento" maxlength="50" placeholder="Ej.: Ficha médica">
                    </div>
                    <div>
                        <label for="descripcion" class="form-label">Descripción <span class="text-muted">(opcional)</span></label>
                        <textarea class="form-control" id="descripcion" name="descripcion" rows="2"></textarea>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="submit" class="boton-primario px-3 py-2 fw-bold">Subir</button>
                </div>
            </form>
        </div>
    </div>
<?php endif; ?>

<?php if ($datos['seccionesDestino'] !== []): ?>
    <!-- Cambiar de sección -->
    <div class="modal fade" id="modalPase" tabindex="-1" aria-labelledby="tituloPase" aria-hidden="true">
        <div class="modal-dialog">
            <form class="modal-content" data-accion="protagonista.cambiarSeccion"
                  data-confirmar="¿Pasar a <?= e($nombre) ?> a otra sección?"
                  data-texto="Queda sin <?= e(mb_strtolower($equipo ?? 'patrulla')) ?>. Su progresión de esta rama queda guardada como historial.">
                <?= Csrf::campo() ?>
                <input type="hidden" name="id_protagonista" value="<?= $id ?>">
                <div class="modal-header">
                    <h2 class="modal-title h5" id="tituloPase">Cambiar de sección</h2>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <label for="id_seccion" class="form-label">Sección de destino</label>
                    <select class="form-select" id="id_seccion" name="id_seccion" required>
                        <?php foreach ($datos['seccionesDestino'] as $destino): ?>
                            <option value="<?= (int) $destino->seccion->id ?>"><?= e(Vista::nombreCompletoSeccion($destino->seccion->nombre, $destino->rama->id)) ?></option>
                        <?php endforeach; ?>
                    </select>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="submit" class="boton-primario px-3 py-2 fw-bold">Cambiar de sección</button>
                </div>
            </form>
        </div>
    </div>
<?php endif; ?>
<?php require __DIR__ . '/../includes/pie.php'; ?>
