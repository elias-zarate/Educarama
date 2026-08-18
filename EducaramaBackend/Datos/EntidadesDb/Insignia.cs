using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Insignia
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int IdCategoriaRama { get; set; }
}
