const Auth = {
    login: async (email, contrasenia) => {
        // 1. Buscamos la config
        const configRes = await fetch('../config.json');
        const config = await configRes.json();
        
        // 2. Le pegamos al endpoint
        const response = await fetch(`${config.apiUrl}/auth/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, contrasenia })
        });
        const data = await response.json();
        
        // 3. Si hay error, lo lanzamos para que la vista lo atrape
        if (!response.ok) {
            throw new Error(data.error || 'Credenciales inválidas');
        }
        
        // 4. Si sale bien, guardamos la sesión
        sessionStorage.setItem('usuarioLogueado', JSON.stringify(data.usuario));
        return data.usuario;
    },

    logout: async (basePath = '../') => {
        try {
            const usuarioString = sessionStorage.getItem('usuarioLogueado');
            if (usuarioString) {
                const usuario = JSON.parse(usuarioString);
                
                // Cargamos la config usando el basePath para asegurar la ruta correcta
                const configRes = await fetch(`${basePath}config.json`);
                const config = await configRes.json();

                // Notificamos al backend que se cerró sesión
                await fetch(`${config.apiUrl}/auth/logout`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ idUsuario: usuario.idUsuario || usuario.id })
                });
            }
        } catch (error) {
            console.error('Error al notificar cierre de sesión al servidor:', error);
        } finally {
            // Siempre limpiamos el sessionStorage, pase lo que pase con la API
            sessionStorage.removeItem('usuarioLogueado');
        }
    }
};