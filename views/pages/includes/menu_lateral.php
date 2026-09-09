
<?php
//views/pages/includes/menu_lateral.php
/** Panel de navegación lateral para la Plataforma Educarama.
 * Incluye secciones de Inicio, Protagonistas, Ramas y Patrullas con submenús colapsables.*/
?>

<div class="sidebar">
    <nav class="nav flex-column">
            
            <a class="nav-link" href="/Educarama/public/index.php">
                <span class="icon">
                    <i class="bi bi-grid"></i>
                </span>
                <span class="description">Inicio</span>
            </a>

            <a class="nav-link" href="#" data-bs-toggle="collapse" data-bs-target="#submenu-protagonistas" aria-expanded="false" aria-controls="submenu-protagonistas">
                <span class="icon"><i class="bi bi-people"></i></span>
                <span class="description">Protagonistas <i class="bi bi-caret-down-fill"></i></span>
            </a>
            
            <div class="sub-menu collapse" id="submenu-protagonistas">
                <a class="nav-link" href="/Educarama/views/pages/protagonistas.php">
                    <span class="icon"><i class="bi bi-eye"></i></span>
                    <span class="description">Ver protagonistas</span>
                </a>
                <a class="nav-link" href="/Educarama/views/pages/alta_protagonista.php">
                    <span class="icon"><i class="bi bi-person-plus"></i></span>
                    <span class="description">Agregar protagonista</span>
                </a>
            </div>

            <a class="nav-link" href="#" data-bs-toggle="collapse" data-bs-target="#submenu-ramas" aria-expanded="false" aria-controls="submenu-ramas">
                <span class="icon"><i class="bi bi-diagram-3"></i></span>
                <span class="description">Ramas <i class="bi bi-caret-down-fill"></i></span>
            </a>
            <div class="sub-menu collapse" id="submenu-ramas">
                <a class="nav-link" href="/Educarama/views/pages/ramas.php">
                    <span class="icon"><i class="bi bi-eye"></i></span>
                    <span class="description">Ver ramas</span>
                </a>

            </div>

            <a class="nav-link" href="#" data-bs-toggle="collapse" data-bs-target="#submenu-patrullas" aria-expanded="false" aria-controls="submenu-patrullas">
                <span class="icon"><i class="bi bi-shield-shaded"></i></span>
                <span class="description">Patrullas <i class="bi bi-caret-down-fill"></i></span>
            </a>
            <div class="sub-menu collapse" id="submenu-patrullas">
                <a class="nav-link" href="/Educarama/views/pages/patrullas.php">
                    <span class="icon"><i class="bi bi-eye"></i></span>
                    <span class="description">Ver patrullas</span>
                </a>
                <a class="nav-link" href="/Educarama/views/pages/alta_patrulla.php">
                    <span class="icon"><i class="bi bi-plus-circle"></i></span>
                    <span class="description">Agregar patrulla</span>
                </a>
            </div>

            <a href="/Educarama/views/pages/eventos.php" class="nav-link">
                <span class="icon"><i class="bi bi-megaphone"></i></span>
                <span class="description">Eventos</span>
            </a>
            
            <nav>
                <a href="#" id="btn-logout" class="nav-link">
                    <span class="icon"><i class="bi bi-box-arrow-right"></i></span>
                    <span class="description">Cerrar sesión</span>
                </a>
            </nav>

    </nav>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        document.addEventListener('DOMContentLoaded', () => {
        const btnLogout = document.getElementById('btn-logout');

        if (btnLogout) {
            btnLogout.addEventListener('click', (e) => {
                e.preventDefault(); // Detiene cualquier navegación por defecto

                Swal.fire({
                    title: '¿Estás seguro?',
                    text: "Se cerrará tu sesión actual.",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Sí, cerrar sesión',
                    cancelButtonText: 'Cancelar'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Solo si el usuario confirma, ejecutamos el logout apuntando a tu controlador
                        window.location.href = '/Educarama/src/Controladores/LogoutController.php'; 
                    }
                });
            });
        }
    });
    </script>
</div>  