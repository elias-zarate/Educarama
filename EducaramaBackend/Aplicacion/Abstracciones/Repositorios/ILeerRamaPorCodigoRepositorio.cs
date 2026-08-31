using Dominio;
using System.Threading.Tasks;

namespace Aplicacion.Abstracciones.Repositorios;

public interface ILeerRamaPorCodigoRepositorio
{
    Task<RamaEntity?> ObtenerPorCodigoAsync(string codigo);
}