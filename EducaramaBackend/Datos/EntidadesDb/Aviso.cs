using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Aviso
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string Prioridad { get; set; } = null!;

    public bool Activo { get; set; }
}
