document.addEventListener('DOMContentLoaded', function () {
    const formulario = document.querySelector('#formLogin');
    
    if (!formulario) return;

    formulario.addEventListener('submit', async function (e) {
        e.preventDefault();

        const email = document.querySelector('#email').value.trim();
        const contrasenia = document.querySelector('#password').value.trim();
        const botonSubmit = formulario.querySelector('button[type="submit"]');

        try {
            // Feedback visual mientras carga
            botonSubmit.innerText = 'Iniciando...';
            botonSubmit.disabled = true;

            // Llamamos al motor que creamos en auth.js y guardamos el usuario que nos devuelve
            const usuario = await Auth.login(email, contrasenia);
            
            // Verificamos si el usuario tiene asignado algún rol (y por ende, un grupo)
            // Revisamos ambas convenciones de nombres por las dudas (CamelCase o PascalCase desde .NET)
            const asignaciones = usuario.asignacionesRoles || usuario.asignacionRol || [];
            const tieneGrupoORoles = asignaciones.length > 0;

            if (!tieneGrupoORoles) {
                // Si la lista de roles está vacía, significa que nunca completó el segundo paso
                window.location.href = 'unirse_grupo.html';
            } else {
                // Si ya tiene rol y grupo, lo dejamos pasar al sistema
                window.location.href = 'dashboard.html';
            }

        } catch (error) {
            console.error('Error:', error);
            alert(error.message); // Muestra el error exacto ("Credenciales inválidas")
        } finally {
            // Restauramos el botón
            botonSubmit.innerText = 'Iniciar Sesión';
            botonSubmit.disabled = false;
        }
    });
});