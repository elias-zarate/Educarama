using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace Aplicacion.Cumple.DTOs;

public class CumpleDTO
{
    public int IdProtagonista { get; set; }
    public int IdAccion { get; set; }
    public bool? AccionCumplida { get; set; }
}
