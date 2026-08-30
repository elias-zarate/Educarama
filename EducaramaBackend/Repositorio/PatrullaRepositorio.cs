using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio;

public class PatrullaRepositorio : ILeerPatrullasPorRamaRepositorio, 
    ILeerRepositorio<PatrullaEntity>, ICrearRepositorio<PatrullaEntity>, 
    IActualizarRepositorio<PatrullaEntity>, IEliminarRepositorio
{
    private readonly EducaramaContext _context;

    public PatrullaRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(PatrullaEntity entidad)
    {
        var patrulla = new Patrulla
        {
            Nombre = entidad.Nombre,
            IdRama = entidad.IdRama
        };
        await _context.Patrullas.AddAsync(patrulla);
        await _context.SaveChangesAsync();

        entidad.Id = patrulla.Id;
    }

    public async Task ActualizarAsync(PatrullaEntity entidad)
    {
        var patrulla = await _context.Patrullas.FindAsync(entidad.Id);
        if (patrulla == null)
            throw new KeyNotFoundException($"No se encontró la patrulla con Id {entidad.Id}.");

        patrulla.Nombre = entidad.Nombre;
        patrulla.IdRama = entidad.IdRama;
        _context.Patrullas.Update(patrulla);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var patrulla = await _context.Patrullas.FindAsync(id);
        if (patrulla != null)
        {
            // Ojo: Si hay protagonistas asociados, acá podría saltar un error de FK. 
            // Más adelante podemos manejar borrados lógicos o reasignar a los jóvenes.
            _context.Patrullas.Remove(patrulla);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<PatrullaEntity>> ObtenerPatrullasPorRamaAsync(int idRama)
    {
        var patrullas = await _context.Patrullas
            .Where(p => p.IdRama == idRama)
            .ToListAsync();
        return patrullas.Select(MapToEntity);
    }

   

    public async Task<PatrullaEntity?> ObtenerPorIdAsync(int id)
    {
        var patrulla = await _context.Patrullas.FindAsync(id);
        if (patrulla == null)
            throw new KeyNotFoundException($"No se encontró la patrulla con Id {id}.");

        return MapToEntity(patrulla);
    }

    public async Task<IEnumerable<PatrullaEntity>> ObtenerTodosAsync()
    {
        var patrullas = await _context.Patrullas.ToListAsync();
        return patrullas.Select(MapToEntity);
    }

    // --- MAPPER ---
    #region
    private static PatrullaEntity MapToEntity(Patrulla patrulla)
    {
        return new PatrullaEntity
        {
            Id = patrulla.Id,
            Nombre = patrulla.Nombre!,
            IdRama = patrulla.IdRama
        };
    }
    #endregion
}