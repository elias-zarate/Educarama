using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Usuarios.DTOs;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Usuarios.CasosDeUso;

public class ObtenerContextoUsuarioUseCase
{
    private readonly ILeerAsignacionPorUsuarioRepositorio _asignacionRepositorio;

    public ObtenerContextoUsuarioUseCase(ILeerAsignacionPorUsuarioRepositorio asignacionRepositorio)
    {
        _asignacionRepositorio = asignacionRepositorio;
    }

    public async Task<UsuarioContextoDTO> ObtenerAsync(int idUsuario)
    {
        var asignacion = await _asignacionRepositorio.ObtenerPorUsuarioAsync(idUsuario);

        if (asignacion == null)
        {
            throw new UnauthorizedAccessException("El usuario no tiene roles ni grupo asignados en el sistema.");
        }

        return new UsuarioContextoDTO
        {
            IdUsuario = asignacion.IdUsuario,
            IdRol = asignacion.IdRol,
            IdGrupo = asignacion.IdGrupo,
            IdRama = asignacion.IdRama
        };
    }
}