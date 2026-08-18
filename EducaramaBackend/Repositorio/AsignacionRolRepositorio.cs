using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio;

public class AsignacionRolRepositorio : ICrearRepositorio<AsignacionRolEntity>,
    IActualizarRepositorio<AsignacionRolEntity>
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

   
    #endregion
}
