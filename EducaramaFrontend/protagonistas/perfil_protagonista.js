let competenciasGlobales = [];

document.addEventListener('DOMContentLoaded', async () => {
    const urlParams = new URLSearchParams(window.location.search);
    const idProtagonista = urlParams.get('id');

    if (!idProtagonista) {
        mostrarAlerta('danger', 'No se especificó un protagonista.');
        return;
    }

    await cargarPerfilCompleto(idProtagonista);

    // Inicializamos el evento para otorgar insignia
    const formInsignia = document.getElementById('formOtorgarInsignia');
    if (formInsignia) {
        formInsignia.addEventListener('submit', (e) => {
            e.preventDefault();
            const idInsignia = document.getElementById('selectInsignia').value;
            otorgarInsignia(parseInt(idInsignia), idProtagonista);
        });
    }
});

async function cargarPerfilCompleto(id) {
    try {
        const configRes = await fetch('../config.json');
        const config = await configRes.json();

        const [perfilRes, familiaresRes, competenciasRes] = await Promise.all([
            fetch(`${config.apiUrl}/protagonistas/${id}`),
            fetch(`${config.apiUrl}/familiares/protagonista/${id}`),
            fetch(`${config.apiUrl}/competencias-protagonista/protagonista/${id}`)
        ]);

        if (!perfilRes.ok) throw new Error('No se pudo cargar el perfil del protagonista.');

        const perfil = await perfilRes.json();
        const familiares = familiaresRes.ok ? await familiaresRes.json() : [];
        competenciasGlobales = competenciasRes.ok ? await competenciasRes.json() : [];

        renderizarCabeceraYDatos(perfil);
        renderizarFamiliares(familiares);
        renderizarListasCompetencias(competenciasGlobales);

    } catch (error) {
        console.error('Error:', error);
        mostrarAlerta('danger', error.message);
    }
}

function renderizarCabeceraYDatos(perfil) {
    document.getElementById('nombre-protagonista').innerText = `${perfil.nombre} ${perfil.apellido}`;
    document.getElementById('rama-protagonista').innerText = `Rama Asignada: ${perfil.idRama}`;

    const promesaBadge = perfil.promesa 
        ? '<span class="badge bg-success">Promesado</span>' 
        : '<span class="badge bg-secondary">Sin promesa</span>';

    document.getElementById('contenido-datos').innerHTML = `
        <ul class="list-unstyled mb-0">
            <li class="mb-2"><strong>DNI:</strong> ${perfil.dni}</li>
            <li class="mb-2"><strong>Fecha de Nacimiento:</strong> ${perfil.fechaNacimiento}</li>
            <li class="mb-2"><strong>Estado:</strong> ${promesaBadge}</li>
        </ul>
    `;
}

function renderizarFamiliares(familiares) {
    const contenedor = document.getElementById('listaFamiliares');
    
    if (familiares.length === 0) {
        contenedor.innerHTML = '<p class="text-muted small">No hay familiares vinculados.</p>';
        return;
    }

    contenedor.innerHTML = familiares.map(f => `
        <div class="d-flex justify-content-between align-items-center border-bottom border-secondary border-opacity-25 py-2">
            <div>
                <strong class="d-block">${f.nombre} ${f.apellido}</strong>
            </div>
            <span class="badge bg-info text-dark">${f.parentesco || 'Familiar'}</span>
        </div>
    `).join('');
}

function renderizarListasCompetencias(competencias) {
    const listaComp = document.getElementById('lista-completadas');
    const listaDes = document.getElementById('lista-desarrollo');

    if (competencias.length === 0) {
        const msjVacio = '<p class="text-muted small">No hay competencias asignadas.</p>';
        listaComp.innerHTML = msjVacio;
        listaDes.innerHTML = msjVacio;
        return;
    }

    const completadas = competencias.filter(c => c.terminada);
    const enDesarrollo = competencias.filter(c => !c.terminada);

    listaComp.innerHTML = completadas.length > 0
        ? completadas.map(c => crearItemLista(c)).join('')
        : '<p class="text-muted small">Aún no hay competencias completadas.</p>';

    listaDes.innerHTML = enDesarrollo.length > 0
        ? enDesarrollo.map(c => crearItemLista(c)).join('')
        : '<p class="text-muted small">No hay competencias en desarrollo.</p>';
}

function crearItemLista(comp) {
    const sendero = obtenerDatosSendero(comp.codigo);
    return `
        <a href="#" class="list-group-item list-group-item-action d-flex align-items-center gap-3 py-3" onclick="abrirModalCompetencia(${comp.idCompetencia}, event)">
            <i class="bi ${sendero.icono} fs-3 text-violeta"></i>
            <div>
                <h6 class="mb-1 fw-bold text-dark">${sendero.nombre}</h6>
                <small class="text-muted d-block line-clamp-1">${comp.codigo}. ${comp.descripcion.substring(0, 60)}...</small>
            </div>
        </a>
    `;
}

function abrirModalCompetencia(idCompetencia, event) {
    event.preventDefault(); 
    
    const comp = competenciasGlobales.find(c => c.idCompetencia === idCompetencia);
    if (!comp) return;

    const sendero = obtenerDatosSendero(comp.codigo);
    
    const requeridas = comp.acciones ? comp.acciones.filter(a => a.idTipoAccion === 1) : [];
    const opcionales = comp.acciones ? comp.acciones.filter(a => a.idTipoAccion === 2) : [];

    const generarListaHtml = (acciones, startNum = 1) => {
        if (acciones.length === 0) return '<p class="small text-muted ps-3">No hay acciones cargadas.</p>';
        return `
            <ol class="lista-acciones" start="${startNum}">
                ${acciones.map(a => `
                    <li>
                        <label class="accion-item-wrapper">
                            <input type="checkbox" 
                                   ${a.cumplida ? 'checked' : ''} 
                                   onchange="guardarCumplimiento(${a.idAccion}, this)">
                            <span>${a.descripcion}</span>
                        </label>
                    </li>
                `).join('')}
            </ol>
        `;
    };

    const htmlTarjeta = `
        <button type="button" class="btn-close position-absolute top-0 end-0 m-3 shadow-none bg-white p-2 rounded-circle" data-bs-dismiss="modal" aria-label="Close" style="z-index: 10;"></button>
        <div class="tarjeta-competencia ${sendero.clase} mb-0 shadow-lg">
            <div class="competencia-header">
                <i class="bi ${sendero.icono} icono-sendero"></i>
                <h3>${sendero.nombre}</h3>
            </div>
            <div class="competencia-subheader">
                ${comp.codigo}. ${comp.descripcion}
            </div>
            <div class="competencia-body">
                <div class="titulo-acciones border-bottom border-secondary border-opacity-25">
                    Acciones Requeridas
                </div>
                ${generarListaHtml(requeridas, 1)}
                
                <div class="titulo-acciones border-bottom border-secondary border-opacity-25 mt-4">
                    Acciones Opcionales
                </div>
                ${generarListaHtml(opcionales, requeridas.length + 1)}
            </div>
        </div>
    `;

    document.getElementById('contenedor-tarjeta-modal').innerHTML = htmlTarjeta;
    const modal = new bootstrap.Modal(document.getElementById('modalCompetencia'));
    modal.show();
}

async function guardarCumplimiento(idAccion, checkbox) {
    const urlParams = new URLSearchParams(window.location.search);
    const idProtagonista = parseInt(urlParams.get('id'));
    const estadoCumplido = checkbox.checked;

    try {
        checkbox.disabled = true;

        const configRes = await fetch('../config.json');
        const config = await configRes.json();

        const payload = {
            idProtagonista: idProtagonista,
            idAccion: idAccion,
            accionCumplida: estadoCumplido
        };

        const response = await fetch(`${config.apiUrl}/cumplimientos`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            const data = await response.json();
            throw new Error(data.error || 'No se pudo guardar el progreso.');
        }

        actualizarEstadoLocal(idAccion, estadoCumplido);

    } catch (error) {
        console.error('Error al guardar:', error);
        mostrarAlerta('danger', error.message);
        checkbox.checked = !estadoCumplido;
    } finally {
        checkbox.disabled = false;
    }
}

function actualizarEstadoLocal(idAccion, estadoCumplido) {
    competenciasGlobales.forEach(comp => {
        if (comp.acciones) {
            const accionEncontrada = comp.acciones.find(a => a.idAccion === idAccion);
            if (accionEncontrada) {
                accionEncontrada.cumplida = estadoCumplido;
            }
        }
    });
}

// SUBTAREA 4: Lógica para otorgar la insignia
async function otorgarInsignia(idInsignia, idProtagonista) {
    const btnConfirmar = document.getElementById('btnConfirmarInsignia');

    try {
        btnConfirmar.innerText = 'Procesando...';
        btnConfirmar.disabled = true;

        const configRes = await fetch('../config.json');
        const config = await configRes.json();

        // El DTO en tu backend es: Id, IdInsignia, FechaObtencion
        const payload = {
            id: idProtagonista,
            idInsignia: idInsignia,
            fechaObtencion: new Date().toISOString()
        };

        const response = await fetch(`${config.apiUrl}/obtenciones`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            const data = await response.json();
            throw new Error(data.error || 'No se pudo otorgar la insignia.');
        }

        // Cerramos el modal
        const modalEl = document.getElementById('modalOtorgarInsignia');
        const modal = bootstrap.Modal.getInstance(modalEl);
        if (modal) modal.hide();

        mostrarAlerta('success', '¡Insignia otorgada exitosamente!');

    } catch (error) {
        console.error('Error al otorgar insignia:', error);
        mostrarAlerta('danger', error.message);
    } finally {
        btnConfirmar.innerText = 'Confirmar Obtención';
        btnConfirmar.disabled = false;
    }
}

function obtenerDatosSendero(codigo) {
    if (codigo >= 1 && codigo <= 14) return { nombre: 'Salud y bienestar', clase: 'sendero-salud', icono: 'bi-heart-pulse-fill' };
    if (codigo >= 15 && codigo <= 30) return { nombre: 'Paz y desarrollo', clase: 'sendero-paz', icono: 'bi-peace' };
    if (codigo >= 31 && codigo <= 39) return { nombre: 'Ambiente', clase: 'sendero-ambiente', icono: 'bi-tree-fill' };
    if (codigo >= 40 && codigo <= 53) return { nombre: 'Habilidades para la vida', clase: 'sendero-habilidades', icono: 'bi-tools' };
    
    return { nombre: 'Sendero General', clase: 'sendero-ambiente', icono: 'bi-star-fill' };
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