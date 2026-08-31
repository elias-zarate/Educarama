using Dominio;
using System.Threading.Tasks;

namespace Aplicacion.Abstracciones.Repositorios;

public interface ILeerGrupoPorCodigoRepositorio
{
    Task<GrupoEntity?> ObtenerPorCodigoAsync(string codigo);
}