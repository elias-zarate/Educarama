using System;

namespace Aplicacion.Familiares.DTOs;

public class RegistroFamiliarCompletoDTO
{
    // Datos de la Persona
    public int Dni { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public DateOnly FechaNacimiento { get; set; }

    // Datos del Familiar
    public string? EmailContacto { get; set; }
    public required string TelefonoContacto { get; set; }

    // Datos de la relación (EsFamilia)
    public int IdProtagonista { get; set; }
    public required string Vinculo { get; set; } // Ej: "Padre", "Madre", "Tutor"
    public bool EsContactoDeEmergencia { get; set; }
}