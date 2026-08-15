using Aplicacion.Abstracciones;
using Aplicacion.Patrullas.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Patrullas.Mappers;

public class PatrullaEntityToDtoMapper : IMapper<PatrullaEntity, PatrullaDTO>
{
    public PatrullaDTO Map(PatrullaEntity entidad)
    {
        if(entidad == null)
            throw new ArgumentNullException(nameof(entidad), "La patrulla no puede ser nula");
        
        return new PatrullaDTO
        {
            Id = entidad.Id,
            Nombre = entidad.Nombre
        };
    }
}
