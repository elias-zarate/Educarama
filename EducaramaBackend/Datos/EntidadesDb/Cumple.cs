using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Cumple
{
    public int IdProtagonista { get; set; }

    public int IdAccion { get; set; }

    public bool AccionCumplida { get; set; }
}
