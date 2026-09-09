<?php
require_once __DIR__ . '/../../src/Controladores/AuthMiddleware.php';
?>

<?php
$tituloPagina = "Nueva Patrulla";
require_once __DIR__ . '/../../src/Controladores/PatrullaController.php';

$controlador = new PatrullaController();

$error = null;

try {
    $controlador->crearSiEsPost();
} catch (Exception $e) {
    $error = $e->getMessage();
}

$ramas = $controlador->listarRamas();
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Educarama - Nueva Patrulla</title>

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

        <div class="barra-superior-inst mb-4">
            <h1 class="fw-bold mb-1">Nueva patrulla</h1>
            <p class="mb-0">Cargá una patrulla o equipo y asocialo a una rama.</p>
        </div>

        <?php if (!empty($error)): ?>
            <div class="alert alert-danger">
                <?= htmlspecialchars($error) ?>
            </div>
        <?php endif; ?>

        <form method="POST" class="tarjeta-blanca-borde p-4">

      

            <div class="mb-3">
                <label class="form-label fw-bold">Rama</label>
                <select name="id_rama" class="form-select" required>
                    <option value="">Seleccionar rama</option>
                    <?php foreach ($ramas as $rama): ?>
                        <option value="<?= $rama['id'] ?>">
                            <?= htmlspecialchars($rama['nombre']) ?>
                        </option>
                    <?php endforeach; ?>
                </select>
            </div>

                  <div class="mb-3">
                <label class="form-label fw-bold">Nombre de la patrulla</label>
                <input 
                    type="text" 
                    name="nombre" 
                    class="form-control" 
                    placeholder="Ej: Halcones, Pumas, Seisena Roja..."
                    required
                >
            </div>

            <div class="d-flex gap-2">
                <button type="submit" class="boton-primario px-4 py-2 fw-bold">
                    <i class="bi bi-save me-2"></i>Guardar patrulla
                </button>

                <a href="/Educarama/views/pages/patrullas.php" class="btn btn-secondary px-4 py-2">
                    Cancelar
                </a>
            </div>

        </form>

    </div>
</div>

<?php include __DIR__ . '/includes/barra_inferior.php'; ?>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>