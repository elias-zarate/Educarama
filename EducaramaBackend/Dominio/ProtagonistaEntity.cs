namespace Dominio;

public class ProtagonistaEntity
{
    public int Id { get; set; }
    public bool? Promesa { get; set; }
    public int IdPersona { get; set; }
    public int IdRama { get; set; }
    public int? IdPatrulla { get; set; }
    public int? IdUsuario { get; set; }
    public virtual PersonaEntity? Persona { get; set; }
}