using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Protagonista
{
    public int Id { get; set; }

    public bool? Promesa { get; set; }

    public int IdPersona { get; set; }

    public int IdRama { get; set; }

    public int? IdPatrulla { get; set; }

    public int? IdUsuario { get; set; }
}
