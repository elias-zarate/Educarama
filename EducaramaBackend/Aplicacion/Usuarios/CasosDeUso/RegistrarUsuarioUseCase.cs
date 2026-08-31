using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Usuarios.DTOs;
using Dominio;
using System;
using System.Threading.Tasks;
using System.Transactions;

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

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        // 1. Guardamos el Usuario
        var usuario = _usuarioMapper.Map(dto);
        await _usuarioRepositorio.AgregarAsync(usuario);

        // 2. Guardamos la Asignación del Rol
        var asignacion = _asignacionMapper.Map(dto);
        asignacion.IdUsuario = usuario.Id;
        await _asignacionRepositorio.AgregarAsync(asignacion);

        // Si todo salió bien, confirmamos la transacción
        scope.Complete();
    }
}