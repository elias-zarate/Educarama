using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Patrulla
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdRama { get; set; }
}
