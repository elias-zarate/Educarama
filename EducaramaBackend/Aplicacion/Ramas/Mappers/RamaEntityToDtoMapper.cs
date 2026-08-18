using Aplicacion.Ramas.DTOs;
using Dominio;

namespace Aplicacion.Ramas.Mappers;

public static class RamaEntityToDtoMapper
{
    public static RamaDTO ToDto(RamaEntity entity)
    {
        return new RamaDTO
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            IdGrupo = entity.IdGrupo,
            IdCategoriaRama = entity.IdCategoriaRama
        };
    }
}