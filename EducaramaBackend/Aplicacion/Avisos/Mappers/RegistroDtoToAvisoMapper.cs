using Aplicacion.Abstracciones;
using Aplicacion.Avisos.DTOs;
using Dominio;
using System;

namespace Aplicacion.Avisos.Mappers;

public class RegistroDtoToAvisoMapper : IMapper<RegistroAvisoDTO, AvisoEntity>
{
    public AvisoEntity Map(RegistroAvisoDTO input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        return new AvisoEntity
        {
            Titulo = input.Titulo,
            Descripcion = input.Descripcion,
            FechaInicio = input.FechaInicio,
            FechaFin = input.FechaFin,
            Prioridad = input.Prioridad,
            Activo = true
        };
    }
}