using System;
using System.Threading.Tasks;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Usuarios.DTOs;
using Dominio;

namespace Aplicacion.Usuarios.CasosDeUso;

public class UnirseGrupoUseCase
{
    private readonly IAsignarGrupoUsuarioRepositorio _asignacionRepositorio;
    private readonly ILeerGrupoPorCodigoRepositorio _grupoRepositorio;
    private readonly ILeerRepositorio<UsuarioEntity> _usuarioRepositorio; // <-- 1. Agregamos el repo de usuarios

    public UnirseGrupoUseCase(
        IAsignarGrupoUsuarioRepositorio asignacionRepositorio,
        ILeerGrupoPorCodigoRepositorio grupoRepositorio,
        ILeerRepositorio<UsuarioEntity> usuarioRepositorio) // <-- 2. Lo inyectamos en el constructor
    {
        _asignacionRepositorio = asignacionRepositorio;
        _grupoRepositorio = grupoRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
    }

    public async Task UnirseAsync(UnirseGrupoDTO dto)
    {
        // NUEVO: Verificamos si el usuario realmente existe en la base de datos
        // (Dependiendo de cómo tengas armado tu ILeerRepositorio, si no existe puede lanzar KeyNotFoundException o devolver null)
        try
        {
            var usuario = await _usuarioRepositorio.ObtenerPorIdAsync(dto.IdUsuario);
            if (usuario == null)
            {
                throw new Exception("El usuario especificado no existe.");
            }
        }
        catch (KeyNotFoundException)
        {
            throw new Exception($"No se encontró ningún usuario con el ID {dto.IdUsuario}.");
        }

        // 1. Buscamos el grupo por su código alfanumérico
        var grupo = await _grupoRepositorio.ObtenerPorCodigoAsync(dto.CodigoGrupo);
        if (grupo == null)
        {
            throw new Exception("El código del grupo no es válido o no existe.");
        }

        // 2. Le asignamos el grupo y el rol al usuario
        await _asignacionRepositorio.AsignarRolYGrupoAsync(dto.IdUsuario, grupo.Id, dto.IdRol);
    }
}