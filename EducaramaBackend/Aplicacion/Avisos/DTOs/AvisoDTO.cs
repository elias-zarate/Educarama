using System;

namespace Aplicacion.Avisos.DTOs;

public class AvisoDTO
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descripcion { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public required string Prioridad { get; set; }
    public bool Activo { get; set; }
}