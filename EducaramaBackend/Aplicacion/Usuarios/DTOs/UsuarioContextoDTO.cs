namespace Aplicacion.Usuarios.DTOs;

public class UsuarioContextoDTO
{
    public int IdUsuario { get; set; }
    public int IdRol { get; set; }
    public int? IdGrupo { get; set; }
    public int? IdRama { get; set; }

    // Propiedades calculadas útiles para las reglas de negocio (usamos los números del enum RolesSistema)
    public bool EsJefeDeGrupo => IdRol == 2;
    public bool EsRolLimitado => IdRol == 3 || IdRol == 4; // Jefe de Rama o Educador
}