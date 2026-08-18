using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio;

public class PatrullaRepositorio : ILeerPatrullasPorRamaRepositorio
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
