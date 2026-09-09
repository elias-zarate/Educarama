<?php
$tituloPagina = "Protagonistas";

require_once __DIR__ . '/../../src/Controladores/ProtagonistaController.php';

$controlador = new ProtagonistaController();
$protagonistas = $controlador->listarCards();



?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Educarama - Protagonistas</title>

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

        <div class="barra-superior-inst mb-4 d-flex justify-content-between align-items-center flex-wrap gap-3">
            
            <div>

            <h1 class="fw-bold mb-1">Protagonistas</h1>
            <p class="mb-0">Seleccioná un protagonista para ver su perfil completo.</p>
            </div>
            <a href="/Educarama/views/pages/alta_protagonista.php" class="boton-primario px-4 py-2 fw-bold text-decoration-none">
                <i class="bi bi-plus-circle me-2"></i>Nuevo protagonista
            </a>
        </div>

        

        <div class="row g-4">
            <?php if (!empty($protagonistas)): ?>
                <?php foreach ($protagonistas as $protagonista): ?>
                    <div class="col-12 col-md-6 col-lg-4">
                        <a href="/Educarama/views/pages/perfil_protagonista.php?id=<?= $protagonista['id'] ?>" class="protagonista-card">
                            <div class="protagonista-foto">
                                <i class="bi bi-person-fill"></i>
                            </div>

                            <div class="protagonista-info">
                                <h3>
                                    <?= htmlspecialchars($protagonista['nombre']) ?>
                                    <?= htmlspecialchars($protagonista['apellido']) ?>
                                </h3>

                                <p>
                                    <i class="bi bi-diagram-3 me-1"></i>
                                    <?= htmlspecialchars($protagonista['rama'] ?? 'Sin rama') ?>
                                </p>

                                <p>
                                    <i class="bi bi-calendar-event me-1"></i>
                                    <?= htmlspecialchars($protagonista['fecha_nacimiento'] ?? 'Sin fecha') ?>
                                </p>
                                <p>
                                    <i class="bi bi-calendar-check me-1"></i>
                                    Ingreso: 
                                    <?= !empty($protagonista['anio_ingreso']) 
                                        ? htmlspecialchars($protagonista['anio_ingreso']) 
                                        : 'Sin dato' 
                                    ?>
                                </p>

                                <span class="ver-perfil">Ver perfil</span>
                            </div>

                        </a>
                    </div>
                <?php endforeach; ?>
            <?php else: ?>
                <p>No hay protagonistas cargados.</p>
            <?php endif; ?>
            
        </div>

    </div>
</div>

<?php include __DIR__ . '/includes/barra_inferior.php'; ?>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>