using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Desarrolla
{
    public int IdProtagonista { get; set; }

    public int IdCompetencia { get; set; }

    public int CodigoCompetencia { get; set; }

    public bool? CompetenciaDesarrollada { get; set; }
}
