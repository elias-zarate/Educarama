using Aplicacion.Abstracciones.Repositorios;
using System.Threading.Tasks;

namespace Aplicacion.Familiares.CasosDeUso;

public class EliminarVinculoFamiliarUseCase
{
    private readonly IEsFamiliaRepositorio _repositorio;

    public EliminarVinculoFamiliarUseCase(IEsFamiliaRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task EjecutarAsync(int idProtagonista, int idFamiliar)
    {
        await _repositorio.EliminarVinculoAsync(idProtagonista, idFamiliar);
    }
}