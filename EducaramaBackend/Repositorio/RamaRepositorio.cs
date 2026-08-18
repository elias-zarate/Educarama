using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio;

public class RamaRepositorio : ICrearRepositorio<RamaEntity>, 
    ILeerRepositorio<RamaEntity>
{
    private readonly EducaramaContext _context;
    public RamaRepositorio(EducaramaContext context)
    {
        _context = context;
    }
    public async Task AgregarAsync(RamaEntity entidad)
    {
        var rama = MapToModel(entidad);
        await _context.Ramas.AddAsync(rama);
        await _context.SaveChangesAsync();
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
    private Rama MapToModel(RamaEntity ramaEntity)
    {
        return new Rama
        {
            Id = ramaEntity.Id,
            Nombre = ramaEntity.Nombre,
            IdCategoriaRama = ramaEntity.IdCategoriaRama
        };
    }
    #endregion
}
