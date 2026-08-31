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
        var asignacion = entidad.AsignacionesRoles?.FirstOrDefault();

        var dto = new PerfilUsuarioDTO
        {
            IdUsuario = entidad.Id,
            Nombre = entidad.Nombre,
            Email = entidad.Email,
            Contrasenia = entidad.Contrasenia,
            // ACÁ ESTABA EL DETALLE: tiene que sacar los IDs de la asignación relacionada
            IdAsignacionRol = asignacion?.Id ?? 0,
            IdRol = asignacion?.IdRol ?? 0,
            IdGrupo = asignacion?.IdGrupo, // Si es int? (nullable), pasa directo
            IdRama = asignacion?.IdRama
        };

        return dto;
    }
}