using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Ramas.DTOs;
using Aplicacion.Ramas.Mappers;
using Dominio;

namespace Aplicacion.Ramas.CasosDeUso;

public class ObtenerRamasUseCase : ILeerUseCase<RamaDTO>
{
    private readonly ILeerRepositorio<RamaEntity> _repositorio;

    public ObtenerRamasUseCase(ILeerRepositorio<RamaEntity> repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<RamaDTO?> ObtenerPorIdAsync(int id) // <-- Agregado el ?
    {
        var entity = await _repositorio.ObtenerPorIdAsync(id);
        return entity != null ? RamaEntityToDtoMapper.ToDto(entity) : null;
    }

    public async Task<IEnumerable<RamaDTO>> ObtenerTodosAsync()
    {
        var entities = await _repositorio.ObtenerTodosAsync();
        return entities.Select(RamaEntityToDtoMapper.ToDto);
    }
}