namespace Aplicacion.Desarrolla.DTOs;

public class AccionConEstadoDTO
{
    public int IdAccion { get; set; }
    public string? Descripcion { get; set; }
    public int IdTipoAccion { get; set; }

    // Acá mapeamos el tilde que pediste (true si existe el registro en la tabla Cumple)
    public bool Cumplida { get; set; }
}