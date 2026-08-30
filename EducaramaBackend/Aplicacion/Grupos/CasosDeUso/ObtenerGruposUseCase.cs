using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Grupos.DTOs;
using Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Grupos.CasosDeUso;

public class ObtenerGruposUseCase : ILeerUseCase<GrupoDTO>
{
    private readonly ILeerRepositorio<GrupoEntity> _repositorio;

    public ObtenerGruposUseCase(ILeerRepositorio<GrupoEntity> repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<GrupoDTO?> ObtenerPorIdAsync(int id)
    {
        var grupo = await _repositorio.ObtenerPorIdAsync(id);

        if (grupo == null) return null;

        // Mapeo manual simple (sin usar IMapper extra)
        return new GrupoDTO
        {
            Id = grupo.Id,
            Codigo = grupo.Codigo,
            Nombre = grupo.Nombre
        };
    }

    public async Task<IEnumerable<GrupoDTO>> ObtenerTodosAsync()
    {
        var grupos = await _repositorio.ObtenerTodosAsync();

        // Mapeo manual simple de la lista
        return grupos.Select(g => new GrupoDTO
        {
            Id = g.Id,
            Codigo = g.Codigo,
            Nombre = g.Nombre
        });
    }
}