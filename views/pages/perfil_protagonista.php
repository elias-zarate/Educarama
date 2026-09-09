<?php
$tituloPagina = "Perfil de Protagonista";

require_once __DIR__ . '/../../src/Controladores/ProtagonistaController.php';

$controlador = new ProtagonistaController();
$protagonista = $controlador->obtenerPerfilDesdeUrl();

if (!$protagonista) {
    header("Location: protagonistas.php");
    exit;
}

$fechaNacimiento = new DateTime($protagonista['fecha_nacimiento']);
$hoy = new DateTime();
$edad = $hoy->diff($fechaNacimiento)->y;

$rama_page = "unidad";
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Educarama - Perfil de Protagonista</title>

    <!-- Bootstrap 5 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css">
    <!-- Google Fonts: Ubuntu -->
    <link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@400;700&display=swap" rel="stylesheet">
    <!-- Estilos Personalizados -->
    <link rel="stylesheet" href="/Educarama/public/css/styles.css?v=1.5">
</head>


<body class="rama-<?php echo $rama_page; ?>">    <?php 
    // Componentes fijos de la interfaz
    include __DIR__ . '/includes/barra_superior.php'; 
    include __DIR__ . '/includes/menu_lateral.php'; 
    ?>

    <!-- CONTENEDOR MAESTRO -->
    <div class="main-content-wrapper">
        
        <div class="container contenedor-principal my-4 p-4">
            <?php if (isset($_GET['actualizado']) && $_GET['actualizado'] == 1): ?>
    <div class="alert alert-success alert-dismissible fade show mb-4" role="alert">
        <i class="bi bi-check-circle-fill me-2"></i>
        Los datos del protagonista se modificaron correctamente.

        <button 
            type="button" 
            class="btn-close" 
            data-bs-dismiss="alert" 
            aria-label="Cerrar">
        </button>
    </div>
<?php endif; ?>
            
            <!-- ENCABEZADO DEL PERFIL -->
<div class="row mb-3 align-items-center barra-superior-inst mx-0">
    <div class="col-8">
        <h1 class="h3 fw-bold mb-0">
            <?= htmlspecialchars($protagonista['nombre']) ?>
            <?= htmlspecialchars($protagonista['apellido']) ?>,
            <?= $edad ?> años
        </h1>

        <p class="mb-0 fw-bold">
            <?= htmlspecialchars($protagonista['rama'] ?? 'Sin rama asignada') ?>
        </p>
    </div>

    <div class="col-4 d-flex justify-content-end">
        <div class="circulo-logo">
            <img 
                class="img-fluid rounded-circle" 
                src="../assets/images/RamaLogoLyL.png" 
                alt="Logo rama"
            >
        </div>
    </div>

    
</div>

<!-- INFORMACIÓN BÁSICA -->
<div class="row mb-4 align-items-stretch">
    <div class="col-md-12 mb-3 mb-md-0">
        <div class="tarjeta-blanca-borde p-3 h-100">
            <p class="m-0 fw-bold">
                <i class="bi bi-calendar-event me-2"></i>
                Fecha de Nacimiento:
                <?= date("d/m/Y", strtotime($protagonista['fecha_nacimiento'])) ?>
            </p>

            <p class="m-0 fw-bold">
                <i class="bi bi-diagram-3 me-2"></i>
                Rama: <?= htmlspecialchars($protagonista['rama'] ?? 'Sin rama') ?>
            </p>

            <p class="m-0 fw-bold">
                <i class="bi bi-shield-shaded me-2"></i>
                Patrulla: <?= htmlspecialchars($protagonista['patrulla'] ?? 'Sin patrulla') ?>
            </p>

            <p class="m-0 fw-bold">
                <i class="bi bi-calendar-check me-2"></i>
                Año de ingreso:
                <?= htmlspecialchars($protagonista['anio_ingreso'] ?? 'Sin dato') ?>
            </p>

            <p class="m-0 fw-bold">
                <i class="bi bi-house-heart me-2"></i>
                Información familiar
            </p>

            <hr class="border-white opacity-25">

            <p class="m-0 small">
                Notas adicionales: El beneficiario presenta gran interés en actividades al aire libre.
            </p>
        </div>
    </div>
</div>

            <!-- SECCIÓN DE PROGRESIÓN -->
            <div class="row g-3 mb-4">
                <div class="col-12">
                    <h2 class="h4 texto-violeta fw-bold">Progresión Personal</h2>
                </div>

                <div class="col-md-6">
                    <div class="  h-100 d-flex flex-column align-items-center justify-content-center">
                        <div class="pieza-insignia  h50 d-flex flex-wrap "> 
                            <div class="d-grid h50" style="grid-template-columns: repeat(2, 1fr);">
                            <img class="img-fluid" src="../assets/images/insignias/Progresion 1 Rama Lobatos y Lobeznas_Lobo Pata Tierna.png" alt="Insignia 1">
                            <img class="img-fluid" src="../assets/images/insignias/Progresion 2 Rama Lobatos y Lobeznas_Lobo Saltador.png" alt="Insignia 2">
                            <img class="img-fluid" src="../assets/images/insignias/Progresion 4 Rama Lobatos y Lobeznas_Lobo Cazador.png" alt="Insignia 4">
                            <img class="img-fluid" src="../assets/images/insignias/Progresion 3 Rama Lobatos y Lobeznas_Lobo Rastreador.png" alt="Insignia 3">
                            </div>
                        </div>            
                    </div>
                </div>

                <div class="col-md-6 justify-content-center d-flex flex-column">
                    <div class="d-flex flex-column align-items-center justify-content-center">
                        <div class="barra-progreso w-100 mb-2"> 
                            <div class="progreso-actual" style="width: 60%;"></div>
                        </div>
                        <p class="texto-violeta fw-bold m-0">Competencias: 6 / 10</p>
                    </div>
                    
                    <h2 class="h4 texto-violeta fw-bold">Promesa Scout</h2>

                    <div class="tarjeta-blanca-borde p-2 d-flex flex-row align-items-center justify-content-around">
                        <div class="insignia-pequena">
                            <img src="../assets/images/FlorLizNacional.png" alt="Insignia Nacional">
                        </div>
                        <div class="insignia-pequena">
                            <img src="../assets/images/FlorLizInternacional.jpg" alt="Insignia Internacional">
                        </div>
                    </div>
                </div>


            </div>

            <!-- BOTONES DE ACCIÓN -->
            <div class="row g-3">
                <div class="col-12 col-md-4">
                    <button class="boton-primario w-100 py-3 fw-bold"><i class="bi bi-pencil-square me-2"></i>Registrar Actividad</button>
                </div>
                <div class="col-12 col-md-4">
                    <button class="boton-primario w-100 py-3 fw-bold"><i class="bi bi-clock-history me-2"></i>Ver Historial</button>
                </div>
                <div class="col-12 col-md-4">
                    <a 
                        href="alta_protagonista.php?id=<?= $protagonista['id'] ?>" 
                        class="boton-primario w-100 py-3 fw-bold text-decoration-none text-center d-block">
                        <i class="bi bi-person-gear me-2"></i>Editar Perfil
                        </a>
                </div>
            </div>
            
        </div>
    </div>

    <?php include __DIR__ . '/includes/barra_inferior.php'; ?>

    <!-- Bootstrap 5 JS Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

<script>
    setTimeout(() => {
        const alerta = document.querySelector('.alert-success');

        if (alerta) {
            const alertaBootstrap = bootstrap.Alert.getOrCreateInstance(alerta);
            alertaBootstrap.close();
        }
    }, 3000);
</script>

</body>
</body>
</html>