using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Grupo
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string? Nombre { get; set; }

    public virtual ICollection<AsignacionRol> AsignacionRol { get; set; } = new List<AsignacionRol>();

    public virtual ICollection<Rama> Rama { get; set; } = new List<Rama>();
}
