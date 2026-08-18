using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public virtual ICollection<AsignacionRol> AsignacionRol { get; set; } = new List<AsignacionRol>();
}
