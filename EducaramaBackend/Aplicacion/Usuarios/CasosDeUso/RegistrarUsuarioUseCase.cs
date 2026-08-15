using System;
using System.Threading.Tasks;
using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Usuarios.DTOs;
using Dominio;

namespace Aplicacion.Usuarios.CasosDeUso;

public class RegistrarUsuarioUseCase : ICrearUseCase<RegistroUsuarioDTO>
{
    private readonly ICrearRepositorio<UsuarioEntity> _usuarioRepositorio;
    private readonly ICrearRepositorio<AsignacionRolEntity> _asignacionRepositorio;
    private readonly IMapper<RegistroUsuarioDTO, UsuarioEntity> _usuarioMapper;
    private readonly IMapper<RegistroUsuarioDTO, AsignacionRolEntity> _asignacionMapper;

    public RegistrarUsuarioUseCase(
        ICrearRepositorio<UsuarioEntity> usuarioRepositorio,
        ICrearRepositorio<AsignacionRolEntity> asignacionRepositorio,
        IMapper<RegistroUsuarioDTO, UsuarioEntity> usuarioMapper,
        IMapper<RegistroUsuarioDTO, AsignacionRolEntity> asignacionMapper)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _asignacionRepositorio = asignacionRepositorio;
        _usuarioMapper = usuarioMapper;
        _asignacionMapper = asignacionMapper;
    }

    public async Task AgregarAsync(RegistroUsuarioDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        // 1. Mapeamos y guardamos el Usuario
        var usuario = _usuarioMapper.Map(dto);

        // TODO a futuro: Acá deberíamos meter un servicio para hashear la contraseña 
        // antes de mandarla a la base de datos (Ej: usuario.Contrasenia = Hash(dto.Contrasenia)).

        await _usuarioRepositorio.AgregarAsync(usuario);

        // 2. Mapeamos y guardamos la Asignación del Rol
        var asignacion = _asignacionMapper.Map(dto);

        // 3. Le pasamos el ID del usuario recién creado
        asignacion.IdUsuario = usuario.Id;

        await _asignacionRepositorio.AgregarAsync(asignacion);
    }
}