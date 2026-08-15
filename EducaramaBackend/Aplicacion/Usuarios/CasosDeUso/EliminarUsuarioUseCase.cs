using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Repositorio;
using System.Threading.Tasks;

namespace Aplicacion.Usuarios.CasosDeUso;

public class EliminarUsuarioUseCase : IEliminarUseCase
{
    private readonly IEliminarRepositorio _repositorio;

    public EliminarUsuarioUseCase(IEliminarRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task EliminarAsync(int id)
    {
        await _repositorio.EliminarAsync(id);
    }
}