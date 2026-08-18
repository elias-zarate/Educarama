using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class CategoriaRama
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Rama> Rama { get; set; } = new List<Rama>();
}
