<?php
// views/pages/inicio.php
//
// Página de inicio, adonde lleva el login. Con accesos: sus grupos, con las
// secciones a las que entra y los próximos eventos. Sin ninguno (recién
// registrado): el estado de su solicitud de alta, para que sepa qué sigue.

declare(strict_types=1);

$app = require_once __DIR__ . '/../../src/main.php';

use Educarama\Controller\InicioController;
use Educarama\Controller\Pagina;
use Educarama\Helper\Vista;
use Educarama\Model\Enum\EstadoSolicitud;

$datos = Pagina::cargar(fn () => $app->obtener(InicioController::class)->paginaInicio());
$usuario = $datos['usuario'];
$solicitud = $datos['solicitud'];

$tituloPagina = 'Inicio';
require __DIR__ . '/../includes/cabecera.php';
?>
<section class="barra-superior-inst mb-4 d-flex justify-content-between align-items-center flex-wrap gap-3">
    <div>
        <h1 class="fw-bold mb-1">Hola, <?= e($usuario->nombre) ?></h1>
        <p class="mb-0">Tus grupos, sus secciones y lo que viene.</p>
    </div>
    <?php if ($usuario->esAdmin): ?>
        <a href="<?= e(url('views/pages/solicitudes.php')) ?>" class="boton-primario px-4 py-2 fw-bold text-decoration-none">
            <i class="bi bi-clipboard-check me-2" aria-hidden="true"></i>Solicitudes de alta
        </a>
    <?php endif; ?>
</section>

<?php if ($datos['grupos'] === []): ?>
    <section class="tarjeta-blanca-borde p-4">
        <h2 class="h4 texto-violeta fw-bold">Todavía no tenés acceso a ningún grupo</h2>

        <?php if ($solicitud === null): ?>
            <p>Podés pedir el alta de tu grupo: un administrador la revisa y, si se aprueba, quedás como Jefe del grupo.</p>
            <p>Si tu grupo ya está en Educarama, pedile a su Jefe que te sume con este correo: <strong><?= e($usuario->email) ?></strong>.</p>
            <a href="<?= e(url('views/pages/solicitud_alta.php')) ?>" class="boton-primario px-4 py-2 fw-bold text-decoration-none d-inline-block">
                Pedir el alta de un grupo
            </a>

        <?php elseif ($solicitud->estado === EstadoSolicitud::Pendiente): ?>
            <div class="alert alert-warning">
                <i class="bi bi-hourglass-split me-1" aria-hidden="true"></i>
                Tu solicitud para <strong><?= e(Vista::nombreCompletoGrupo($solicitud->datosGrupo['nombre'])) ?></strong>
                (enviada el <?= e(Vista::fechaDeFechaHora($solicitud->fecha)) ?>) está pendiente de revisión.
            </div>
            <a href="<?= e(url('views/pages/solicitud_alta.php')) ?>" class="btn btn-outline-secondary">Ver mi solicitud</a>

        <?php else: ?>
            <div class="alert alert-danger">
                <p class="fw-bold mb-1"><i class="bi bi-x-circle me-1" aria-hidden="true"></i>Tu última solicitud fue rechazada</p>
                <p class="mb-0"><?= e($solicitud->motivoRechazo ?? 'Sin motivo.') ?></p>
            </div>
            <a href="<?= e(url('views/pages/solicitud_alta.php')) ?>" class="boton-primario px-4 py-2 fw-bold text-decoration-none d-inline-block">
                Enviar otra solicitud
            </a>
        <?php endif; ?>
    </section>
<?php endif; ?>

<?php foreach ($datos['grupos'] as $fila): ?>
    <?php
    $grupoPantalla = $fila['grupo'];
    $idGrupo = (int) $grupoPantalla->grupo->id;
    ?>
    <section class="mb-5" aria-labelledby="grupo-<?= $idGrupo ?>">
        <div class="encabezado-grupo d-flex justify-content-between align-items-center flex-wrap gap-2 mb-3 p-3 rounded-3">
            <div>
                <h2 class="h4 fw-bold texto-violeta m-0" id="grupo-<?= $idGrupo ?>">
                    <?= e(Vista::nombreCompletoGrupo($grupoPantalla->grupo->nombre)) ?>
                </h2>
            </div>
            <a href="<?= e(url('views/pages/grupo.php?id=' . $idGrupo)) ?>" class="btn btn-outline-secondary btn-sm">
                Resumen del grupo <i class="bi bi-arrow-right" aria-hidden="true"></i>
            </a>
        </div>

        <?php $conteo = $fila['conteo']; ?>
        <div class="row g-3 mb-4">
            <div class="col-6 col-md-3">
                <div class="stat-tile">
                    <i class="bi bi-people stat-tile-icono" aria-hidden="true"></i>
                    <span class="stat-tile-numero"><?= $conteo['protagonistas'] ?></span>
                    <span class="stat-tile-label">Protagonistas</span>
                </div>
            </div>
            <div class="col-6 col-md-3">
                <div class="stat-tile">
                    <i class="bi bi-flag stat-tile-icono" aria-hidden="true"></i>
                    <span class="stat-tile-numero"><?= $conteo['patrullas'] ?></span>
                    <span class="stat-tile-label">Patrullas</span>
                </div>
            </div>
            <div class="col-6 col-md-3">
                <div class="stat-tile">
                    <i class="bi bi-diagram-3 stat-tile-icono" aria-hidden="true"></i>
                    <span class="stat-tile-numero"><?= count($grupoPantalla->secciones) ?></span>
                    <span class="stat-tile-label">Secciones</span>
                </div>
            </div>
            <div class="col-6 col-md-3">
                <div class="stat-tile">
                    <i class="bi bi-calendar-event stat-tile-icono" aria-hidden="true"></i>
                    <span class="stat-tile-numero"><?= count($fila['proximos']) ?></span>
                    <span class="stat-tile-label">Próx. eventos</span>
                </div>
            </div>
        </div>

        <div class="row g-3 mb-4">
            <?php foreach ($grupoPantalla->secciones as $seccion): ?>
                <?php
                // El logo oficial de la rama, o el ícono de Bootstrap si la rama no tiene uno.
                $logoRama = Vista::logoRama($seccion->rama->id);
                $marcaRama = $logoRama !== null
                    ? '<div class="rama-icono con-logo"><img src="' . e($logoRama) . '" alt=""></div>'
                    : '<div class="rama-icono"><i class="bi ' . e(Vista::iconoRama($seccion->rama->id)) . '" aria-hidden="true"></i></div>';

                $contenido = $marcaRama
                    . '<div><h3>' . e(Vista::nombreCompletoSeccion($seccion->seccion->nombre, $seccion->rama->id)) . '</h3>'
                    . '<p>' . e($seccion->rama->titulo) . ($seccion->nivel !== null ? ' · ' . e($seccion->nivel->etiqueta()) : '') . '</p></div>';
                ?>
                <div class="col-12 col-md-6">
                    <?php if ($seccion->nivel !== null): ?>
                        <a class="rama-card text-decoration-none <?= e(Vista::claseRama($seccion->rama->id)) ?>"
                           href="<?= e(url('views/pages/perfil_seccion.php?id=' . $seccion->seccion->id)) ?>">
                            <?= $contenido ?>
                        </a>
                    <?php else: ?>
                        <?php /* Solo la ve por su rol de grupo: sin acceso al detalle de la sección. */ ?>
                        <div class="rama-card <?= e(Vista::claseRama($seccion->rama->id)) ?> opacity-75">
                            <?= $contenido ?>
                        </div>
                    <?php endif; ?>
                </div>
            <?php endforeach; ?>
        </div>

        <?php
        // Acciones rápidas: si tiene al menos una sección donde puede editar,
        // mostrar los links para crear protagonista y patrulla.
        $seccionEditable = null;
        foreach ($grupoPantalla->secciones as $sec) {
            if ($sec->puedeEditar()) {
                $seccionEditable = $sec;
                break;
            }
        }
        ?>
        <?php if ($seccionEditable !== null): ?>
            <div class="d-flex flex-wrap gap-2 mb-4">
                <a href="<?= e(url('views/pages/protagonista_form.php?id_seccion=' . $seccionEditable->seccion->id)) ?>" class="boton-primario px-3 py-2 fw-bold text-decoration-none">
                    <i class="bi bi-person-plus me-1" aria-hidden="true"></i>Nuevo protagonista
                </a>
                <a href="<?= e(url('views/pages/protagonistas.php')) ?>" class="btn btn-outline-secondary">
                    <i class="bi bi-list-ul me-1" aria-hidden="true"></i>Ver nómina
                </a>
            </div>
        <?php endif; ?>

        <h3 class="h5 texto-violeta fw-bold">Próximos eventos</h3>
        <?php if ($fila['proximos'] === []): ?>
            <p class="text-muted">No hay eventos próximos.</p>
        <?php else: ?>
            <ul class="list-group">
                <?php foreach ($fila['proximos'] as $proximo): ?>
                    <?php $evento = $proximo->evento; ?>
                    <li class="list-group-item">
                        <div class="d-flex justify-content-between align-items-start flex-wrap gap-2">
                            <strong><?= e($evento->titulo) ?></strong>
                            <span class="badge text-bg-<?= e(Vista::colorPrioridad($evento->prioridad)) ?>"><?= e($evento->prioridad->etiqueta()) ?></span>
                        </div>
                        <div class="small text-muted">
                            <i class="bi bi-calendar-event me-1" aria-hidden="true"></i><?= e(Vista::periodo($evento->fechaInicio, $evento->fechaFin)) ?>
                            · <?= $proximo->esDeTodoElGrupo() ? 'Todo el grupo' : 'Algunas secciones' ?>
                        </div>
                    </li>
                <?php endforeach; ?>
            </ul>
        <?php endif; ?>
    </section>
<?php endforeach; ?>
<?php require __DIR__ . '/../includes/pie.php'; ?>
