using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Repositorio;
using System.Threading.Tasks;

namespace Aplicacion.Protagonistas.CasosDeUso;

public class EliminarProtagonistaUseCase : IEliminarUseCase
{
    private readonly IEliminarRepositorio _repositorio;

    public EliminarProtagonistaUseCase(IEliminarRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task EliminarAsync(int id)
    {
        await _repositorio.EliminarAsync(id);
    }
}