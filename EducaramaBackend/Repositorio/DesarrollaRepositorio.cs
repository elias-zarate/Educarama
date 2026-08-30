using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Desarrolla.DTOs;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio;

public class DesarrollaRepositorio : IDesarrollaRepositorio
{
    private readonly EducaramaContext _context;

    public DesarrollaRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(DesarrollaEntity entidad)
    {
        // Mapeamos nuestra entidad de dominio a la entidad de base de datos (DbSet)
        var desarrollaDb = new Desarrolla
        {
            IdProtagonista = entidad.IdProtagonista,
            IdCompetencia = entidad.IdCompetencia,
            CodigoCompetencia = entidad.CodigoCompetencia,
            CompetenciaDesarrollada = entidad.CompetenciaDesarrollada ?? false
        };

        await _context.Desarrollos.AddAsync(desarrollaDb);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarEstadoAsync(int idProtagonista, int idCompetencia, bool estado)
    {
        var registro = await _context.Desarrollos
            .FirstOrDefaultAsync(d => d.IdProtagonista == idProtagonista && d.IdCompetencia == idCompetencia);

        if (registro != null)
        {
            registro.CompetenciaDesarrollada = estado;
            _context.Desarrollos.Update(registro);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<CompetenciaEnDesarrolloDTO>> ObtenerPorProtagonistaAsync(int idProtagonista)
    {
        // 1. Traemos las competencias que el protagonista está desarrollando
        var competenciasDesarrollo = await (from d in _context.Desarrollos
                                            join c in _context.Competencias on d.IdCompetencia equals c.Id
                                            where d.IdProtagonista == idProtagonista
                                            select new
                                            {
                                                c.Id,
                                                c.Codigo,
                                                c.Descripcion,
                                                Terminada = d.CompetenciaDesarrollada ?? false
                                            }).ToListAsync();

        var resultado = new List<CompetenciaEnDesarrolloDTO>();

        // 2. Para cada competencia, buscamos sus acciones y evaluamos cuáles ya cumplió el protagonista
        foreach (var comp in competenciasDesarrollo)
        {
            // Buscamos todas las acciones pertenecientes a esta competencia
            var accionesDeCompetencia = await (from a in _context.Acciones
                                               where a.IdCompetencia == comp.Id
                                               select new AccionConEstadoDTO
                                               {
                                                   IdAccion = a.Id,
                                                   Descripcion = a.Descripcion,
                                                   IdTipoAccion = a.IdTipoAccion,
                                                   // Verificamos si existe un registro en Cumple que indique que el protagonista hizo esta acción
                                               }).ToListAsync();

            // Consultamos los cumplimientos del protagonista para estas acciones de forma eficiente
            var idsAcciones = accionesDeCompetencia.Select(a => a.IdAccion).ToList();
            var cumplidas = await _context.Cumplimientos
                .Where(cu => cu.IdProtagonista == idProtagonista && idsAcciones.Contains(cu.IdAccion) && cu.AccionCumplida)
                .Select(cu => cu.IdAccion)
                .ToListAsync();

            // Seteamos el tilde (Cumplida = true) a las acciones que correspondan
            foreach (var accion in accionesDeCompetencia)
            {
                accion.Cumplida = cumplidas.Contains(accion.IdAccion);
            }

            resultado.Add(new CompetenciaEnDesarrolloDTO
            {
                IdCompetencia = comp.Id,
                Codigo = comp.Codigo,
                Descripcion = comp.Descripcion,
                Terminada = comp.Terminada,
                Acciones = accionesDeCompetencia
            });
        }

        return resultado;
    }
}