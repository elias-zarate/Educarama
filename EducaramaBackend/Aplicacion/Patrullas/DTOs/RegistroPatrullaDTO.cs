namespace Aplicacion.Patrullas.DTOs;

public class RegistroPatrullaDTO
{
    public required string Nombre { get; set; }
    public int IdRama { get; set; }
    public int IdUsuarioLogueado { get; set; }
}