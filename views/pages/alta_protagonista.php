<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

$tituloPagina = "Nuevo Protagonista";

require_once __DIR__ . '/../../src/Controladores/ProtagonistaController.php';

$controlador = new ProtagonistaController();

$error = null;

$id = isset($_GET['id']) ? (int) $_GET['id'] : null;
$protagonista = null;

if ($id) {
    $protagonista = $controlador->obtenerPerfilPorId($id);

    if (!$protagonista) {
        header("Location: protagonistas.php");
        exit;
    }

    $tituloPagina = "Editar Protagonista";
}

try {
    $controlador->guardarSiEsPost();
} catch (Throwable $e) {
    $error = $e->getMessage();
}

$ramas = $controlador->listarRamas();
$patrullas = $controlador->listarPatrullas();

$rama_page = "unidad";

$esEdicion = !empty($protagonista);
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Educarama - <?= $esEdicion ? 'Editar Protagonista' : 'Nuevo Protagonista' ?></title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@400;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="/Educarama/public/css/styles.css?v=1.5">
</head>

<body class="rama-<?php echo $rama_page; ?>">
    <?php 
    include __DIR__ . '/includes/barra_superior.php'; 
    include __DIR__ . '/includes/menu_lateral.php'; 
    ?>

    <div class="main-content-wrapper">
        <div class="container contenedor-principal my-4 p-4">

            <div class="row mb-3 align-items-center barra-superior-inst mx-0">
                <div class="col-12">
                    <h1 class="h3 fw-bold mb-0">
                        <?= $esEdicion ? 'Editar Protagonista' : 'Nuevo Protagonista' ?>
                    </h1>

                    <p class="mb-0">
                        <?= $esEdicion 
                            ? 'Modificá los datos principales del beneficiario.' 
                            : 'Cargá los datos principales del beneficiario.' 
                        ?>
                    </p>
                </div>
            </div>

            <?php if (!empty($error)): ?>
                <div class="alert alert-danger">
                    <?= htmlspecialchars($error) ?>
                </div>
            <?php endif; ?>

            <div class="row mb-4 align-items-stretch">
                <div class="col-md-12 mb-3 mb-md-0">
                    <div class="tarjeta-blanca-borde p-4 h-100">

                        <form method="POST">
                            <input 
                                type="hidden" 
                                name="id" 
                                value="<?= $esEdicion ? htmlspecialchars($protagonista['id']) : '' ?>"
                            >
                            

                            <h2 class="h4 texto-violeta fw-bold mb-3">Datos personales</h2>

                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="apellido" class="form-label fw-bold">
                                        Apellido del Protagonista
                                    </label>

                                    <input 
                                        type="text" 
                                        class="form-control" 
                                        id="apellido" 
                                        name="apellido"
                                        placeholder="Ingrese el/los apellido/s del protagonista"
                                        value="<?= htmlspecialchars($protagonista['apellido'] ?? '') ?>"
                                        required
                                    >
                                </div>

                                <div class="col-md-6 mb-3">
                                    <label for="nombre" class="form-label fw-bold">
                                        Nombre del Protagonista
                                    </label>  

                                    <input 
                                        type="text" 
                                        class="form-control" 
                                        id="nombre" 
                                        name="nombre"
                                        placeholder="Ingrese el/los nombre/s del protagonista"
                                        value="<?= htmlspecialchars($protagonista['nombre'] ?? '') ?>"
                                        required
                                    >
                                </div>

                                <div class="col-md-4 mb-3">
                                    <label for="fecha_nacimiento" class="form-label fw-bold">
                                        Fecha de Nacimiento
                                    </label>

                                    <input 
                                        type="date" 
                                        class="form-control" 
                                        id="fecha_nacimiento"
                                        name="fecha_nacimiento"
                                        value="<?= htmlspecialchars($protagonista['fecha_nacimiento'] ?? '') ?>"
                                        required
                                    >
                                </div>

                                <div class="col-md-4 mb-3">
                                    <label for="dni" class="form-label fw-bold">DNI</label>

                                    <input 
                                        type="text" 
                                        class="form-control" 
                                        id="dni" 
                                        name="dni"
                                        placeholder="Ingrese el DNI del protagonista"
                                        value="<?= htmlspecialchars($protagonista['dni'] ?? '') ?>"
                                        required
                                    >
                                </div>

                                <div class="col-md-4 mb-3">
                                    <label for="anio_ingreso" class="form-label fw-bold">
                                        Año de Ingreso
                                    </label>

                                    <input 
                                        type="number" 
                                        class="form-control" 
                                        id="anio_ingreso" 
                                        name="anio_ingreso"
                                        placeholder="Ingrese el año de ingreso"
                                        value="<?= htmlspecialchars($protagonista['anio_ingreso'] ?? '') ?>"
                                    >
                                </div>
                            </div>

                            <hr>

                            <h2 class="h4 texto-violeta fw-bold mb-3">Datos scout</h2>

                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <label for="id_rama" class="form-label fw-bold">
                                        Rama
                                    </label>

                                    <select name="id_rama" id="id_rama" class="form-select" required>
                                        <option value="">Seleccionar rama</option>

                                        <?php foreach ($ramas as $rama): ?>
                                            <option 
                                                value="<?= $rama['id'] ?>"
                                                <?= isset($protagonista['id_rama']) && (int)$protagonista['id_rama'] === (int)$rama['id'] ? 'selected' : '' ?>
                                            >
                                                <?= htmlspecialchars($rama['nombre']) ?>
                                            </option>
                                        <?php endforeach; ?>
                                    </select>
                                </div>

                                <div class="col-md-4 mb-3">
                                    <label for="id_patrulla" class="form-label fw-bold">
                                        Patrulla
                                    </label>

                                    <select name="id_patrulla" id="id_patrulla" class="form-select">
                                        <option value="">Sin patrulla</option>

                                        <?php foreach ($patrullas as $patrulla): ?>
                                            <option 
                                                value="<?= $patrulla['id'] ?>"
                                                <?= isset($protagonista['id_patrulla']) && (int)$protagonista['id_patrulla'] === (int)$patrulla['id'] ? 'selected' : '' ?>
                                            >
                                                <?= htmlspecialchars($patrulla['nombre']) ?>
                                            </option>
                                        <?php endforeach; ?>
                                    </select>
                                </div>

                                <div class="col-md-4 mb-3 d-flex align-items-end">
                                    <div class="form-check">
                                        <input 
                                            class="form-check-input" 
                                            type="checkbox" 
                                            name="promesa" 
                                            id="promesa"
                                            <?= !empty($protagonista['promesa']) ? 'checked' : '' ?>
                                        >

                                        <label class="form-check-label fw-bold" for="promesa">
                                            Tiene promesa scout
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <div class="d-flex gap-2 mt-3">
                                <button type="submit" class="boton-primario px-4 py-2 fw-bold">
                                    <i class="bi bi-save me-2"></i>
                                    <?= $esEdicion ? 'Guardar cambios' : 'Registrar' ?>
                                </button>

                                <?php if ($esEdicion): ?>
                                    <a 
                                        href="/Educarama/views/pages/perfil_protagonista.php?id=<?= $protagonista['id'] ?>" 
                                        class="btn btn-secondary px-4 py-2"
                                    >
                                        Cancelar
                                    </a>
                                <?php else: ?>
                                    <a href="/Educarama/views/pages/protagonistas.php" class="btn btn-secondary px-4 py-2">
                                        Cancelar
                                    </a>
                                <?php endif; ?>
                            </div>

                        </form>

                    </div>
                </div>
            </div>

        </div>
    </div>

    <?php include __DIR__ . '/includes/barra_inferior.php'; ?>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>