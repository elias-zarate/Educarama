using System.Threading.Tasks;
using Aplicacion.Usuarios.DTOs;

namespace Aplicacion.Usuarios.CasosDeUso;

public class CerrarSesionUseCase
{
    public CerrarSesionUseCase()
    {
        // El constructor está vacío por ahora. 
        // En el futuro, acá podés inyectar repositorios como IAuditoriaRepositorio o ITokenBlacklistRepositorio.
    }

    public async Task CerrarSesionAsync(LogoutDTO dto)
    {
        if (dto == null || dto.IdUsuario <= 0)
        {
            throw new System.ArgumentException("Datos de usuario inválidos para el cierre de sesión.");
        }

        // Lógica futura: guardar en base de datos la fecha/hora de salida o invalidar token.
        // Como actualmente la sesión vive en el navegador (sessionStorage), solo simulamos el proceso exitoso.
        await Task.CompletedTask;
    }
}