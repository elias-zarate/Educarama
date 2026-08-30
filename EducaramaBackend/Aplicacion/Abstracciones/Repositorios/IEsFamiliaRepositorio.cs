using Aplicacion.Familiares.DTOs;
using Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Abstracciones.Repositorios;

public interface IEsFamiliaRepositorio
{
    Task AgregarAsync(EsFamiliaEntity entidad);
    Task EliminarVinculoAsync(int idProtagonista, int idFamiliar);
    Task<IEnumerable<FamiliarPerfilDTO>> ObtenerPorProtagonistaAsync(int idProtagonista);
}