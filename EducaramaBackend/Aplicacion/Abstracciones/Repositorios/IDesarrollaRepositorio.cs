using Aplicacion.Desarrolla.DTOs;
using Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Abstracciones.Repositorios;

public interface IDesarrollaRepositorio
{
    // Para asignar una nueva competencia al protagonista
    Task AgregarAsync(DesarrollaEntity entidad);

    // Para marcar la competencia como terminada (true) o volverla a en curso (false)
    Task ActualizarEstadoAsync(int idProtagonista, int idCompetencia, bool estado);

    // Para traer la "tarjeta" completa con sus acciones y tildes
    Task<IEnumerable<CompetenciaEnDesarrolloDTO>> ObtenerPorProtagonistaAsync(int idProtagonista);
}