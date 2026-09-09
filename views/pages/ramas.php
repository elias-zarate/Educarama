<?php
$tituloPagina = "Ramas";

require_once __DIR__ . '/../../src/Controladores/RamaController.php';

$controlador = new RamaController();
$ramas = $controlador->listar();
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Educarama - Ramas</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@400;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="/Educarama/public/css/styles.css?v=1.5">
v</head>

<body>
<?php
include __DIR__ . '/includes/barra_superior.php';
include __DIR__ . '/includes/menu_lateral.php';
?>

<div class="main-content-wrapper">
    <div class="container contenedor-principal my-4 p-4">

        <div class="barra-superior-inst mb-4 d-flex justify-content-between align-items-center flex-wrap gap-3">
            <div>
                <h1 class="fw-bold mb-1">Ramas</h1>
                <p class="mb-0">Administrá las ramas del grupo scout.</p>
            </div>

            <a href="/Educarama/views/pages/alta_rama.php" class="boton-primario px-4 py-2 fw-bold text-decoration-none">
                <i class="bi bi-plus-circle me-2"></i>Nueva rama
            </a>
        </div>

        <div class="row g-4">
            <?php if (!empty($ramas)): ?>
                <?php foreach ($ramas as $rama): ?>
                    <div class="col-12 col-md-6 col-lg-4">
                        <div class="rama-card">
                            <div class="rama-icono">
                                <i class="bi bi-diagram-3-fill"></i>
                            </div>

                            <div>
                                <h3><?= htmlspecialchars($rama['nombre']) ?></h3>

                                <p>
                                    <i class="bi bi-collection me-1"></i>
                                    Categoría: <?= htmlspecialchars($rama['categoria'] ?? 'Sin categoría') ?>
                                </p>

                                <p>
                                    <i class="bi bi-house-heart me-1"></i>
                                    Grupo: <?= htmlspecialchars($rama['grupo'] ?? 'Sin grupo') ?>
                                </p>
                            </div>
                        </div>
                    </div>
                <?php endforeach; ?>
            <?php else: ?>
                <p>No hay ramas cargadas.</p>
            <?php endif; ?>
        </div>

    </div>
</div>

<?php include 'includes/barra_inferior.php'; ?>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>