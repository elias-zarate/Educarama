namespace Aplicacion.Ramas.DTOs;

public class RegistroRamaDTO
{
    public required string Nombre { get; set; }
    public int IdGrupo { get; set; }
    public int IdRamaVinculada { get; set; }
}
