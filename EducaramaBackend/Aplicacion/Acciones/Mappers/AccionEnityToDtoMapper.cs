using Aplicacion.Abstracciones;
using Aplicacion.Acciones.DTOs;
using Dominio;

namespace Aplicacion.Acciones.Mappers;

public class AccionEntityToDtoMapper : IMapper<AccionEntity, AccionDTO>
{
    public AccionDTO Map(AccionEntity input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input), "La acción no existe o es nula.");

        return new AccionDTO
        {
            Id = input.Id,
            Descripcion = input.Descripcion,
            IdCompetencia = input.IdCompetencia,
            CodigoCompetencia = input.CodigoCompetencia,
            IdTipoAccion = input.IdTipoAccion
        };
    }
}