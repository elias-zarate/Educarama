namespace Aplicacion.Familiares.DTOs;

public class AsignarFamiliarExistenteDTO
{
    public int IdFamiliar { get; set; }
    public int IdProtagonista { get; set; }
    public required string Vinculo { get; set; }
    public bool EsContactoDeEmergencia { get; set; }
}