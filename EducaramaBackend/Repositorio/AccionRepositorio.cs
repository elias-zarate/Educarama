using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Repositorio;

public class AccionRepositorio : ILeerRepositorio<AccionEntity>
{
    private readonly EducaramaContext _context;

    public AccionRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task<AccionEntity?> ObtenerPorIdAsync(int id)
    {
        var accion = await _context.Acciones.FirstOrDefaultAsync(a => a.Id == id);
        if (accion == null)
            throw new KeyNotFoundException($"No se encontró la acción con ID {id}.");

        return MapToEntity(accion);
    }

    public async Task<IEnumerable<AccionEntity>> ObtenerTodosAsync()
    {
        var acciones = await _context.Acciones.ToListAsync();
        return acciones.Select(MapToEntity);
    }

    #region
    private static AccionEntity MapToEntity(Accion accion)
    {
        return new AccionEntity
        {
            Id = accion.Id,
            Descripcion = accion.Descripcion!,
            IdCompetencia = accion.IdCompetencia,
            CodigoCompetencia = accion.CodigoCompetencia,
            IdTipoAccion = accion.IdTipoAccion
        };
    }
    #endregion
}