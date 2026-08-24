using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio;

public class RamaRepositorio : ILeerRepositorio<RamaEntity>, IActualizarRepositorio<RamaEntity>
{
    private readonly EducaramaContext _context;
    public RamaRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task<RamaEntity?> ObtenerPorIdAsync(int id)
    {
        var rama = await _context.Ramas.FindAsync(id);

        if (rama is null)
            throw new KeyNotFoundException($"No se encontró la rama con Id {id}.");

        return MapToEntity(rama);
    }

    public async Task<IEnumerable<RamaEntity>> ObtenerTodosAsync()
    {
        var ramas = await _context.Ramas.ToListAsync();
        return ramas.Select(MapToEntity);
    }

    public async Task ActualizarAsync(RamaEntity entidad)
    {
        var rama = await _context.Ramas.FindAsync(entidad.Id);
        if (rama is null)
            throw new KeyNotFoundException($"No se encontró la rama con Id {entidad.Id}.");

        rama.CodigoAcceso = entidad.CodigoAcceso;
        await _context.SaveChangesAsync();
    }

    #region
    private RamaEntity MapToEntity(Rama rama)
    {
        return new RamaEntity
        {
            Id = rama.Id,
            Nombre = rama.Nombre,
            IdGrupo = rama.IdGrupo,
            IdCategoriaRama= rama.IdCategoriaRama
        };
    }
    #endregion
}
