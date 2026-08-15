using Aplicacion.Abstracciones;
using Aplicacion.Cumple.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Cumple.Mappers;

public class CumpleDtoToEntityMapper : IMapper<CumpleDTO, CumpleEntity>
{
    public CumpleEntity Map(CumpleDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "El DTO a mapear está vacío. " +
                "No se puede convertir a entidad de dominio.");
        
        return new CumpleEntity
        {
            IdProtagonista = dto.IdProtagonista,
            IdAccion = dto.IdAccion,
            AccionCumplida = dto.AccionCumplida ?? false
        };
    }
}
