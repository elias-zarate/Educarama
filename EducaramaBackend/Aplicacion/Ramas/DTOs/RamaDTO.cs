namespace Aplicacion.Ramas.DTOs;

public class RamaDTO
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public int? IdGrupo { get; set; }
    public int IdCategoriaRama { get; set; }
    public string? CodigoAcceso { get; set; } // <-- Agregado
}