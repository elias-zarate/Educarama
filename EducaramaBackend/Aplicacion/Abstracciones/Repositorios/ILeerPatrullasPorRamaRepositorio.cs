using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Abstracciones.Repositorios;

public interface ILeerPatrullasPorRamaRepositorio
{
    Task<IEnumerable<PatrullaEntity>> ObtenerPatrullasPorRamaAsync(int idRama);
}
