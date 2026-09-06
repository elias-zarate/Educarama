document.addEventListener('DOMContentLoaded', async () => {
    await cargarSelectsAltaRama();
    configurarFormularioAltaRama();
});

async function cargarSelectsAltaRama() {
    const selectGrupo = document.getElementById('id_grupo');
    const selectCategoria = document.getElementById('id_categoria_rama');

    try {
        const configRes = await fetch('../config.json');
        const config = await configRes.json();

        const [gruposRes, categoriasRes] = await Promise.all([
            fetch(`${config.apiUrl}/grupos`),
            fetch(`${config.apiUrl}/categoria-ramas`)
        ]);

        if (!gruposRes.ok || !categoriasRes.ok) {
            throw new Error('Error al obtener los datos para los selectores.');
        }

        const grupos = await gruposRes.json();
        const categorias = await categoriasRes.json();

        selectGrupo.innerHTML = '<option value="" disabled selected>Elegí un grupo...</option>';
        if (grupos.length === 0) {
            selectGrupo.innerHTML = '<option value="" disabled>No hay grupos disponibles</option>';
        } else {
            grupos.forEach(g => {
                selectGrupo.innerHTML += `<option value="${g.id}">${g.nombre} (Cód: ${g.codigo})</option>`;
            });
        }

        selectCategoria.innerHTML = '<option value="" disabled selected>Elegí una categoría...</option>';
        if (categorias.length === 0) {
            selectCategoria.innerHTML = '<option value="" disabled>No hay categorías disponibles</option>';
        } else {
            categorias.forEach(cat => {
                selectCategoria.innerHTML += `<option value="${cat.id}">${cat.nombre}</option>`;
            });
        }

    } catch (error) {
        console.error('Error:', error);
        mostrarAlerta('danger', 'No se pudieron cargar los datos de configuración.');
        selectGrupo.innerHTML = '<option value="" disabled>Error de conexión</option>';
        selectCategoria.innerHTML = '<option value="" disabled>Error de conexión</option>';
    }
}

function configurarFormularioAltaRama() {
    const form = document.getElementById('form-alta-rama');
    if (!form) return;

    form.addEventListener('submit', async (e) => {
        e.preventDefault();

        const nombre = document.getElementById('nombre').value.trim();
        const idGrupo = parseInt(document.getElementById('id_grupo').value);
        const idCategoriaRama = parseInt(document.getElementById('id_categoria_rama').value);

        const btnSubmit = form.querySelector('button[type="submit"]');
        const textoOriginal = btnSubmit.innerHTML;

        try {
            btnSubmit.innerHTML = '<i class="bi bi-hourglass-split me-2"></i>Guardando...';
            btnSubmit.disabled = true;

            const configRes = await fetch('../config.json');
            const config = await configRes.json();

            const payload = {
                nombre: nombre,
                idGrupo: idGrupo,
                idCategoriaRama: idCategoriaRama
            };

            const response = await fetch(`${config.apiUrl}/ramas`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });

            const data = await response.json();

            if (!response.ok) {
                throw new Error(data.error || 'No se pudo registrar la rama.');
            }

            mostrarAlerta('success', '¡Rama creada exitosamente! Redirigiendo...');

            setTimeout(() => {
                window.location.href = 'ramas.html';
            }, 1500);

        } catch (error) {
            console.error('Error al registrar rama:', error);
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