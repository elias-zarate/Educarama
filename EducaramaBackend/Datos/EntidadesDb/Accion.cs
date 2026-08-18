using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Accion
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public int IdCompetencia { get; set; }

    public int CodigoCompetencia { get; set; }

    public int IdTipoAccion { get; set; }
}
