using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio;

public class GrupoRepositorio : ICrearRepositorio<GrupoEntity>, 
    ILeerRepositorio<GrupoEntity>, ILeerGrupoPorCodigoRepositorio
{
    private readonly EducaramaContext _context;

    public GrupoRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(GrupoEntity entidad)
    {
        var grupo = MapToModel(entidad);

        await _context.Grupos.AddAsync(grupo);
        await _context.SaveChangesAsync();

        // Le devolvemos el ID autogenerado a la entidad del dominio para usarlo en las ramas
        entidad.Id = grupo.Id;
    }

    public async Task<GrupoEntity?> ObtenerPorIdAsync(int id)
    {
        var grupo = await _context.Grupos.FindAsync(id);

        return grupo != null ? MapToEntity(grupo) : null;
    }

    public async Task<IEnumerable<GrupoEntity>> ObtenerTodosAsync()
    {
        var grupos = await _context.Grupos.ToListAsync();

        return grupos.Select(MapToEntity);
    }

    public async Task<GrupoEntity?> ObtenerPorCodigoAsync(string codigo)
    {
        // Buscamos en la BD el grupo que tenga ese código exacto
        var grupo = await _context.Grupos.FirstOrDefaultAsync(g => g.Codigo == codigo);

        if (grupo == null)
            return null; // Si no existe, devolvemos null

        // Lo mapeamos a tu entidad de dominio (fijate si tus propiedades se llaman igual)
        return new GrupoEntity
        {
            Id = grupo.Id,
            Codigo = grupo.Codigo,
            Nombre = grupo.Nombre!
        };
    }

    #region Mappers
    private static GrupoEntity MapToEntity(Grupo grupo)
    {
        return new GrupoEntity
        {
            Id = grupo.Id,
            Codigo = grupo.Codigo,
            Nombre = grupo.Nombre!
        };
    }

    private static Grupo MapToModel(GrupoEntity grupoEntity)
    {
        return new Grupo
        {
            Codigo = grupoEntity.Codigo,
            Nombre = grupoEntity.Nombre
        };
    }
    #endregion
}