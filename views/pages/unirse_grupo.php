<?php
session_start();
require_once __DIR__ . '/../Controladores/AuthMiddleware.php';
require_once __DIR__ . '/../../src/Datos/Database.php';
require_once __DIR__ . '/../../src/Datos/Gruporepository.php';

$error = null;
$mensajeExito = null;
$usuarioId = $_SESSION['usuario_id'] ?? null;

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $codigoGrupo = trim($_POST['codigo_grupo'] ?? '');

    if ($codigoGrupo === '') {
        $error = "El código del grupo es obligatorio.";
    } else {
        try {
            $conexion = Database::getConexion();
            $grupoRepo = new GrupoRepository();
            $grupo = $grupoRepo->buscarPorCodigo($codigoGrupo);

            if (!$grupo) {
                $error = "El código de grupo ingresado no es válido o no existe.";
            } else {
                // Registramos la relación en asignacion_rol con el rol por defecto (ej: educador = 2 o según convenga) y el grupo
                $stmt = $conexion->prepare("
                    INSERT INTO asignacion_rol (id_usuario, id_rol, id_grupo, id_rama)
                    VALUES (:id_usuario, 1, :id_grupo, NULL)
                ");
                $stmt->execute([
                    ':id_usuario' => $usuarioId,
                    ':id_grupo' => $grupo['id']
                ]);

                // Redirección automática al panel principal (index.php) tras guardar
                header("Location: /Educarama/public/index.php?vinculacion=exitosa");
                exit;
            }
        } catch (Throwable $e) {
            $error = "Error al vincular el grupo: " . $e->getMessage();
        }
    }
}
?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Educarama - Unirse a Grupo Scout</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@400;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="/Educarama/public/css/styles.css?v=1.5">
</head>
<body>
    <?php 
    include __DIR__ . '/includes/barra_superior.php'; 
    ?>
    <div class="main-content-wrapper without-sidebar d-flex align-items-center justify-content-center" style="min-height: calc(100vh - 122px);">
        <div class="container" style="max-width: 500px;">
            <div class="tarjeta-blanca-borde p-4 shadow-lg">
                <div class="text-center mb-4">
                    <i class="bi bi-diagram-3-fill text-violeta" style="font-size: 3rem;"></i>
                    <h2 class="fw-bold mt-2 text-violeta">Vincular a Grupo Scout</h2>
                    <p class="text-muted">Ingresá el código alfanumérico institucional para asociar tu cuenta a un Grupo Scout.</p>
                </div>

                <?php if (!empty($error)): ?>
                    <div class="alert alert-danger alert-dismissible fade show shadow-sm" role="alert">
                        <?= htmlspecialchars($error) ?>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                <?php endif; ?>

                <?php if (!empty($mensajeExito)): ?>
                    <div class="alert alert-success alert-dismissible fade show shadow-sm" role="alert">
                        <?= htmlspecialchars($mensajeExito) ?>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                <?php endif; ?>

                <form method="POST" action="">
                    <input type="hidden" name="id_usuario" value="<?= htmlspecialchars($usuarioId) ?>">
                    
                    <div class="mb-4">
                        <label for="codigoGrupo" class="form-label fw-bold">Código del Grupo Scout</label>
                        <input type="text" class="form-control form-control-lg text-center fw-bold" id="codigoGrupo" name="codigo_grupo" placeholder="Ej: GRP-001" required style="letter-spacing: 2px; text-transform: uppercase;">
                    </div>

                    <button type="submit" class="boton-primario w-100 py-2 fs-5 fw-bold">
                        Vincular Grupo
                    </button>
                </form>

                <div class="text-center mt-4">
                    <a href="/Educarama/public/index.php" class="text-secondary text-decoration-none small">Volver al inicio</a>
                </div>
            </div>
        </div>
    </div>
    <?php include __DIR__ . '/includes/barra_inferior.php'; ?>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>