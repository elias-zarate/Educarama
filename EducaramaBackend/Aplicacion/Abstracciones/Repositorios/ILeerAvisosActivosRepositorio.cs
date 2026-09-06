using Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Abstracciones.Repositorios;

public interface ILeerAvisosActivosRepositorio
{
    Task<IEnumerable<AvisoEntity>> ObtenerActivosAsync();
}