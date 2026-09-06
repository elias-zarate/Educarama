document.addEventListener('DOMContentLoaded', async () => {
    await cargarSelectRamas();
    configurarFormularioAltaPatrulla();
});

async function cargarSelectRamas() {
    const selectRama = document.getElementById('id_rama');

    try {
        const configRes = await fetch('../config.json');
        const config = await configRes.json();

        const response = await fetch(`${config.apiUrl}/ramas`);
        if (!response.ok) throw new Error('Error al obtener las ramas.');

        const ramas = await response.json();

        selectRama.innerHTML = '<option value="" disabled selected>Elegí una rama...</option>';
        if (ramas.length === 0) {
            selectRama.innerHTML = '<option value="" disabled>No hay ramas disponibles</option>';
            return;
        }

        ramas.forEach(rama => {
            selectRama.innerHTML += `<option value="${rama.id}">${rama.nombre}</option>`;
        });

    } catch (error) {
        console.error('Error:', error);
        mostrarAlerta('danger', 'No se pudieron cargar las ramas.');
        selectRama.innerHTML = '<option value="" disabled>Error de conexión</option>';
    }
}

function configurarFormularioAltaPatrulla() {
    const form = document.getElementById('form-alta-patrulla');
    if (!form) return;

    form.addEventListener('submit', async (e) => {
        e.preventDefault();

        const nombre = document.getElementById('nombre').value.trim();
        const idRama = parseInt(document.getElementById('id_rama').value);

        const btnSubmit = form.querySelector('button[type="submit"]');
        const textoOriginal = btnSubmit.innerHTML;

        try {
            btnSubmit.innerHTML = '<i class="bi bi-hourglass-split me-2"></i>Guardando...';
            btnSubmit.disabled = true;

            const configRes = await fetch('../config.json');
            const config = await configRes.json();

            const payload = {
                nombre: nombre,
                idRama: idRama
            };

            const response = await fetch(`${config.apiUrl}/patrullas`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });

            const data = await response.json();

            if (!response.ok) {
                throw new Error(data.error || 'No se pudo registrar la patrulla.');
            }

            mostrarAlerta('success', '¡Patrulla creada exitosamente! Redirigiendo...');

            setTimeout(() => {
                window.location.href = 'patrullas.html';
            }, 1500);

        } catch (error) {
            console.error('Error al registrar patrulla:', error);
            mostrarAlerta('danger', error.message);
            btnSubmit.innerHTML = textoOriginal;
            btnSubmit.disabled = false;
        }
    });
}

function mostrarAlerta(tipo, mensaje) {
    const contenedor = document.getElementById('alerta-contenedor');
    if (contenedor) {
        contenedor.innerHTML = `
            <div class="alert alert-${tipo} alert-dismissible fade show shadow-sm" role="alert">
                ${mensaje}
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        `;
    }
}