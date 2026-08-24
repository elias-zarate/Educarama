document.addEventListener('DOMContentLoaded', cargarProtagonistas);

async function cargarProtagonistas() {
    const contenedor = document.getElementById('contenedor-protagonistas');
    
    try {
        // 1. Obtener la URL base desde el archivo de configuración
        const configResponse = await fetch('../config.json');
        if (!configResponse.ok) throw new Error('No se pudo cargar config.json');
        const config = await configResponse.json();
        const apiUrl = config.apiUrl;

        // 2. Hacer la petición al endpoint dinámico
        const response = await fetch(`${apiUrl}/protagonistas`);
        
        if (!response.ok) throw new Error('Error al obtener los protagonistas');
        
        const protagonistas = await response.json();
        contenedor.innerHTML = ''; 

        if (protagonistas.length === 0) {
            contenedor.innerHTML = '<p>No hay protagonistas cargados.</p>';
            return;
        }

        protagonistas.forEach(prota => {
            const tarjeta = `
                <div class="col-12 col-md-6 col-lg-4">
                    <a href="../perfil_protagonista.html?id=${prota.idProtagonista}" class="protagonista-card text-decoration-none">
                        <div class="protagonista-foto">
                            <i class="bi bi-person-fill"></i>
                        </div>
                        <div class="protagonista-info">
                            <h3 class="text-dark">
                                ${prota.nombre} ${prota.apellido}
                            </h3>
                            <p class="text-secondary">
                                <i class="bi bi-diagram-3 me-1"></i>
                                Rama ID: ${prota.idRama}
                            </p>
                            <p class="text-secondary">
                                <i class="bi bi-calendar-event me-1"></i>
                                ${prota.fechaNacimiento}
                            </p>
                            <span class="ver-perfil">Ver perfil</span>
                        </div>
                    </a>
                </div>
            `;
            contenedor.innerHTML += tarjeta;
        });
    } catch (error) {
        console.error(error);
        contenedor.innerHTML = '<p class="text-danger">Error de conexión con el servidor.</p>';
    }
}