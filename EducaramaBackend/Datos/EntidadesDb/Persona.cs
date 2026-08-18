using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Persona
{
    public int Id { get; set; }

    public int Dni { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }
}
