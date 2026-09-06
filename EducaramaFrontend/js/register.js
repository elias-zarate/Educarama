document.addEventListener('DOMContentLoaded', () => {
    // 1. Usamos los IDs exactos de tu HTML
    const formRegistro = document.getElementById('registroForm');
    const mensajeAlerta = document.getElementById('mensajeAlerta');

    if (formRegistro) {
        formRegistro.addEventListener('submit', async (e) => {
            e.preventDefault();
            
            // 2. Capturamos los valores de los inputs de tu HTML
            const nombre = document.getElementById('nombre').value.trim();
            const email = document.getElementById('email').value.trim();
            const contrasenia = document.getElementById('contrasenia').value;
            
            // Capturamos el botón usando querySelector porque no tiene ID en tu HTML
            const btnSubmit = formRegistro.querySelector('button[type="submit"]');

            try {
                // Estado de carga
                btnSubmit.innerText = 'Registrando...';
                btnSubmit.disabled = true;
                if (mensajeAlerta) mensajeAlerta.innerHTML = '';

                // 3. Cargamos la URL de la API desde la config
                const configRes = await fetch('../config.json'); // Ajustá la ruta según donde esté
                const config = await configRes.json();

                // 4. Armamos el DTO
                const payload = {
                    nombre: nombre,
                    email: email,
                    contrasenia: contrasenia
                };

                // 5. Le pegamos al endpoint POST /api/usuarios
                const response = await fetch(`${config.apiUrl}/usuarios`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(payload)
                });

                const data = await response.json();

                if (!response.ok) {
                    throw new Error(data.error || 'No se pudo completar el registro. Verifique los datos.');
                }

                // 6. Éxito: Guardamos el usuario en sessionStorage
                const usuarioLogueado = data.usuario ? data.usuario : data;
                sessionStorage.setItem('usuarioLogueado', JSON.stringify(usuarioLogueado));
                
                mostrarAlerta('success', '¡Registro exitoso! Llevándote a vincular tu grupo...');
                
                // 7. Redirección automática a la vista de unirse al grupo (SIEMPRE, porque es nuevo)
                setTimeout(() => {
                    window.location.href = 'unirse_grupo.html';
                }, 1500);

            } catch (error) {
                console.error('Error al registrar:', error);
                mostrarAlerta('danger', error.message);
            } finally {
                // Restauramos el botón
                btnSubmit.innerText = 'Registrarse';
                btnSubmit.disabled = false;
            }
        });
    }

    function mostrarAlerta(tipo, mensaje) {
        if (!mensajeAlerta) return;
        mensajeAlerta.innerHTML = `
            <div class="alert alert-${tipo} alert-dismissible fade show" role="alert">
                ${mensaje}
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        `;
    }
});