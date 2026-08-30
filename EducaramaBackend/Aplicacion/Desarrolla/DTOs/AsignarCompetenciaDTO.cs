namespace Aplicacion.Desarrolla.DTOs;

public class AsignarCompetenciaDTO
{
    public int IdProtagonista { get; set; }
    public int IdCompetencia { get; set; }

    // Opcional: El backend puede buscar el CodigoCompetencia solo, 
    // pero si el front ya lo tiene, lo recibimos para evitar una consulta extra.
    public int CodigoCompetencia { get; set; }
}