using System;
using System.Linq;
using Aplicacion.Abstracciones;
using Aplicacion.Usuarios.DTOs;
using Dominio;

namespace Aplicacion.Usuarios.Mappers;

public class UsuarioEntityToPerfilDtoMapper : IMapper<UsuarioEntity, PerfilUsuarioDTO>
{
    public PerfilUsuarioDTO Map(UsuarioEntity entidad)
    {
        if (entidad == null) throw new ArgumentNullException(nameof(entidad));

        // Validación defensiva: Obtenemos el primer rol o creamos uno vacío por defecto
        var asignacion = entidad.AsignacionesRoles?.FirstOrDefault() ?? new AsignacionRolEntity { IdRol = 0 };
        
        return new PerfilUsuarioDTO
        {

            Nombre = entidad.Nombre,
            Email = entidad.Email,
            Contrasenia = entidad.Contrasenia,

            IdRol = asignacion.IdRol,
            IdGrupo = asignacion.IdGrupo,
            IdRama = asignacion.IdRama
        };
    }
}