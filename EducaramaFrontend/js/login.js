document.addEventListener('DOMContentLoaded', function () {
    const formulario = document.querySelector('#formLogin');
    
    if (!formulario) return;

    formulario.addEventListener('submit', async function (e) {
        e.preventDefault();

        const email = document.querySelector('#email').value.trim();
        const password = document.querySelector('#password').value.trim();

        try {
            // 1. Cargamos la URL base desde config.json
            const configRes = await fetch('../config.json');
            const config = await configRes.json();
            const apiUrl = config.apiUrl;

            // 2. Hacemos el POST al endpoint de autenticación que armamos en C#
            const response = await fetch(`${apiUrl}/auth/login`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ email: email, contrasenia: password })
            });

            const data = await response.json();

            if (response.ok) {
                // Guardamos los datos del usuario en sessionStorage para la sesión del cliente
                sessionStorage.setItem('usuario', JSON.stringify(data.usuario));
                
                // Redirigimos al dashboard principal
                window.location.href = '../index.html';
            } else {
                alert(data.error || 'No se pudo iniciar sesión.');
            }
        } catch (error) {
            console.error('Error de red:', error);
            alert('Ocurrió un error al intentar comunicarse con el servidor.');
        }
    });
});