using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Ramas.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Ramas.CasosDeUso;

public class UnirseRamaUseCase
{
    private readonly ILeerRamaPorCodigoRepositorio _ramaRepositorioLeer;
    private readonly ILeerAsignacionPorUsuarioRepositorio _asignacionRepositorioLeer;
    private readonly IActualizarRepositorio<AsignacionRolEntity> _asignacionRepositorioActualizar;

    public UnirseRamaUseCase(
        ILeerRamaPorCodigoRepositorio ramaRepositorioLeer,
        ILeerAsignacionPorUsuarioRepositorio asignacionRepositorioLeer,
        IActualizarRepositorio<AsignacionRolEntity> asignacionRepositorioActualizar)
    {
        _ramaRepositorioLeer = ramaRepositorioLeer;
        _asignacionRepositorioLeer = asignacionRepositorioLeer;
        _asignacionRepositorioActualizar = asignacionRepositorioActualizar;
    }

    public async Task UnirseAsync(UnirseRamaDTO dto)
    {
        var rama = await _ramaRepositorioLeer.ObtenerPorCodigoAsync(dto.CodigoRama);
        if (rama == null)
            throw new ArgumentException("El código de rama ingresado no es válido.");

        var asignacion = await _asignacionRepositorioLeer.ObtenerPorUsuarioAsync(dto.IdUsuario);
        if (asignacion == null)
            throw new KeyNotFoundException("El usuario no tiene un registro de roles en el sistema.");

        // Validamos que el usuario y la rama pertenezcan al mismo grupo
        if (asignacion.IdGrupo == null || asignacion.IdGrupo != rama.IdGrupo)
            throw new UnauthorizedAccessException("No puedes unirte a una rama que pertenece a un grupo distinto al tuyo.");

        asignacion.IdRama = rama.Id;
        await _asignacionRepositorioActualizar.ActualizarAsync(asignacion);
    }
}