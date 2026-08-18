using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Familiar
{
    public int Id { get; set; }

    public int? TelefonoContacto { get; set; }

    public string? EmailContacto { get; set; }

    public int? IdPersona { get; set; }
}
