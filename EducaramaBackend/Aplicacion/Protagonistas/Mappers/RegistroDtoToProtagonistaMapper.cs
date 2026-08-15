using System;
using Aplicacion.Abstracciones;
using Aplicacion.Protagonistas.DTOs;
using Dominio;

namespace Aplicacion.Protagonistas.Mappers;

public class RegistroDtoToProtagonistaMapper : IMapper<RegistroProtagonistaDTO, ProtagonistaEntity>
{
    public ProtagonistaEntity Map(RegistroProtagonistaDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        return new ProtagonistaEntity
        {
            Promesa = dto.Promesa,
            IdRama = dto.IdRama,
            IdPatrulla = dto.IdPatrulla,
            IdUsuario = dto.IdUsuario
        };
    }
}