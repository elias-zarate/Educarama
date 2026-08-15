using Aplicacion.Abstracciones;
using Aplicacion.Ramas.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Ramas.Mappers;

public class RegistroRamaDtoToEntityMapper : IMapper<RegistroRamaDTO, RamaEntity>
{
    public RamaEntity Map(RegistroRamaDTO dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }
        return new RamaEntity
        {
            Nombre = dto.Nombre,
            IdGrupo = dto.IdGrupo,
            IdRamaVinculada = dto.IdRamaVinculada
        };
    }
}
