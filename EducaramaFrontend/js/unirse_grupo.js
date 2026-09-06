document.addEventListener('DOMContentLoaded', () => {
    const formulario = document.getElementById('formUnirseGrupo');
    const mensajeAlerta = document.getElementById('mensajeAlerta');

    // Verificamos que haya un usuario en la sesión para poder enviarle el ID al backend
    const usuarioString = sessionStorage.getItem('usuarioLogueado');
    if (!usuarioString) {
        window.location.href = 'index.html'; // Si no está logueado, lo mandamos al login
        return;
    }
    const usuario = JSON.parse(usuarioString);

    formulario.addEventListener('submit', async (e) => {
        e.preventDefault();
        
        const codigoGrupo = document.getElementById('codigoGrupo').value.trim().toUpperCase();
        const idRol = parseInt(document.getElementById('idRol').value);
        const btnSubmit = document.getElementById('btnSubmit');

        try {
            btnSubmit.innerText = 'Verificando código...';
            btnSubmit.disabled = true;
            mensajeAlerta.innerHTML = '';

            const configRes = await fetch('config.json');
            const config = await configRes.json();

            // Armamos el payload que espera tu UnirseGrupoDTO
            const payload = {
                idUsuario: usuario.idUsuario || usuario.id,
                codigoGrupo: codigoGrupo,
                idRol: idRol
            };

            // Pegamos al endpoint que armaste en el backend
            const response = await fetch(`${config.apiUrl}/usuarios/unirse-grupo`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });

            const data = await response.json();

            if (!response.ok) {
                throw new Error(data.error || 'No se pudo vincular al grupo. Verificá el código.');
            }

            // Si salió bien, mostramos éxito y redirigimos al dashboard
            mostrarAlerta('success', '¡Vinculación exitosa! Redirigiendo...');
            
            setTimeout(() => {
                window.location.href = 'dashboard.html';
            }, 1500);

        } catch (error) {
            console.error('Error:', error);
            mostrarAlerta('danger', error.message);
        } finally {
            btnSubmit.innerText = 'Vincular Cuenta';
            btnSubmit.disabled = false;
        }
    });

    function mostrarAlerta(tipo, mensaje) {
        mensajeAlerta.innerHTML = `
            <div class="alert alert-${tipo} alert-dismissible fade show" role="alert">
                ${mensaje}
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        `;
    }
});