using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class AsignacionRol
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public int? IdGrupo { get; set; }

    public int IdRama { get; set; }

    public virtual Grupo? IdGrupoNavigation { get; set; }

    public virtual Rama IdRamaNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
