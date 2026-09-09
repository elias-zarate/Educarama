<?php
// Configuración de la página
$rama_page = ""; // "manada", "unidad", "caminante", "rover"
?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Educarama - Iniciar Sesión</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@400;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="/Educarama/public/css/styles.css?v=1.5">
</head>

<body class="rama-<?php echo $rama_page; ?>">
    <?php 
    // Componentes fijos de la interfaz
    include __DIR__ . '/includes/barra_superior.php'; 
    ?>

    <div class="main-content-wrapper without-sidebar">
        
        <div class="container contenedor-principal without-sidebar my-4 p-4">
            
            <div class="row mb-3 align-items-center barra-superior-inst mx-0">
                <div class="align-items-center">
                    <h1 class="h3 fw-bold mb-0">Iniciar Sesión</h1>
                </div>
            </div>

            <div class="row mb-4 align-items-stretch">
                <div class="col-md-12 mb-3 mb-md-0">
                    <div class="tarjeta-blanca-borde p-3 h-100">
                        <hr class="border-white opacity-25">

                        <form id="formLogin" method="POST">
                            <div class="mb-3">
                                <label for="email" class="form-label">Correo Electrónico</label>
                                <input type="email" class="form-control" id="email" name="email" placeholder="nombre@ejemplo.com" required>
                            </div>
                            <div class="mb-3">
                                <label for="password" class="form-label">Contraseña</label>
                                <input type="password" class="form-control" id="password" name="password" placeholder="Ingrese su contraseña" required>
                            </div>
                            <button type="submit" class="btn btn-primary">Iniciar Sesión</button>
                            <a href="register.php" class="btn btn-secondary">Registrarse</a>                        </form>

                    </div>
                </div>
            </div>

        </div>
    </div>

    <?php include __DIR__ . '/includes/barra_inferior.php'; ?>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    
    <script>
document.addEventListener('DOMContentLoaded', function () {
    const formulario = document.querySelector('#formLogin');

    if (!formulario) {
        console.error('No se encontró el formulario con id="formLogin"');
        return;
    }

    formulario.addEventListener('submit', function (e) {
        e.preventDefault();

        const formData = new FormData(formulario);

        fetch('/Educarama/src/Controladores/AuthController.php', {
            method: 'POST',
            body: formData
        })
        .then(response => response.text())
        .then(texto => {
            console.log('Respuesta del servidor:', texto);

            let data;

            try {
                data = JSON.parse(texto);
            } catch (error) {
                alert('El servidor respondió algo que no es JSON. Revisá la consola.');
                console.error(texto);
                return;
            }

            if (data.status === 'success') {
                window.location.href = '/Educarama/public/index.php';
            } else {
                alert(data.message || 'No se pudo iniciar sesión.');
            }
        })
        .catch(error => {
            console.error(error);
            alert('Ocurrió un error al intentar comunicarse con el servidor.');
        });
    });
});
</script>
</body>
</html>