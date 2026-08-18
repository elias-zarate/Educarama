using System;
using System.Threading.Tasks;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Usuarios.DTOs;
using Dominio;

namespace Aplicacion.Usuarios.CasosDeUso;

public class ActualizarPerfilUsuarioUseCase : IActualizarUseCase<PerfilUsuarioDTO>
{
    private readonly IActualizarRepositorio<UsuarioEntity> _usuarioRepositorio;
    private readonly IActualizarRepositorio<AsignacionRolEntity> _asignacionRepositorio;

    public ActualizarPerfilUsuarioUseCase(
        IActualizarRepositorio<UsuarioEntity> usuarioRepositorio,
        IActualizarRepositorio<AsignacionRolEntity> asignacionRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _asignacionRepositorio = asignacionRepositorio;
    }

    public async Task ActualizarAsync(PerfilUsuarioDTO dto, int idUsuario)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var usuario = new UsuarioEntity
        {
            Id = idUsuario, // Usamos el ID de la ruta
            Nombre = dto.Nombre,
            Email = dto.Email,
            Contrasenia = dto.Contrasenia
        };

        var asignacion = new AsignacionRolEntity
        {
            Id = dto.IdAsignacionRol,
            IdUsuario = idUsuario,
            IdRol = dto.IdRol,
            IdGrupo = dto.IdGrupo,
            IdRama = dto.IdRama
        };

        await _usuarioRepositorio.ActualizarAsync(usuario);
        await _asignacionRepositorio.ActualizarAsync(asignacion);
    }
}