<?php
$tituloPagina = "Patrullas";

require_once __DIR__ . '/../../src/Controladores/PatrullaController.php';

$controlador = new PatrullaController();
$patrullas = $controlador->listar();
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Educarama - Patrullas</title>

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
                <h1 class="fw-bold mb-1">Patrullas</h1>
                <p class="mb-0">Administrá las patrullas o equipos del grupo.</p>
            </div>

            <a href="/Educarama/views/pages/alta_patrulla.php" class="boton-primario px-4 py-2 fw-bold text-decoration-none">
                <i class="bi bi-plus-circle me-2"></i>Nueva patrulla
            </a>
        </div>

        <div class="row g-4">
            <?php if (!empty($patrullas)): ?>
                <?php foreach ($patrullas as $patrulla): ?>
                    <div class="col-12 col-md-6 col-lg-4">
                        <div class="patrulla-card">
                            <div class="patrulla-icono">
                                <i class="bi bi-shield-shaded"></i>
                            </div>

                            <div>
                                <h3><?= htmlspecialchars($patrulla['nombre']) ?></h3>

                                <p>
                                    <i class="bi bi-diagram-3 me-1"></i>
                                    Rama: <?= htmlspecialchars($patrulla['rama'] ?? 'Sin rama') ?>
                                </p>
                            </div>
                        </div>
                    </div>
                <?php endforeach; ?>
            <?php else: ?>
                <p>No hay patrullas cargadas.</p>
            <?php endif; ?>
        </div>

    </div>
</div>

<?php include 'includes/barra_inferior.php'; ?>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>