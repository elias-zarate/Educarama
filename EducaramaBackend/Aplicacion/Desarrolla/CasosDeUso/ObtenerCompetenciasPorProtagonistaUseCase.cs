using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Desarrolla.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Desarrolla.CasosDeUso;

public class ObtenerCompetenciasPorProtagonistaUseCase
{
    private readonly IDesarrollaRepositorio _desarrollaRepositorio;

    public ObtenerCompetenciasPorProtagonistaUseCase(IDesarrollaRepositorio desarrollaRepositorio)
    {
        _desarrollaRepositorio = desarrollaRepositorio;
    }

    public async Task<IEnumerable<CompetenciaEnDesarrolloDTO>> EjecutarAsync(int idProtagonista)
    {
        return await _desarrollaRepositorio.ObtenerPorProtagonistaAsync(idProtagonista);
    }
}