let avisosGlobales = [];

document.addEventListener('DOMContentLoaded', async () => {
    await cargarAvisos();
    inicializarFiltros();
    configurarControlDeFechas();
    configurarFormulario();
});

// SUBTAREA 1 & 2: Cargar avisos y almacenarlos en memoria
async function cargarAvisos() {
    const tbody = document.getElementById('tabla-avisos');
    
    try {
        tbody.innerHTML = '<tr><td colspan="6" class="text-center text-muted">Cargando avisos...</td></tr>';

        const configRes = await fetch('../config.json');
        const config = await configRes.json();

        const response = await fetch(`${config.apiUrl}/avisos`);
        if (!response.ok) throw new Error('No se pudieron cargar los avisos.');

        avisosGlobales = await response.json();
        
        // Mantener el filtro visual activo al recargar la tabla
        const filtroActivo = document.querySelector('#filtros-avisos button.active');
        const tipoFiltro = filtroActivo ? filtroActivo.getAttribute('data-filtro') : 'todos';
        filtrarYRenderizarAvisos(tipoFiltro);

    } catch (error) {
        console.error('Error:', error);
        tbody.innerHTML = `<tr><td colspan="6" class="text-center text-danger">Ocurrió un error: ${error.message}</td></tr>`;
    }
}

// SUBTAREA 2: Lógica de filtrado
function inicializarFiltros() {
    const botonesFiltro = document.querySelectorAll('#filtros-avisos button');
    
    botonesFiltro.forEach(boton => {
        boton.addEventListener('click', (e) => {
            botonesFiltro.forEach(b => b.classList.remove('active'));
            e.target.classList.add('active');
            
            const tipoFiltro = e.target.getAttribute('data-filtro');
            filtrarYRenderizarAvisos(tipoFiltro);
        });
    });
}

function filtrarYRenderizarAvisos(filtro) {
    let avisosFiltrados = avisosGlobales;

    if (filtro === 'activos') {
        avisosFiltrados = avisosGlobales.filter(a => a.activo);
    } else if (filtro === 'inactivos') {
        avisosFiltrados = avisosGlobales.filter(a => !a.activo);
    }

    renderizarTabla(avisosFiltrados);
}

function renderizarTabla(avisos) {
    const tbody = document.getElementById('tabla-avisos');

    if (avisos.length === 0) {
        tbody.innerHTML = '<tr><td colspan="6" class="text-center text-muted">No hay avisos que coincidan con el filtro.</td></tr>';
        return;
    }

    tbody.innerHTML = avisos.map(aviso => {
        let badgePrioridad = 'bg-secondary';
        if (aviso.prioridad.toLowerCase() === 'alta') badgePrioridad = 'bg-danger';
        if (aviso.prioridad.toLowerCase() === 'media') badgePrioridad = 'bg-warning text-dark';
        if (aviso.prioridad.toLowerCase() === 'baja') badgePrioridad = 'bg-info text-dark';

        const badgeEstado = aviso.activo 
            ? '<span class="badge bg-success">Activo</span>' 
            : '<span class="badge bg-secondary">Inactivo</span>';

        const fechaInicio = formatearFecha(aviso.fechaInicio);
        const fechaFin = aviso.fechaFin ? formatearFecha(aviso.fechaFin) : '<span class="text-muted">-</span>';

        return `
            <tr>
                <td class="fw-bold texto-violeta">${aviso.titulo}</td>
                <td><span class="badge ${badgePrioridad}">${aviso.prioridad.toUpperCase()}</span></td>
                <td>${fechaInicio}</td>
                <td>${fechaFin}</td>
                <td>${badgeEstado}</td>
                <td class="text-end">
                    <button class="btn btn-sm btn-outline-primary me-1" title="Editar" onclick="editarAviso(${aviso.id})">
                        <i class="bi bi-pencil"></i>
                    </button>
                    <button class="btn btn-sm btn-outline-danger" title="Eliminar" onclick="eliminarAviso(${aviso.id})">
                        <i class="bi bi-trash"></i>
                    </button>
                </td>
            </tr>
        `;
    }).join('');
}

// SUBTAREA 2: Control de fechas (aislado del evento submit)
function configurarControlDeFechas() {
    const inputFechaInicio = document.getElementById('fecha_inicio');
    const inputFechaFin = document.getElementById('fecha_fin');

    if (inputFechaInicio && inputFechaFin) {
        // Aseguramos que la fecha de fin mínima sea igual a la fecha de inicio seleccionada
        inputFechaInicio.addEventListener('change', () => {
            inputFechaFin.min = inputFechaInicio.value;
            if (inputFechaFin.value && inputFechaFin.value < inputFechaInicio.value) {
                inputFechaFin.value = inputFechaInicio.value;
            }
        });
    }
}

// SUBTAREA 3: Captura del formulario y POST a la API
function configurarFormulario() {
    const formAviso = document.getElementById('form-aviso');
    if (formAviso) {
        formAviso.addEventListener('submit', guardarAviso);
    }
}

async function guardarAviso(e) {
    e.preventDefault();

    const titulo = document.getElementById('titulo').value.trim();
    const descripcion = document.getElementById('descripcion').value.trim();
    const fechaInicio = document.getElementById('fecha_inicio').value;
    const fechaFin = document.getElementById('fecha_fin').value;
    const prioridad = document.getElementById('prioridad').value;

    // Validación extra para evitar envío si tocaron el HTML manual
    if (fechaFin && fechaFin < fechaInicio) {
        mostrarAlerta('warning', 'La fecha de fin no puede ser anterior a la fecha de inicio.');
        return;
    }

    const btnGuardar = document.getElementById('btn-guardar-aviso');
    const textoOriginal = btnGuardar.innerHTML;

    try {
        btnGuardar.innerHTML = '<i class="bi bi-hourglass-split me-2"></i>Guardando...';
        btnGuardar.disabled = true;

        const configRes = await fetch('../config.json');
        const config = await configRes.json();

        // Armamos el RegistroAvisoDTO
        const payload = {
            titulo: titulo,
            descripcion: descripcion,
            fechaInicio: fechaInicio,
            fechaFin: fechaFin ? fechaFin : null,
            prioridad: prioridad
        };

        const response = await fetch(`${config.apiUrl}/avisos`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        const data = await response.json();

        if (!response.ok) {
            throw new Error(data.error || 'No se pudo guardar el aviso.');
        }

        // Éxito: Mostramos alerta, limpiamos y recargamos
        mostrarAlerta('success', '¡Aviso creado exitosamente!');
        document.getElementById('form-aviso').reset();
        await cargarAvisos(); 

    } catch (error) {
        console.error('Error al guardar aviso:', error);
        mostrarAlerta('danger', error.message);
    } finally {
        btnGuardar.innerHTML = textoOriginal;
        btnGuardar.disabled = false;
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

// Funciones preparadas para futuras subtareas
function editarAviso(id) {
    console.log('Preparando edición del aviso ID:', id);
}

function eliminarAviso(id) {
    console.log('Preparando eliminación del aviso ID:', id);
}