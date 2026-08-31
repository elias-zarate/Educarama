namespace Aplicacion.Usuarios.DTOs;

public class UnirseGrupoDTO
{
    public int IdUsuario { get; set; }
    public required string CodigoGrupo { get; set; }
    public int IdRol { get; set; } // Acá mandás el 2 si es Jefe de Grupo, etc.
}