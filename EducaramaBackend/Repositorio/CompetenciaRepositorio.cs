using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Repositorio;

public class CompetenciaRepositorio : ILeerRepositorio<CompetenciaEntity>
{
    private readonly EducaramaContext _context;

    public CompetenciaRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task<CompetenciaEntity?> ObtenerPorIdAsync(int id)
    {
        var competencia = await _context.Competencias.FirstOrDefaultAsync(c => c.Id == id);
        if (competencia == null)
            throw new KeyNotFoundException($"No se encontró la competencia con ID {id}.");

        return MapToEntity(competencia);
    }

    public async Task<IEnumerable<CompetenciaEntity>> ObtenerTodosAsync()
    {
        var competencias = await _context.Competencias.ToListAsync();
        return competencias.Select(MapToEntity);
    }

    #region
    private static CompetenciaEntity MapToEntity(Competencia competencia)
    {
        return new CompetenciaEntity
        {
            Id = competencia.Id,
            Codigo = competencia.Codigo,
            Descripcion = competencia.Descripcion!,
            IdCategoriaRama = competencia.IdCategoriaRama,
            IdSendero = competencia.IdSendero
        };
    }
    #endregion|
}