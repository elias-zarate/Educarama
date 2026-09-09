<?php
require_once __DIR__ . '/../../src/Controladores/AuthMiddleware.php';
?>
<?php

ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

$tituloPagina = "Gestionar eventos";

require_once __DIR__ . '/../../src/Controladores/EventoControlador.php';

$controlador = new EventoControlador();

try {
    $controlador->procesarAcciones();

    $eventoEditar = $controlador->obtenerEventosParaEditar();
    $eventos = $controlador->listarEventos();

} catch (Exception $e) {
    $error = $e->getMessage();

    $eventoEditar = null;
    $eventos = [];
}
?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Educarama - Eventos</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@400;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="/Educarama/public/css/styles.css?v=1.5">
</head>

<body>
<?php
include __DIR__ . '/includes/barra_superior.php';
include __DIR__ . '/includes/menu_lateral.php';
?>

<div class="main-content-wrapper">
    <div class="container contenedor-principal my-4 p-4">
        <?php if (!empty($error)): ?>
    <div class="alert alert-danger">
        <?= htmlspecialchars($error) ?>
    </div>
        <?php endif; ?>

        <div class="barra-superior-inst mb-4">
            <h1 class="fw-bold mb-1">Eventos        
            </h1>
            <p class="mb-0">Administrá los eventos que se muestran en la pantalla de inicio.</p>
        </div>


        <div class="tarjeta-blanca-borde p-4">
            <div class="d-flex justify-content-between align-items-center mb-3 flex-wrap gap-2">
                <h2 class="h4 texto-violeta fw-bold mb-0">Eventos cargados</h2>


            </div>

            <?php if (!empty($eventos)): ?>
                <div class="table-responsive">
                    <table class="table align-middle">
                        <thead>
                            <tr>
                                <th>Título</th>
                                <th>Prioridad</th>
                                <th>Inicio</th>
                                <th>Fin</th>
                                <th>Estado</th>
                                <th class="text-end">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <?php foreach ($eventos as $evento): ?>
                                <tr>
                                    <td>
                                        <strong><?= htmlspecialchars($evento['titulo']) ?></strong><br>
                                        <small><?= htmlspecialchars($evento['descripcion']) ?></small>
                                    </td>
                                    <td><?= ucfirst(htmlspecialchars($evento['prioridad'])) ?></td>
                                    <td><?= htmlspecialchars($evento['fecha_inicio']) ?></td>
                                    <td><?= $evento['fecha_fin'] ? htmlspecialchars($evento['fecha_fin']) : 'Sin fin' ?></td>
                                    <td>
                                        <?php if ($evento['activo']): ?>
                                            <span class="badge bg-success">Activo</span>
                                        <?php else: ?>
                                            <span class="badge bg-secondary">Inactivo</span>
                                        <?php endif; ?>
                                    </td>
                                    <td class="text-end">
                                        <a href="/Educarama/views/pages/gestionar_eventos.php?editar=<?= $evento['id'] ?>" class="btn btn-sm btn-warning">
                                            <i class="bi bi-pencil"></i>
                                        </a>

                                        <?php if ($evento['activo']): ?>
                                            <a
                                                href="#"
                                                class="btn btn-sm btn-danger btn-desactivar"
                                                data-id="<?= $evento['id'] ?>"
                                                data-titulo="<?= htmlspecialchars($evento['titulo']) ?>"
                                            >
                                                <i class="bi bi-trash"></i>
                                            </a>
                                        <?php else: ?>
                                            <a 
                                                href="/Educarama/views/pages/gestionar_eventos.php?accion=reactivar&id=<?= $evento  ['id'] ?>" 
                                                class="btn btn-sm btn-success"
                                            >
                                                <i class="bi bi-arrow-clockwise"></i>
                                            </a>
                                        <?php endif; ?>
                                    </td>
                                </tr>
                            <?php endforeach; ?>
                        </tbody>
                    </table>
                </div>
            <?php else: ?>
                <p>No hay eventos cargados.</p>
            <?php endif; ?>
            
            <!-- BOTONES DE ACCIÓN -->
            <div class="row g-3">

          
                <div class="col-12 col-md-4">
                    <a 
                        href="gestionar_eventos.php" 
                        class="boton-primario w-100 py-3 fw-bold text-decoration-none text-center d-block">
                        <i class="bi bi-person-gear me-2"></i>Crear nuevo evento
                        </a>
                </div>
            </div>
        </div>

    </div>
</div>

<?php include __DIR__ . '/includes/barra_inferior.php'; ?>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<script>

document.querySelectorAll('.btn-desactivar').forEach(boton => {

    boton.addEventListener('click', function(e){

        e.preventDefault();

        let id = this.dataset.id;
        let titulo = this.dataset.titulo;

        Swal.fire({

            title: '¿Desactivar evento?',
            html: `
                <b>${titulo}</b><br><br>
                El evento dejará de mostrarse en la pantalla de inicio.
            `,
            icon: 'warning',

            showCancelButton: true,

            confirmButtonColor: '#45009e',
            cancelButtonColor: '#d33',

            confirmButtonText: 'Sí, desactivar',
            cancelButtonText: 'Cancelar'

        }).then((result)=>{

            if(result.isConfirmed){

                window.location =
                    'gestionar_eventos.php?accion=desactivar&id=' + id;

            }

        });

    });

});

</script>
</body>
</html>