using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class EsFamilia
{
    public int IdProtagonista { get; set; }

    public int IdFamiliar { get; set; }

    public string? Vinculo { get; set; }

    public bool? EsContactoDeEmergencia { get; set; }
}
