using Aplicacion.Abstracciones.CasosDeUso;
using Repositorio;
using System.Threading.Tasks;

namespace Aplicacion.Patrullas.CasosDeUso;

public class EliminarPatrullaUseCase : IEliminarUseCase
{
    private readonly IEliminarRepositorio _repositorio;

    public EliminarPatrullaUseCase(IEliminarRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task EliminarAsync(int id)
    {
        await _repositorio.EliminarAsync(id);
    }
}