using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using System.Threading.Tasks;

namespace Repositorio;

public class CumpleRepositorio : ICrearRepositorio<CumpleEntity>
{
    private readonly EducaramaContext _context;

    public CumpleRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(CumpleEntity entidad)
    {
        var cumple = MapToModel(entidad);
        await _context.Cumplimientos.AddAsync(cumple);
        await _context.SaveChangesAsync();
    }

    #region Mappers
    private static Cumple MapToModel(CumpleEntity entidad)
    {
        return new Cumple
        {
            IdProtagonista = entidad.IdProtagonista,
            IdAccion = entidad.IdAccion,
            AccionCumplida = entidad.AccionCumplida
        };
    }
    #endregion
}