using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Familiares.DTOs;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio;

public class EsFamiliaRepositorio : IEsFamiliaRepositorio
{
    private readonly EducaramaContext _context;

    public EsFamiliaRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(EsFamiliaEntity entidad)
    {
        var vinculo = new EsFamilia
        {
            IdProtagonista = entidad.IdProtagonista,
            IdFamiliar = entidad.IdFamiliar,
            Vinculo = entidad.Vinculo,
            EsContactoDeEmergencia = entidad.EsContactoDeEmergencia ?? false
        };

        await _context.RelacionesFamilia.AddAsync(vinculo);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarVinculoAsync(int idProtagonista, int idFamiliar)
    {
        var vinculo = await _context.RelacionesFamilia
            .FirstOrDefaultAsync(ef => ef.IdProtagonista == idProtagonista && ef.IdFamiliar == idFamiliar);

        if (vinculo != null)
        {
            _context.RelacionesFamilia.Remove(vinculo);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<FamiliarPerfilDTO>> ObtenerPorProtagonistaAsync(int idProtagonista)
    {
        var query = await (from ef in _context.RelacionesFamilia
                           join f in _context.Familiares on ef.IdFamiliar equals f.Id
                           join p in _context.Personas on f.IdPersona equals p.Id
                           where ef.IdProtagonista == idProtagonista
                           select new FamiliarPerfilDTO
                           {
                               IdFamiliar = f.Id,
                               IdPersona = p.Id,
                               Dni = p.Dni,
                               Nombre = p.Nombre,
                               Apellido = p.Apellido,
                               EmailContacto = f.EmailContacto,
                               TelefonoContacto = f.TelefonoContacto != null ? f.TelefonoContacto.ToString() : null,
                               Vinculo = ef.Vinculo,
                               EsContactoDeEmergencia = ef.EsContactoDeEmergencia ?? false
                           }).ToListAsync();

        return query;
    }
}