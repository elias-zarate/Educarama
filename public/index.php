
<?php
//public/index.php
require_once __DIR__ . '/../src/Controladores/AuthMiddleware.php';

$tituloPagina = "Inicio";
require_once __DIR__ . '/../src/Controladores/eventoControlador.php';

// 1. Instancias el controlador
$eventoControlador = new eventoControlador();

// 2. Llamas al método y guardas el resultado en la variable que usa el HTML
$eventos = $eventoControlador->listareventosHome();
?>


<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Educarama - Inicio</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@400;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="/Educarama/public/css/styles.css?v=1.5">
</head>

<body>
    <?php
    include __DIR__ . '/../views/pages/includes/barra_superior.php';
    include __DIR__ . '/../views/pages/includes/menu_lateral.php';
    ?>

    <div class="main-content-wrapper">
        <div class="container contenedor-principal home-container my-4 p-4">

            <section class="home-welcome">
                <h2>Bienvenido a Educarama</h2>
                <p>Panel principal para visualizar eventos, actividades y novedades del grupo scout.</p>
            </section>

            <h3 class="home-section-title">📢 Eventos</h3>

            <section class="home-alerts">
                <ul>
                    <?php if (!empty($eventos)): ?>
                        <?php foreach ($eventos as $evento): ?>
                            <li>
                                <strong><?= htmlspecialchars($evento['titulo']) ?>:</strong>
                                <?= htmlspecialchars($evento['descripcion']) ?>
                            </li>
                        <?php endforeach; ?>
                    <?php else: ?>
                        <li>No hay eventos importantes pronto.</li>
                    <?php endif; ?>
                </ul>
            </section>

            <section class="home-stats">
                <a class="home-stat-card text-decoration-none" href="/Educarama/views/pages/protagonistas.php">
                    <div class="icon">👦</div>
                    <p>Protagonistas</p>
                </a>

                <a class="home-stat-card text-decoration-none" href="/Educarama/views/pages/patrullas.php">
                    <div class="icon">👥</div>
                    <p>Patrullas</p>
                </a>

                <a class="home-stat-card text-decoration-none" href="/Educarama/views/pages/ramas.php">
                    <div class="icon">🏕️</div>
                    <p>Ramas</p>
                </a>

                <div class="home-stat-card" >
                    <div class="icon">📅</div>
                    <p>Actividades</p>                
                </div>
            </section>

            <section class="home-grid">
                <div class="home-card">
                    <h3>📅 Próximas actividades</h3>

                    <table class="home-table">
                        <thead>
                            <tr>
                                <th>Fecha</th>
                                <th>Actividad</th>
                                <th>Rama</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>05/07</td>
                                <td>Reunión semanal</td>
                                <td>Lobatos</td>
                            </tr>
                            <tr>
                                <td>10/07</td>
                                <td>Fogón scout</td>
                                <td>Scouts</td>
                            </tr>
                            <tr>
                                <td>15/07</td>
                                <td>Campamento</td>
                                <td>Todo el grupo</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="home-card">
                    <h3>🎂 Cumpleaños</h3>

                    <ul class="birthday-list">
                        <li>Juan Pérez - 02/07</li>
                        <li>Lucía Gómez - 03/07</li>
                        <li>Mateo Ruiz - 06/07</li>
                    </ul>
                </div>
            </section>

            <section class="home-actions">
                <a href="/Educarama/views/pages/protagonistas.php" class="home-action-btn">Ver protagonistas</a>
                <a href="/Educarama/views/pages/alta_patrulla.php" class="home-action-btn">Nueva patrulla</a>
            </section>

        </div>
    </div>

    <?php include __DIR__ . '/../views/pages/includes/barra_inferior.php'; ?>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>