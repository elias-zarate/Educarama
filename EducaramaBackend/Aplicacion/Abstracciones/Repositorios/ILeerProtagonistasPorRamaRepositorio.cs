using Dominio;

namespace Aplicacion.Abstracciones.Repositorios;

public interface ILeerProtagonistasPorRamaRepositorio
{
    Task<IEnumerable<ProtagonistaEntity>> ObtenerPorRamaAsync(int idRama);
}
