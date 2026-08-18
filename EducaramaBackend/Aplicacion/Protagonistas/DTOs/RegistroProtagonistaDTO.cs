using System;

namespace Aplicacion.Protagonistas.DTOs;

public class RegistroProtagonistaDTO
{
    // Datos de la Persona
    public int Dni { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public DateOnly FechaNacimiento { get; set; }

    // Datos del Protagonista (Scout)
    public bool? Promesa { get; set; }
    public int IdRama { get; set; }
    public int? IdPatrulla { get; set; }
    public int? IdUsuario { get; set; }
}