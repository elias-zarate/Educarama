using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Insignias.DTOs;

public class ObtenerInsigniaDTO
{
    public int Id { get; set; }
    public int IdInsignia { get; set; }
    public DateTime FechaObtencion { get; set; }
}
