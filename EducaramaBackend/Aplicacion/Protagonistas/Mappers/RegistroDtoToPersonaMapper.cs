using System;
using Aplicacion.Abstracciones;
using Aplicacion.Protagonistas.DTOs;
using Dominio;

namespace Aplicacion.Protagonistas.Mappers;

public class RegistroDtoToPersonaMapper : IMapper<RegistroProtagonistaDTO, PersonaEntity>
{
    public PersonaEntity Map(RegistroProtagonistaDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        return new PersonaEntity
        {
            Dni = dto.Dni,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            FechaNacimiento = dto.FechaNacimiento
        };
    }
}