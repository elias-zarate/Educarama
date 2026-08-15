using System;
using Aplicacion.Abstracciones;
using Aplicacion.Usuarios.DTOs;
using Dominio;

namespace Aplicacion.Usuarios.Mappers;

public class RegistroDtoToUsuarioMapper : IMapper<RegistroUsuarioDTO, UsuarioEntity>
{
    public UsuarioEntity Map(RegistroUsuarioDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        return new UsuarioEntity
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            Contrasenia = dto.Contrasenia
        };
    }
}