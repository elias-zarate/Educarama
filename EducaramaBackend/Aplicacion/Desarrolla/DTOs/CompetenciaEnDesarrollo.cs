using System.Collections.Generic;

namespace Aplicacion.Desarrolla.DTOs;

public class CompetenciaEnDesarrolloDTO
{
    public int IdCompetencia { get; set; }
    public int Codigo { get; set; }
    public string? Descripcion { get; set; }

    // True = Ya la cumplió (Historial) | False/Null = En curso
    public bool Terminada { get; set; }

    // La lista de acciones a renderizar con el checkbox al costado
    public List<AccionConEstadoDTO> Acciones { get; set; } = new();
}