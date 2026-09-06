// js/app.js
document.addEventListener('DOMContentLoaded', () => {
    // 1. Validar que el usuario esté logueado
    const usuarioString = sessionStorage.getItem('usuarioLogueado');
    if (!usuarioString) {
        window.location.href = '../index.html';
        return;
    }

    const usuario = JSON.parse(usuarioString);
    
    // Opcional: Saludo personalizado
    const welcomeTitle = document.querySelector('.home-welcome h2');
    if(welcomeTitle){
        welcomeTitle.innerText = `Bienvenido a Educarama, ${usuario.nombre}`;
    }

    // 2. Cargar avisos
    cargarAvisos();
});

async function cargarAvisos() {
    const contenedorAvisos = document.getElementById('contenedor-avisos');
    if (!contenedorAvisos) return; // Si no estamos en el dashboard, no hace nada

    try {
        const configRes = await fetch('../config.json');
        const config = await configRes.json();
        
        const response = await fetch(`${config.apiUrl}/avisos/activos`);
        
        if (!response.ok) throw new Error('No se pudieron cargar los avisos');

        const avisos = await response.json();
        contenedorAvisos.innerHTML = '';

        if (avisos.length === 0) {
            contenedorAvisos.innerHTML = '<p class="text-muted mb-0">No hay avisos importantes por el momento.</p>';
            return;
        }

        avisos.forEach(aviso => {
            let badgeColor = 'bg-secondary';
            if (aviso.prioridad.toLowerCase() === 'alta') badgeColor = 'bg-danger';
            if (aviso.prioridad.toLowerCase() === 'media') badgeColor = 'bg-warning text-dark';
            if (aviso.prioridad.toLowerCase() === 'baja') badgeColor = 'bg-info text-dark';

            const fechaFinStr = aviso.fechaFin ? `Hasta el ${formatearFecha(aviso.fechaFin)}` : 'Sin fecha de expiración';

            const tarjetaHtml = `
                <div class="card mb-3 border-0 shadow-sm" style="background-color: rgba(255,255,255,0.7); border-left: 4px solid var(--violeta-primario) !important;">
                    <div class="card-body">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <h5 class="card-title mb-0 fw-bold text-violeta">${aviso.titulo}</h5>
                            <span class="badge ${badgeColor}">${aviso.prioridad.toUpperCase()}</span>
                        </div>
                        <p class="card-text mb-2">${aviso.descripcion}</p>
                        <p class="card-text m-0"><small class="text-muted"><i class="bi bi-calendar-event me-1"></i> ${fechaFinStr}</small></p>
                    </div>
                </div>
            `;
            contenedorAvisos.innerHTML += tarjetaHtml;
        });

    } catch (error) {
        console.error('Error al cargar avisos:', error);
        contenedorAvisos.innerHTML = '<p class="text-danger">Ocurrió un error al cargar los avisos.</p>';
    }
}

function formatearFecha(fechaSql) {
    if (!fechaSql) return '';
    const partes = fechaSql.split('-'); 
    if (partes.length === 3) {
        return `${partes[2]}/${partes[1]}/${partes[0]}`;
    }
    return fechaSql;
}