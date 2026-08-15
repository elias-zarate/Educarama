using Aplicacion.Abstracciones;
using Aplicacion.CategoriaRamas.DTOs;
using Dominio;

namespace Aplicacion.CategoriaRamas.Mappers;

public class CategoriaRamaEntityToDtoMapper : IMapper<CategoriaRamaEntity, CategoriaRamaDTO>
{
    public CategoriaRamaDTO Map(CategoriaRamaEntity input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input), 
                "La categoría de rama no existe o es nula.");

        return new CategoriaRamaDTO
        {
            Id = input.Id,
            Nombre = input.Nombre
        };
    }
}
