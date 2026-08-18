using Aplicacion.Patrullas.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Abstracciones.CasosDeUso;

public interface ILeerPatrullasPorRamaUseCase
{
    Task<IEnumerable<PatrullaDTO>> ObtenerPatrullasPorRamaAsync(int idRama);
}
