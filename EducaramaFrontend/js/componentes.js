document.addEventListener("DOMContentLoaded", () => {
    // 1. Detectar inteligentemente si estamos en una subcarpeta
    const pathname = window.location.pathname;
    const rutaActual = pathname.substring(pathname.lastIndexOf('/') + 1);
    
    // Lista de subcarpetas conocidas en tu estructura
    const subcarpetas = ['ramas', 'patrullas', 'protagonistas', 'avisos', 'roles', 'usuarios'];
    
    // Verificamos si alguna subcarpeta es parte de la ruta actual
    const enSubcarpeta = subcarpetas.some(carpeta => pathname.includes(`/${carpeta}/`));
    
    // Si estamos en una subcarpeta, necesitamos subir un nivel para los recursos base
    const basePath = enSubcarpeta ? '../' : '';

    // 2. Inyectamos la Barra Superior (Header)
    const headerContainer = document.querySelector('.header-inst');
    if (headerContainer && headerContainer.innerHTML.trim() === '') {
        headerContainer.innerHTML = `
            <div class="d-flex align-items-center gap-2">
                <img class="img-fluid rounded-circle" src="${basePath}assets/images/Logo1.png" alt="Logo Educarama" style="width: 40px; height: 40px;">
                <h1 class="m-0 fs-4 texto-blanco fw-bold">Educarama</h1>
            </div>
        `;
    }

    // 3. Inyectamos el Menú Lateral (Sidebar)
    const sidebarContainer = document.querySelector('.sidebar');
    if (sidebarContainer) {
        sidebarContainer.innerHTML = `
            <nav class="nav flex-column">
                <a class="nav-link ${rutaActual === 'dashboard.html' || rutaActual === '' ? 'active' : ''}" href="${basePath}dashboard.html">
                    <span class="icon"><i class="bi bi-grid"></i></span><span class="description">Inicio</span>
                </a>
                
                <a class="nav-link" href="#" data-bs-toggle="collapse" data-bs-target="#submenu-protagonistas">
                    <span class="icon"><i class="bi bi-people"></i></span><span class="description">Protagonistas <i class="bi bi-caret-down-fill"></i></span>
                </a>
                <div class="sub-menu collapse ${pathname.includes('/protagonistas/') ? 'show' : ''}" id="submenu-protagonistas">
                    <a class="nav-link ${rutaActual === 'protagonistas.html' ? 'active' : ''}" href="${basePath}protagonistas/protagonistas.html"><span class="icon"><i class="bi bi-eye"></i></span><span class="description">Ver protagonistas</span></a>
                    <a class="nav-link ${rutaActual === 'alta_protagonista.html' ? 'active' : ''}" href="${basePath}protagonistas/alta_protagonista.html"><span class="icon"><i class="bi bi-person-plus"></i></span><span class="description">Agregar protagonista</span></a>
                </div>

                <a class="nav-link" href="#" data-bs-toggle="collapse" data-bs-target="#submenu-ramas">
                    <span class="icon"><i class="bi bi-diagram-3"></i></span><span class="description">Ramas <i class="bi bi-caret-down-fill"></i></span>
                </a>
                <div class="sub-menu collapse ${pathname.includes('/ramas/') ? 'show' : ''}" id="submenu-ramas">
                    <a class="nav-link ${rutaActual === 'ramas.html' ? 'active' : ''}" href="${basePath}ramas/ramas.html"><span class="icon"><i class="bi bi-eye"></i></span><span class="description">Ver ramas</span></a>
                    <a class="nav-link ${rutaActual === 'alta_rama.html' ? 'active' : ''}" href="${basePath}ramas/alta_rama.html"><span class="icon"><i class="bi bi-plus-circle"></i></span><span class="description">Agregar rama</span></a>
                </div>

                <a class="nav-link" href="#" data-bs-toggle="collapse" data-bs-target="#submenu-patrullas">
                    <span class="icon"><i class="bi bi-shield-shaded"></i></span><span class="description">Patrullas <i class="bi bi-caret-down-fill"></i></span>
                </a>
                <div class="sub-menu collapse ${pathname.includes('/patrullas/') ? 'show' : ''}" id="submenu-patrullas">
                    <a class="nav-link ${rutaActual === 'patrullas.html' ? 'active' : ''}" href="${basePath}patrullas/patrullas.html"><span class="icon"><i class="bi bi-eye"></i></span><span class="description">Ver patrullas</span></a>
                    <a class="nav-link ${rutaActual === 'alta_patrulla.html' ? 'active' : ''}" href="${basePath}patrullas/alta_patrulla.html"><span class="icon"><i class="bi bi-plus-circle"></i></span><span class="description">Agregar patrulla</span></a>
                </div>

                <a class="nav-link ${rutaActual === 'gestionar_avisos.html' ? 'active' : ''}" href="${basePath}avisos/gestionar_avisos.html">
                    <span class="icon"><i class="bi bi-megaphone"></i></span><span class="description">Avisos</span>
                </a>

                <hr class="border-secondary opacity-25 my-2">

                <!-- Botón de Cerrar Sesión -->
                <a href="#" id="btnCerrarSesion" class="nav-link text-danger">
                    <span class="icon"><i class="bi bi-box-arrow-right"></i></span>
                    <span class="description">Cerrar Sesión</span>
                </a>
            </nav>
        `;

        // 4. Lógica para cerrar sesión
        const btnCerrarSesion = document.getElementById('btnCerrarSesion');
        if (btnCerrarSesion) {
            btnCerrarSesion.addEventListener('click', async (e) => {
                e.preventDefault();
                
                // Aseguramos que auth.js esté cargado en la vista actual
                if (typeof Auth !== 'undefined' && Auth.logout) {
                    await Auth.logout(basePath);
                } else {
                    // Fallback seguro por si la vista no incluyó el script de auth.js
                    sessionStorage.removeItem('usuarioLogueado');
                }
                
                window.location.href = `${basePath}index.html`; 
            });
        }
    }
});