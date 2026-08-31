using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio;

public class AsignacionRolRepositorio : ICrearRepositorio<AsignacionRolEntity>,
    IActualizarRepositorio<AsignacionRolEntity>, IAsignarGrupoUsuarioRepositorio,
    ILeerAsignacionPorUsuarioRepositorio
{
    private readonly EducaramaContext _context;

    public AsignacionRolRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(AsignacionRolEntity entidad)
    {
        var rol = MapTomodel(entidad);
        await _context.AsignacionesRol.AddAsync(rol);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(AsignacionRolEntity entidad, int id)
    {
        var rol = MapTomodel(entidad);
        _context.AsignacionesRol.Update(rol);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(AsignacionRolEntity entidad)
    {
        if (entidad.Id is null)
            throw new ArgumentException("El Id de la entidad no coincide con el Id proporcionado.");
        var rol = await _context.AsignacionesRol.FindAsync(entidad.Id);
        if (rol is null)
            throw new KeyNotFoundException($"No se encontró la asignación de rol con Id {entidad.Id}.");
        rol.IdUsuario = entidad.IdUsuario;
        rol.IdRol = entidad.IdRol;
        rol.IdGrupo = entidad.IdGrupo;
        rol.IdRama = entidad.IdRama;
        await _context.SaveChangesAsync();
    }


    public async Task AsignarRolYGrupoAsync(int idUsuario, int idGrupo, int idRol)
    {
        var asignacion = await _context.AsignacionesRol.FirstOrDefaultAsync(a => a.IdUsuario == idUsuario);
        if (asignacion != null)
        {
            asignacion.IdGrupo = idGrupo;
            asignacion.IdRol = idRol; // Supongamos que 2 es "Jefe de Grupo"
            _context.AsignacionesRol.Update(asignacion);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<AsignacionRolEntity?> ObtenerPorUsuarioAsync(int idUsuario)
    {
        var asignacion = await _context.AsignacionesRol.FirstOrDefaultAsync(a => a.IdUsuario == idUsuario);
        if (asignacion == null) return null;
        return MapToEntity(asignacion);
    }

    #region
    private static AsignacionRol MapTomodel(AsignacionRolEntity asignacionRolEntity)
    {
        return new AsignacionRol
        {
            IdUsuario = asignacionRolEntity.IdUsuario,
            IdRol = asignacionRolEntity.IdRol,
            IdGrupo = asignacionRolEntity.IdGrupo,
            IdRama = asignacionRolEntity.IdRama
        };
    }

    private static AsignacionRolEntity MapToEntity(AsignacionRol asignacion)
    {
        return new AsignacionRolEntity
        {
            Id = asignacion.Id,
            IdUsuario = asignacion.IdUsuario,
            IdRol = asignacion.IdRol,
            IdGrupo = asignacion.IdGrupo,
            IdRama = asignacion.IdRama
        };
    }


    #endregion
}
