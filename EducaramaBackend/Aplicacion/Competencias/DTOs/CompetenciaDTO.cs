namespace Aplicacion.Competencias.DTOs;

public class CompetenciaDTO
{
    public int Id { get; set; }
    public int Codigo { get; set; }
    public required string Descripcion { get; set; }
    public int IdCategoriaRama { get; set; }
    public int IdSendero { get; set; }
}