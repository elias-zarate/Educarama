namespace Aplicacion.Acciones.DTOs;

public class AccionDTO
{
    public int Id { get; set; }
    public required string Descripcion { get; set; }
    public int IdCompetencia { get; set; }
    public int CodigoCompetencia { get; set; }
    public int IdTipoAccion { get; set; }
}