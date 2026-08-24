using Aplicacion.Abstracciones;
using Aplicacion.Competencias.DTOs;
using Dominio;

namespace Aplicacion.Competencias.Mappers;

public class CompetenciaEntityToDtoMapper : IMapper<CompetenciaEntity, CompetenciaDTO>
{
    public CompetenciaDTO Map(CompetenciaEntity input)
    {
        return new CompetenciaDTO
        {
            Id = input.Id,
            Codigo = input.Codigo,
            Descripcion = input.Descripcion,
            IdCategoriaRama = input.IdCategoriaRama,
            IdSendero = input.IdSendero
        };
    }
}