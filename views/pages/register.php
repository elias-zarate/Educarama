<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Educarama - Registro</title>

    <!-- Bootstrap 5 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css">
    <!-- Google Fonts: Ubuntu -->
    <link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@400;700&display=swap" rel="stylesheet">
    <!-- Estilos Personalizados -->
    <link rel="stylesheet" href="/Educarama/public/css/styles.css?v=1.5">
</head>stylesheet


<?php
$rama_page = "unidad"; // puede ser "manada", "unidad", "caminante", "rover"
?>

<body class="rama-<?php echo $rama_page; ?>">
    <?php 
    // Componentes fijos de la interfaz
    include 'includes/barra_superior.php'; 
    ?>

    <!-- CONTENEDOR MAESTRO -->
    <div class="main-content-wrapper without-sidebar">
        
        <div class="container contenedor-principal without-sidebar my-4 p-4">
            
            
            <!-- ENCABEZADO DEL PERFIL -->
            <div class="row mb-3 align-items-center barra-superior-inst mx-0">
                <div class=" align-items-center">
                    <h1 class="h3 fw-bold mb-0">Registro</h1>
                </div>
              
            </div>
            
            <!-- INFORMACIÓN BÁSICA -->
            <div class="row mb-4 align-items-stretch">
                <div class="col-md-12 mb-3 mb-md-0">


           <div class="tarjeta-blanca-borde p-3 h-100">
                <hr class="border-white opacity-25">
                
                <div id="mensajeAlerta"></div>
                    <form id="registroForm">
                        <div class="mb-3">
                            <label for="nombre" class="form-label">Nombre</label>
                            <input type="text" class="form-control" id="nombre" name="nombre" placeholder="Ingrese su nombre" required>
                        </div>
                        <div class="mb-3">
                            <label for="email" class="form-label">Correo Electrónico</label>
                            <input type="email" class="form-control" id="email" name="email" placeholder="nombre@ejemplo.com" required>
                        </div>
                        <div class="mb-3">
                            <label for="contrasenia" class="form-label">Contraseña</label>
                            <input type="password" class="form-control" id="contrasenia" name="password" placeholder="Ingrese su contraseña" required>
                        </div>
                        <button type="submit" class="btn btn-primary">Registrarse</button>
                    </form>
                 </div>
            </div>
            </div>
        </div>
    
    </div>
    <?php include 'includes/barra_inferior.php'; ?>
 <!-- Bootstrap 5 JS Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
    document.getElementById('registroForm').addEventListener('submit', function(e) {
    e.preventDefault(); // Evita que la página se recargue

    // Capturamos el formulario y el div de alertas
    const form = this;
    const formData = new FormData(form);
    const alertaDiv = document.getElementById('mensajeAlerta');

    // Mostramos estado de carga opcional (deshabilita el botón temporalmente)
    const btnSubmit = form.querySelector('button[type="submit"]');
    btnSubmit.disabled = true;
    btnSubmit.innerText = "Registrando...";

    // Hacemos la petición al backend
    fetch('/Educarama/src/Controladores/RegistroController.php', {
        method: 'POST',
        body: formData // FormData emula exactamente lo que haría un form tradicional enviando $_POST
    })
    .then(response => response.json())
    .then(data => {
        // Restauramos el botón
        btnSubmit.disabled = false;
        btnSubmit.innerText = "Registrarse";

        if (data.status === 'success') {
            // Mostrar mensaje de éxito
            alertaDiv.innerHTML = `<div class="alert alert-success d-flex align-items-center" role="alert">
                                      <i class="bi bi-check-circle-fill me-2"></i>
                                      <div>${data.message} Redirigiendo...</div>
                                   </div>`;
            
            // Redirigir al login después de 1.5 segundos
            setTimeout(() => {
            
                window.location.href = 'login.php'; 
            }, 1500);

        } else {
            // Mostrar mensaje de error
            alertaDiv.innerHTML = `<div class="alert alert-danger d-flex align-items-center" role="alert">
                                      <i class="bi bi-exclamation-triangle-fill me-2"></i>
                                      <div>${data.message}</div>
                                   </div>`;
        }
    })
    .catch(error => {
        btnSubmit.disabled = false;
        btnSubmit.innerText = "Registrarse";
        alertaDiv.innerHTML = `<div class="alert alert-danger">Error de conexión con el servidor.</div>`;
        console.error('Error:', error);
    });
});
</script>
</body>
</html>