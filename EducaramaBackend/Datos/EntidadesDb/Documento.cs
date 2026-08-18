using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Documento
{
    public int Id { get; set; }

    public string? TipoDocumento { get; set; }

    public string? Descripcion { get; set; }

    public int IdProtagonista { get; set; }
}
