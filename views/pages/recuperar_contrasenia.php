<?php
require_once __DIR__ . '/../../src/Datos/Database.php';
require_once __DIR__ . '/../../src/Datos/UsuarioRepository.php';
require_once __DIR__ . '/../../src/Servicios/LoginService.php';

$error = null;
$mensajeExito = null;
$paso = 1; 
$emailGuardado = ''; 

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $accion = $_POST['accion'] ?? '';
    
    try {
        $conexion = Database::getConexion();
        $usuarioRepo = new UsuarioRepository($conexion);
        $loginService = new LoginService($usuarioRepo);

        if ($accion === 'solicitar_codigo') {
            $resultado = $loginService->solicitarRecuperacion($_POST);

            if ($resultado['status'] === 'success') {
                $mensajeExito = $resultado['message'];
                $emailGuardado = $resultado['email'];
                $paso = 2; 
            } else {
                $error = $resultado['message'];
            }
        } elseif ($accion === 'restablecer_contrasenia') {
            $resultado = $loginService->restablecerContrasenia($_POST);

            if ($resultado['status'] === 'success') {
                header("Location: login.php?exito=contrasenia_actualizada");
                exit;
            } else {
                $error = $resultado['message'];
                $paso = 2; 
                $emailGuardado = $_POST['email'] ?? '';
            }
        }
    } catch (Throwable $e) {
        $error = "Error en el servidor: " . $e->getMessage();
    }
}
?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Educarama - Recuperar Contraseña</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@400;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="/Educarama/public/css/styles.css">
</head>
<body class="bg-light no-sidebar">
    <div class="header-inst">
        <div class="d-flex align-items-center gap-2">
            <img class="img-fluid rounded-circle" src="../assets/images/Logo1.png" alt="Logo Educarama" style="width: 40px; height: 40px;">
            <h1 class="m-0 fs-4 texto-blanco fw-bold">Educarama</h1>
        </div>
    </div>

    <div class="main-content-wrapper without-sidebar d-flex align-items-center justify-content-center" style="min-height: calc(100vh - 122px);">
        <div class="container" style="max-width: 500px;">
            <div class="tarjeta-blanca-borde p-4 shadow-lg">
                <div class="text-center mb-4">
                    <i class="bi bi-shield-lock-fill text-violeta" style="font-size: 3rem;"></i>
                    <h2 class="fw-bold mt-2 text-violeta">Recuperar Contraseña</h2>
                    <p class="text-muted">
                        <?= $paso === 1 
                            ? 'Ingresá tu correo electrónico y te enviaremos un código para restablecer tu contraseña.' 
                            : 'Ingresá el código que recibiste y tu nueva contraseña.' ?>
                    </p>
                </div>
                
                <div id="mensajeAlerta">
                    <?php if (!empty($error)): ?>
                        <div class="alert alert-danger alert-dismissible fade show shadow-sm" role="alert">
                            <?= htmlspecialchars($error) ?>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>
                    <?php endif; ?>

                    <?php if (!empty($mensajeExito)): ?>
                        <div class="alert alert-success alert-dismissible fade show shadow-sm" role="alert">
                            <?= $mensajeExito ?>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>
                    <?php endif; ?>
                </div>

                <?php if ($paso === 1): ?>
                <div>
                    <form method="POST">
                        <input type="hidden" name="accion" value="solicitar_codigo">
                        <div class="mb-4">
                            <label for="emailRecuperacion" class="form-label fw-bold">Correo Electrónico</label>
                            <input type="email" class="form-control form-control-lg" id="emailRecuperacion" name="email" placeholder="nombre@ejemplo.com" required>
                        </div>
                        <button type="submit" class="boton-primario w-100 py-2 fs-5 fw-bold">
                            Enviar Código
                        </button>
                    </form>
                </div>
                <?php endif; ?>

                <?php if ($paso === 2): ?>
                <div>
                    <form method="POST">
                        <input type="hidden" name="accion" value="restablecer_contrasenia">
                        <input type="hidden" name="email" value="<?= htmlspecialchars($emailGuardado) ?>">
                        
                        <div class="mb-3">
                            <label for="tokenRecuperacion" class="form-label fw-bold">Código de Recuperación</label>
                            <input type="text" class="form-control form-control-lg text-center fw-bold" id="tokenRecuperacion" name="token" placeholder="Ej: A1B2C3" required style="letter-spacing: 2px; text-transform: uppercase;">
                        </div>
                        <div class="mb-4">
                            <label for="nuevaContrasenia" class="form-label fw-bold">Nueva Contraseña</label>
                            <input type="password" class="form-control form-control-lg" id="nuevaContrasenia" name="nueva_contrasenia" placeholder="Mínimo 6 caracteres" required minlength="6">
                        </div>
                        <button type="submit" class="boton-primario w-100 py-2 fs-5 fw-bold">
                            Guardar Nueva Contraseña
                        </button>
                    </form>
                </div>
                <?php endif; ?>

                <div class="text-center mt-4">
                    <a href="login.php" class="text-secondary text-decoration-none small">Volver al inicio de sesión</a>
                </div>
            </div>
        </div>
    </div>

    <div class="footer-inst">
        <p class="m-0 texto-blanco small">© 2026 Plataforma Educarama - Scouts de Argentina</p>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>