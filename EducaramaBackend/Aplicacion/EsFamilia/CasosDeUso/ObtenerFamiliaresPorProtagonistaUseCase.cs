using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Familiares.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Familiares.CasosDeUso;

public class ObtenerFamiliaresPorProtagonistaUseCase
{
    private readonly IEsFamiliaRepositorio _esFamiliaRepositorio;

    public ObtenerFamiliaresPorProtagonistaUseCase(IEsFamiliaRepositorio esFamiliaRepositorio)
    {
        _esFamiliaRepositorio = esFamiliaRepositorio;
    }

    public async Task<IEnumerable<FamiliarPerfilDTO>> EjecutarAsync(int idProtagonista)
    {
        return await _esFamiliaRepositorio.ObtenerPorProtagonistaAsync(idProtagonista);
    }
}