document.addEventListener('DOMContentLoaded', () => {
    // Referencias del DOM
    const paso1 = document.getElementById('paso1');
    const paso2 = document.getElementById('paso2');
    const formSolicitar = document.getElementById('formSolicitar');
    const formRestablecer = document.getElementById('formRestablecer');
    const mensajeAlerta = document.getElementById('mensajeAlerta');
    const textoInstructivo = document.getElementById('textoInstructivo');
    
    let emailGuardado = '';

    // EVENTO 1: Enviar solicitud de recuperación
    formSolicitar.addEventListener('submit', async (e) => {
        e.preventDefault();
        
        const email = document.getElementById('emailRecuperacion').value.trim();
        const btnSolicitar = document.getElementById('btnSolicitar');

        try {
            btnSolicitar.innerText = 'Enviando...';
            btnSolicitar.disabled = true;
            mensajeAlerta.innerHTML = '';

            const configRes = await fetch('config.json');
            const config = await configRes.json();

            const payload = { email: email };

            const response = await fetch(`${config.apiUrl}/auth/recuperar-contrasenia`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });

            const data = await response.json();

            if (!response.ok) {
                throw new Error(data.error || 'No se pudo procesar la solicitud.');
            }

            // ÉXITO MVP: Guardamos el mail para el paso 2 y mostramos el token en pantalla
            emailGuardado = email;
            
            // Ocultamos paso 1 y mostramos paso 2
            paso1.classList.add('d-none');
            paso2.classList.remove('d-none');
            textoInstructivo.innerText = 'Ingresá el código que recibiste y tu nueva contraseña.';
            
            // Simulamos el correo recibido mostrando el token en pantalla (SOLO PARA MVP)
            mostrarAlerta('info', `<strong>¡Correo Simulado!</strong> Tu código de recuperación es: <span class="fs-4 d-block mt-2 text-center" style="letter-spacing: 2px;">${data.token}</span>`);

        } catch (error) {
            console.error('Error:', error);
            mostrarAlerta('danger', error.message);
        } finally {
            btnSolicitar.innerText = 'Enviar Código';
            btnSolicitar.disabled = false;
        }
    });

    // EVENTO 2: Confirmar restablecimiento
    formRestablecer.addEventListener('submit', async (e) => {
        e.preventDefault();
        
        const token = document.getElementById('tokenRecuperacion').value.trim().toUpperCase();
        const nuevaContrasenia = document.getElementById('nuevaContrasenia').value;
        const btnRestablecer = document.getElementById('btnRestablecer');

        try {
            btnRestablecer.innerText = 'Guardando...';
            btnRestablecer.disabled = true;
            mensajeAlerta.innerHTML = '';

            const configRes = await fetch('config.json');
            const config = await configRes.json();

            const payload = { 
                email: emailGuardado,
                token: token,
                nuevaContrasenia: nuevaContrasenia
            };

            const response = await fetch(`${config.apiUrl}/auth/restablecer`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });

            const data = await response.json();

            if (!response.ok) {
                throw new Error(data.error || 'No se pudo restablecer la contraseña.');
            }

            // ÉXITO
            mostrarAlerta('success', '¡Contraseña actualizada con éxito! Redirigiendo al login...');
            
            setTimeout(() => {
                window.location.href = 'login.html';
            }, 2000);

        } catch (error) {
            console.error('Error:', error);
            mostrarAlerta('danger', error.message);
        } finally {
            btnRestablecer.innerText = 'Guardar Nueva Contraseña';
            btnRestablecer.disabled = false;
        }
    });

    // Función auxiliar para alertas
    function mostrarAlerta(tipo, mensaje) {
        mensajeAlerta.innerHTML = `
            <div class="alert alert-${tipo} alert-dismissible fade show shadow-sm" role="alert">
                ${mensaje}
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        `;
    }
});