namespace Dominio;

public class AsignacionRolEntity
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public int IdRol { get; set; }
    public int? IdGrupo { get; set; }
    public int? IdRama { get; set; }
}