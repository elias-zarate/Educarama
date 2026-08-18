using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using System;
using System.Threading.Tasks;

namespace Repositorio;

public class ObtieneRepositorio : ICrearRepositorio<ObtieneEntity>
{
    private readonly EducaramaContext _context;

    public ObtieneRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(ObtieneEntity entidad)
    {
        var obtiene = MapToModel(entidad);
        await _context.Obtenciones.AddAsync(obtiene);
        await _context.SaveChangesAsync();
    }

    #region Mappers
    private static Obtiene MapToModel(ObtieneEntity entidad)
    {
        return new Obtiene
        {
            IdProtagonista = entidad.IdProtagonista,
            IdInsignia = entidad.IdInsignia,
            Fecha = entidad.Fecha // Recordá que este campo ahora es NOT NULL según corregimos
        };
    }
    #endregion
}