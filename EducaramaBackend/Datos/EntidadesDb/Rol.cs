using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Rol
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<AsignacionRol> AsignacionRol { get; set; } = new List<AsignacionRol>();
}
