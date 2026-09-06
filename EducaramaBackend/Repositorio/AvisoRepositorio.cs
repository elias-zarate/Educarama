using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio;

public class AvisoRepositorio :
    ICrearRepositorio<AvisoEntity>,
    ILeerRepositorio<AvisoEntity>,
    IActualizarRepositorio<AvisoEntity>,
    IEliminarRepositorio,
    ILeerAvisosActivosRepositorio
{
    private readonly EducaramaContext _context;

    public AvisoRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(AvisoEntity entidad)
    {
        var aviso = MapToModel(entidad);
        await _context.Avisos.AddAsync(aviso);
        await _context.SaveChangesAsync();
        entidad.Id = aviso.Id;
    }

    public async Task<AvisoEntity?> ObtenerPorIdAsync(int id)
    {
        var aviso = await _context.Avisos.FindAsync(id);
        if (aviso == null)
            throw new KeyNotFoundException($"No se encontró el aviso con ID {id}.");
        return MapToEntity(aviso);
    }

    public async Task<IEnumerable<AvisoEntity>> ObtenerTodosAsync()
    {
        var avisos = await _context.Avisos.ToListAsync();
        return avisos.Select(MapToEntity);
    }

    // Consulta específica para el dashboard
    public async Task<IEnumerable<AvisoEntity>> ObtenerActivosAsync()
    {
        var hoy = DateOnly.FromDateTime(DateTime.Today);

        var avisos = await _context.Avisos
            .Where(a => a.Activo && a.FechaInicio <= hoy && (a.FechaFin == null || a.FechaFin >= hoy))
            .ToListAsync();

        return avisos.Select(MapToEntity);
    }

    public async Task ActualizarAsync(AvisoEntity entidad)
    {
        var aviso = await _context.Avisos.FindAsync(entidad.Id);
        if (aviso == null)
            throw new KeyNotFoundException($"No se encontró el aviso con ID {entidad.Id}.");

        aviso.Titulo = entidad.Titulo;
        aviso.Descripcion = entidad.Descripcion;
        aviso.FechaInicio = entidad.FechaInicio;
        aviso.FechaFin = entidad.FechaFin;
        aviso.Prioridad = entidad.Prioridad;
        aviso.Activo = entidad.Activo;

        _context.Avisos.Update(aviso);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var aviso = await _context.Avisos.FindAsync(id);
        if (aviso != null)
        {
            _context.Avisos.Remove(aviso);
            await _context.SaveChangesAsync();
        }
    }

    #region Mappers
    private static AvisoEntity MapToEntity(Aviso aviso)
    {
        return new AvisoEntity
        {
            Id = aviso.Id,
            Titulo = aviso.Titulo!,
            Descripcion = aviso.Descripcion!,
            FechaInicio = aviso.FechaInicio,
            FechaFin = aviso.FechaFin,
            Prioridad = aviso.Prioridad!,
            Activo = aviso.Activo
        };
    }

    private static Aviso MapToModel(AvisoEntity entidad)
    {
        return new Aviso
        {
            Titulo = entidad.Titulo,
            Descripcion = entidad.Descripcion,
            FechaInicio = entidad.FechaInicio,
            FechaFin = entidad.FechaFin,
            Prioridad = entidad.Prioridad,
            Activo = entidad.Activo
        };
    }
    #endregion
}