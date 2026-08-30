namespace Aplicacion.Grupos.DTOs;

public class RegistroGrupoDTO
{
    public required string NombreGrupo { get; set; }
    public int IdUsuario { get; set; } // El usuario que se está registrando como Jefe
}