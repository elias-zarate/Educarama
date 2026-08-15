using System;
using Aplicacion.Abstracciones;
using Aplicacion.Usuarios.DTOs;
using Dominio;

namespace Aplicacion.Usuarios.Mappers;

public class RegistroDtoToAsignacionRolMapper : IMapper<RegistroUsuarioDTO, AsignacionRolEntity>
{
    public AsignacionRolEntity Map(RegistroUsuarioDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        return new AsignacionRolEntity
        {
            IdRol = dto.IdRol,
            IdGrupo = dto.IdGrupo,
            IdRama = dto.IdRama
            // El IdUsuario se lo asignamos en el caso de uso
        };
    }
}