namespace Aplicacion.Familiares.DTOs;

public class FamiliarPerfilDTO
{
    public int IdFamiliar { get; set; }
    public int IdPersona { get; set; }
    public int Dni { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? EmailContacto { get; set; }
    public string? TelefonoContacto { get; set; }

    // Datos del vínculo específico con el protagonista consultado
    public string? Vinculo { get; set; }
    public bool EsContactoDeEmergencia { get; set; }
}