namespace Aplicacion.Usuarios.DTOs;

public class CambiarContraseniaDTO
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NuevaContrasenia { get; set; } = string.Empty;
}