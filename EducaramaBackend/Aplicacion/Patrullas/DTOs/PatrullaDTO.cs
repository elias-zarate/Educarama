using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Patrullas.DTOs;

public class PatrullaDTO
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public int IdRama { get; set; }
}
