using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Rama
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdGrupo { get; set; }

    public int IdCategoriaRama { get; set; }

    public virtual ICollection<AsignacionRol> AsignacionRol { get; set; } = new List<AsignacionRol>();

    public virtual CategoriaRama IdCategoriaRamaNavigation { get; set; } = null!;

    public virtual Grupo IdGrupoNavigation { get; set; } = null!;
}
