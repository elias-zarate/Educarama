<?php
// views/includes/nav_protagonista.php
//
// Barra de navegación entre las tres vistas del protagonista: Perfil,
// Competencias y Etapas e insignias. Las tres páginas la incluyen pasando
// $idProtagonista, $nombreProtagonista, $idRama y $paginaActual
// ('perfil', 'competencias' o 'etapas').

declare(strict_types=1);

$_navItems = [
    'perfil'       => ['url' => 'views/pages/perfil_protagonista.php?id=', 'icono' => 'bi-person',      'texto' => 'Perfil'],
    'competencias' => ['url' => 'views/pages/gestionar_competencias.php?id=', 'icono' => 'bi-list-check', 'texto' => 'Competencias'],
    'etapas'       => ['url' => 'views/pages/progresion.php?id=',          'icono' => 'bi-award',       'texto' => 'Etapas e insignias'],
];
?>
<nav class="nav-protagonista mb-4" aria-label="Vistas del protagonista">
    <ul class="nav nav-pills gap-1">
        <?php foreach ($_navItems as $clave => $item): ?>
            <?php $activa = ($clave === $paginaActual); ?>
            <li class="nav-item">
                <?php if ($activa): ?>
                    <span class="nav-link active" aria-current="page">
                        <i class="bi <?= $item['icono'] ?> me-1" aria-hidden="true"></i><?= $item['texto'] ?>
                    </span>
                <?php else: ?>
                    <a class="nav-link" href="<?= e(url($item['url'] . $idProtagonista)) ?>">
                        <i class="bi <?= $item['icono'] ?> me-1" aria-hidden="true"></i><?= $item['texto'] ?>
                    </a>
                <?php endif; ?>
            </li>
        <?php endforeach; ?>
    </ul>
</nav>
