using Aplicacion.Abstracciones;
using Aplicacion.Insignias.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Insignias.Mappers;

public class ObtenerInsigniaDtoToEntityMapper : IMapper<ObtenerInsigniaDTO, ObtieneEntity>
{
    public ObtieneEntity Map(ObtenerInsigniaDTO insigniaDto)
    {
        if (insigniaDto == null)
            throw new ArgumentNullException(nameof(insigniaDto), 
                "La insignia no existe o los datos son nulos.");

        var insigniaEntity = new ObtieneEntity
        {
            IdProtagonista = insigniaDto.Id,
            IdInsignia = insigniaDto.IdInsignia,
            Fecha = DateOnly.FromDateTime(insigniaDto.FechaObtencion)
        };

        return insigniaEntity;
    }
}

