<?php
require_once __DIR__ . '/../../src/Controladores/AuthMiddleware.php';
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

require_once __DIR__ . '/../../src/Controladores/RamaController.php';
require_once __DIR__ . '/../../src/Datos/ProgresionRepository.php';
require_once __DIR__ . '/../../src/Servicios/ProgresionService.php';

// --- NUEVO: REPOSITORIO DE DOCUMENTOS ---
require_once __DIR__ . '/../../src/Datos/DocumentoRepository.php';

$progRepo = new ProgresionRepository();
$progService = new ProgresionService($progRepo);
$docRepo = new DocumentoRepository();

// Determinamos la clase CSS de la rama
$rama_page = strtolower($protagonista['rama'] ?? 'unidad');

// Mapeo dinámico de rama a ID de categoría
$idCategoriaRama = 2; // Default a Unidad/Scouts
if (strpos($rama_page, 'manada') !== false || strpos($rama_page, 'lobato') !== false) {
    $idCategoriaRama = 1;
} elseif (strpos($rama_page, 'caminante') !== false) {
    $idCategoriaRama = 3;
} elseif (strpos($rama_page, 'rover') !== false) {
    $idCategoriaRama = 4;
}

// Obtenemos alertas y documentos
$alertas = $progService->obtenerAlertas($protagonista['id'], $idCategoriaRama);
$documentos = $docRepo->obtenerDocumentosPorProtagonista($protagonista['id']);
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
<body class="rama-<?php echo $rama_page; ?>">

    <?php 
    include __DIR__ . '/includes/barra_superior.php'; 
    include __DIR__ . '/includes/menu_lateral.php'; 
    ?>

    <!-- CONTENEDOR MAESTRO -->
    <div class="main-content-wrapper">
        
        <div class="container contenedor-principal my-4 p-4">
            
            <!-- MENSAJES DE ÉXITO -->
            <?php if (isset($_GET['actualizado']) && $_GET['actualizado'] == 1): ?>
                <div class="alert alert-success alert-dismissible fade show mb-4" role="alert">
                    <i class="bi bi-check-circle-fill me-2"></i>
                    Los datos del protagonista se modificaron correctamente.
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Cerrar"></button>
                </div>
            <?php endif; ?>

            <!-- ALERTAS DE PROGRESIÓN DINÁMICAS -->
            <?php if (!empty($alertas)): ?>
                <?php foreach ($alertas as $alerta): ?>
                    <div class="alert alert-warning alert-dismissible fade show mb-4 d-flex align-items-center shadow-sm" role="alert">
                        <i class="bi bi-exclamation-triangle-fill fs-4 me-3 text-warning"></i>
                        <div>
                            <strong class="d-block"><?= htmlspecialchars($alerta['titulo']) ?></strong>
                            <?= htmlspecialchars($alerta['mensaje']) ?>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Cerrar"></button>
                    </div>
                <?php endforeach; ?>
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
                        <img class="img-fluid rounded-circle" src="../assets/images/RamaLogoLyL.png" alt="Logo rama">
                    </div>
                </div>
            </div>

            <!-- INFORMACIÓN BÁSICA -->
            <div class="row mb-4 align-items-stretch">
                <div class="col-md-12 mb-3 mb-md-0">
                    <div class="tarjeta-blanca-borde p-3 h-100">
                        <p class="m-0 fw-bold">
                            <i class="bi bi-calendar-event me-2"></i>
                            Fecha de Nacimiento: <?= date("d/m/Y", strtotime($protagonista['fecha_nacimiento'])) ?>
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
                            Año de ingreso: <?= htmlspecialchars($protagonista['anio_ingreso'] ?? 'Sin dato') ?>
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
                    <div class="h-100 d-flex flex-column align-items-center justify-content-center">
                        <div class="pieza-insignia h50 d-flex flex-wrap">
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
                    
                    <h2 class="h4 texto-violeta fw-bold mt-4">Promesa Scout</h2>
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

            <!-- SECCIÓN DE DOCUMENTACIÓN -->
            <div class="row g-3 mb-4">
                <div class="col-12">
                    <h2 class="h4 texto-violeta fw-bold">Documentación Adjunta</h2>
                </div>
                <div class="col-12">
                    <div class="tarjeta-blanca-borde p-3">
                        <?php if (empty($documentos)): ?>
                            <p class="text-muted m-0"><i class="bi bi-info-circle me-2"></i>No hay documentos adjuntos para este protagonista.</p>
                        <?php else: ?>
                            <div class="list-group">
                                <?php foreach ($documentos as $doc): ?>
                                    <a href="/Educarama<?= htmlspecialchars($doc['ruta_archivo']) ?>" target="_blank" class="list-group-item list-group-item-action d-flex justify-content-between align-items-center">
                                        <div>
                                            <i class="bi bi-file-earmark-text text-primary me-2"></i>
                                            <?= htmlspecialchars($doc['nombre_original']) ?>
                                            <br>
                                            <small class="text-muted">Subido el <?= date('d/m/Y', strtotime($doc['fecha_subida'])) ?></small>
                                        </div>
                                        <i class="bi bi-box-arrow-up-right text-muted"></i>
                                    </a>
                                <?php endforeach; ?>
                            </div>
                        <?php endif; ?>
                    </div>
                </div>
            </div>

            <!-- BOTONES DE ACCIÓN -->
            <div class="row g-3">
                <div class="col-12 col-md-4">
                    <a href="alta_protagonista.php?id=<?= $protagonista['id'] ?>" class="boton-primario w-100 py-3 fw-bold text-decoration-none text-center d-block">
                        <i class="bi bi-person-gear me-2"></i>Editar Perfil
                    </a>
                </div>
                <div class="col-12 col-md-4">
                    <button type="button" id="btnRecomendarInsignia" class="boton-primario w-100 py-3 fw-bold d-block">
                        <i class="bi bi-award me-2"></i>Evaluar Insignia
                    </button>
                </div>
            </div>
            
        </div>
    </div>

    <!-- Modal Recomendación de Insignia -->
    <div class="modal fade" id="modalRecomendacion" tabindex="-1" aria-labelledby="modalRecomendacionLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content tarjeta-blanca-borde">
          <div class="modal-header border-0 pb-0">
            <h5 class="modal-title texto-violeta fw-bold" id="modalRecomendacionLabel">Recomendación de Progresión</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body text-center">
            <div id="loaderRecomendacion" class="spinner-border text-primary my-3" role="status" style="display: none;"></div>
            <div id="contenidoRecomendacion">
                <h3 id="porcentajeAvance" class="display-4 fw-bold texto-violeta mb-0"></h3>
                <p class="text-muted fw-bold">Competencias logradas: <span id="textoCompetencias"></span></p>
                <div class="alert alert-info mt-3 fw-bold" id="mensajeRecomendacion"></div>
            </div>
          </div>
          <div class="modal-footer border-0 pt-0 justify-content-center">
            <button type="button" class="btn btn-secondary px-4" data-bs-dismiss="modal">Cerrar</button>
            <button type="button" class="boton-primario px-4" id="btnOtorgarInsigniaBtn" style="display: none;">Otorgar Insignia</button>
          </div>
        </div>
      </div>
    </div>

    <?php include __DIR__ . '/includes/barra_inferior.php'; ?>
    
    <!-- Bootstrap 5 JS Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            setTimeout(() => {
                const alerta = document.querySelector('.alert-success');
                if (alerta) {
                    const alertaBootstrap = bootstrap.Alert.getOrCreateInstance(alerta);
                    alertaBootstrap.close();
                }
            }, 3000);

            const idProtagonista = <?= json_encode($protagonista['id']) ?>;
            const idCategoriaRama = <?= $idCategoriaRama ?>; 

            // --- MANEJO DE CHECKBOXES DE ACCIONES ---
            const checkboxesAccion = document.querySelectorAll('.checkbox-accion');
            checkboxesAccion.forEach(checkbox => {
                checkbox.addEventListener('change', function () {
                    const idAccion = this.dataset.idAccion;
                    const estado = this.checked ? 1 : 0;

                    fetch('/Educarama/src/Controladores/ProgresionController.php', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({
                            action: 'marcar_accion',
                            id_protagonista: idProtagonista,
                            id_accion: idAccion,
                            estado: estado
                        })
                    })
                    .then(response => response.json())
                    .then(data => {
                        if (data.status !== 'success') {
                            alert('Error al guardar la acción: ' + data.message);
                            this.checked = !this.checked; 
                        }
                    })
                    .catch(error => {
                        console.error('Error de Fetch:', error);
                        alert('Error de conexión con el servidor.');
                        this.checked = !this.checked;
                    });
                });
            });

            // --- MANEJO DE CHECKBOXES DE COMPETENCIAS ---
            const checkboxesCompetencia = document.querySelectorAll('.checkbox-competencia');
            checkboxesCompetencia.forEach(checkbox => {
                checkbox.addEventListener('change', function () {
                    const idCompetencia = this.dataset.idCompetencia;
                    const codigoCompetencia = this.dataset.codigoCompetencia;
                    const estado = this.checked ? 1 : 0;

                    fetch('/Educarama/src/Controladores/ProgresionController.php', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({
                            action: 'marcar_competencia',
                            id_protagonista: idProtagonista,
                            id_competencia: idCompetencia,
                            codigo_competencia: codigoCompetencia,
                            estado: estado
                        })
                    })
                    .then(response => response.json())
                    .then(data => {
                        if (data.status !== 'success') {
                            alert('Error al guardar la competencia: ' + data.message);
                            this.checked = !this.checked;
                        } else {
                            location.reload(); 
                        }
                    })
                    .catch(error => {
                        console.error('Error de Fetch:', error);
                        alert('Error de conexión con el servidor.');
                        this.checked = !this.checked;
                    });
                });
            });

            // --- LÓGICA PARA RECOMENDACIÓN DE INSIGNIA ---
            const btnRecomendar = document.getElementById('btnRecomendarInsignia');
            const modalElement = document.getElementById('modalRecomendacion');
            
            if (btnRecomendar && modalElement) {
                const modalRecomendacion = new bootstrap.Modal(modalElement);
                
                btnRecomendar.addEventListener('click', function() {
                    document.getElementById('contenidoRecomendacion').style.display = 'none';
                    document.getElementById('loaderRecomendacion').style.display = 'inline-block';
                    document.getElementById('btnOtorgarInsigniaBtn').style.display = 'none';
                    modalRecomendacion.show();

                    fetch('/Educarama/src/Controladores/ProgresionController.php', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({
                            action: 'evaluar_recomendacion',
                            id_protagonista: idProtagonista,
                            id_categoria_rama: idCategoriaRama
                        })
                    })
                    .then(response => response.json())
                    .then(data => {
                        document.getElementById('loaderRecomendacion').style.display = 'none';
                        document.getElementById('contenidoRecomendacion').style.display = 'block';

                        if (data.status === 'success') {
                            const info = data.data;
                            document.getElementById('porcentajeAvance').innerText = info.porcentaje + '%';
                            document.getElementById('textoCompetencias').innerText = info.desarrolladas + ' de ' + info.total;
                            document.getElementById('mensajeRecomendacion').innerText = info.mensaje;

                            if (info.recomendacion_id) {
                                const btnOtorgar = document.getElementById('btnOtorgarInsigniaBtn');
                                btnOtorgar.style.display = 'inline-block';
                                
                                btnOtorgar.onclick = function() {
                                    otorgarInsignia(info.recomendacion_id);
                                };
                            }
                        } else {
                            document.getElementById('mensajeRecomendacion').innerText = 'Error: ' + data.message;
                        }
                    })
                    .catch(error => {
                        console.error('Error:', error);
                        document.getElementById('loaderRecomendacion').style.display = 'none';
                        document.getElementById('mensajeRecomendacion').innerText = 'Hubo un error de conexión con el servidor.';
                        document.getElementById('contenidoRecomendacion').style.display = 'block';
                    });
                });
            }

            function otorgarInsignia(idInsignia) {
                fetch('/Educarama/src/Controladores/ProgresionController.php', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        action: 'otorgar_insignia',
                        id_protagonista: idProtagonista,
                        id_insignia: idInsignia
                    })
                })
                .then(response => response.json())
                .then(data => {
                    if (data.status === 'success') {
                        alert(data.message);
                        location.reload(); 
                    } else {
                        alert('Error al otorgar insignia: ' + data.message);
                    }
                });
            }
        });
    </script>
</body>
</html>