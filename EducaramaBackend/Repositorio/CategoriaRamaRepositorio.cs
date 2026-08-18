using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio;

public class CategoriaRamaRepositorio : ILeerRepositorio<CategoriaRamaEntity>
{
    private readonly EducaramaContext _context;
    public CategoriaRamaRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task<CategoriaRamaEntity?> ObtenerPorIdAsync(int id)
    {
        var categoriaRama = await _context.CategoriasRama.FindAsync(id);

        if(categoriaRama == null)
            throw new KeyNotFoundException($"No se encontró la categoría de rama con el ID {id}.");
        
        return MapToEntity(categoriaRama);
    }
    public async Task<IEnumerable<CategoriaRamaEntity>> ObtenerTodosAsync()
    {
        var categoriasRamas = await _context.CategoriasRama.ToListAsync();
        return categoriasRamas.Select(MapToEntity);
    }
    #region
    private CategoriaRamaEntity MapToEntity(CategoriaRama categoriaRama)
    {
        return new CategoriaRamaEntity
        {
            Id = categoriaRama.Id,
            Nombre = categoriaRama.Nombre,
        };
    }
    #endregion
}
