document.addEventListener('DOMContentLoaded', cargarCategoriasRamas);

async function cargarCategoriasRamas() {
    const contenedor = document.getElementById('contenedor-ramas');
    
    try {
        // 1. Cargamos el archivo de configuración
        const configResponse = await fetch('../config.json');
        if (!configResponse.ok) throw new Error('No se pudo cargar config.json');
        const config = await configResponse.json();
        const apiUrl = config.apiUrl;

        // 2. Consultamos las Categorías de Rama (los nombres oficiales)
        const response = await fetch(`${apiUrl}/categoria-ramas`);
        
        if (!response.ok) throw new Error('Error al obtener las categorías de ramas');
        
        const categorias = await response.json();
        contenedor.innerHTML = ''; 

        if (categorias.length === 0) {
            contenedor.innerHTML = '<p>No hay categorías de ramas cargadas.</p>';
            return;
        }

        categorias.forEach(cat => {
            // Armamos la tarjeta para cada categoría
            const tarjeta = `
                <div class="col-12 col-md-6 col-lg-4">
                    <div class="rama-card">
                        <div class="rama-icono">
                            <i class="bi bi-diagram-3-fill"></i>
                        </div>
                        <div>
                            <h3>${cat.nombre}</h3>
                            <p class="text-secondary">
                                <i class="bi bi-collection me-1"></i>
                                Rama Oficial del Movimiento
                            </p>
                            <!-- Podés agregar un botón o enlace para entrar a ver las competencias o protagonistas de esta categoría -->
                            <a href="#" class="btn btn-sm btn-outline-primary mt-2">
                                Ver detalles <i class="bi bi-arrow-right"></i>
                            </a>
                        </div>
                    </div>
                </div>
            `;
            contenedor.innerHTML += tarjeta;
        });
    } catch (error) {
        console.error(error);
        contenedor.innerHTML = '<p class="text-danger">Error de conexión con el servidor.</p>';
    }
}