using Aplicacion.Abstracciones;
using Aplicacion.Avisos.DTOs;
using Dominio;
using System;

namespace Aplicacion.Avisos.Mappers;

public class AvisoEntityToDtoMapper : IMapper<AvisoEntity, AvisoDTO>
{
    public AvisoDTO Map(AvisoEntity input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        return new AvisoDTO
        {
            Id = input.Id,
            Titulo = input.Titulo,
            Descripcion = input.Descripcion,
            FechaInicio = input.FechaInicio,
            FechaFin = input.FechaFin,
            Prioridad = input.Prioridad,
            Activo = input.Activo
        };
    }
}