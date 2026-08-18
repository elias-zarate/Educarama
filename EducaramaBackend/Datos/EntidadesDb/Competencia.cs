using System;
using System.Collections.Generic;

namespace Datos.EntidadesDb;

public partial class Competencia
{
    public int Id { get; set; }

    public int Codigo { get; set; }

    public string? Descripcion { get; set; }

    public int IdCategoriaRama { get; set; }

    public int IdSendero { get; set; }
}
