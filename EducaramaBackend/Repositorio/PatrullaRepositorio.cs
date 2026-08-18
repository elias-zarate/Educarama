using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio;

public class PatrullaRepositorio : ILeerPatrullasPorRamaRepositorio, ILeerRepositorio<PatrullaEntity>
{
    private readonly EducaramaContext _context;

    public PatrullaRepositorio(EducaramaContext context)
    {
        _context = context;
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