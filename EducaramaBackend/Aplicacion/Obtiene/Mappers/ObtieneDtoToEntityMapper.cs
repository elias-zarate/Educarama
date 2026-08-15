using System;
using Aplicacion.Abstracciones;
using Aplicacion.Obtiene.DTOs;
using Dominio;

namespace Aplicacion.Obtiene.Mappers;

public class ObtieneDtoToEntityMapper : IMapper<ObtieneDTO, ObtieneEntity>
{
    public ObtieneEntity Map(ObtieneDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        return new ObtieneEntity
        {
            IdProtagonista = dto.IdProtagonista,
            IdInsignia = dto.IdInsignia,
            // Si el front no manda fecha, le asignamos la fecha de hoy por defecto
            Fecha = dto.Fecha ?? DateOnly.FromDateTime(DateTime.Today)
        };
    }
}