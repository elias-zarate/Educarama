using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio;

public class ProtagonistaRepositorio : ICrearRepositorio<ProtagonistaEntity>,
    ILeerRepositorio<ProtagonistaEntity>,
    IActualizarRepositorio<ProtagonistaEntity>,
    IEliminarRepositorio,
    ILeerProtagonistasPorRamaRepositorio
{
    private readonly EducaramaContext _context;

    public ProtagonistaRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(ProtagonistaEntity entidad)
    {
        var protagonista = MapToModel(entidad);
        await _context.Protagonistas.AddAsync(protagonista);
        await _context.SaveChangesAsync();
    }

    public async Task<ProtagonistaEntity?> ObtenerPorIdAsync(int id)
    {
        // Buscamos al protagonista
        var protagonista = await _context.Protagonistas.FirstOrDefaultAsync(p => p.Id == id);

        if (protagonista is null)
            throw new KeyNotFoundException($"No se encontró el protagonista con Id {id}.");

        // Buscamos a mano a la persona asociada
        var persona = await _context.Personas.FirstOrDefaultAsync(per => per.Id == protagonista.IdPersona);

        return MapToEntity(protagonista, persona);
    }

    public async Task<IEnumerable<ProtagonistaEntity>> ObtenerTodosAsync()
    {
        // Hacemos el Join manual cruzando las tablas por el IdPersona
        var query = await (from p in _context.Protagonistas
                           join per in _context.Personas on p.IdPersona equals per.Id
                           select new { Protagonista = p, Persona = per }).ToListAsync();

        return query.Select(q => MapToEntity(q.Protagonista, q.Persona));
    }

    public async Task<IEnumerable<ProtagonistaEntity>> ObtenerPorRamaAsync(int idRama)
    {
        // Mismo Join, pero filtrando por la rama
        var query = await (from p in _context.Protagonistas
                           join per in _context.Personas on p.IdPersona equals per.Id
                           where p.IdRama == idRama
                           select new { Protagonista = p, Persona = per }).ToListAsync();

        return query.Select(q => MapToEntity(q.Protagonista, q.Persona));
    }

    public async Task ActualizarAsync(ProtagonistaEntity protagonistaEntity)
    {
        if (protagonistaEntity.Id is null)
            throw new ArgumentException("El Id de la entidad no coincide con el Id proporcionado.");

        var protagonista = await _context.Protagonistas.FindAsync(protagonistaEntity.Id);
        if (protagonista is null)
            throw new KeyNotFoundException($"No se encontró el protagonista con Id {protagonistaEntity.Id}.");

        protagonista.Promesa = protagonistaEntity.Promesa;
        protagonista.IdPersona = protagonistaEntity.IdPersona;
        protagonista.IdRama = protagonistaEntity.IdRama;
        protagonista.IdPatrulla = protagonistaEntity.IdPatrulla;
        protagonista.IdUsuario = protagonistaEntity.IdUsuario;

        _context.Protagonistas.Update(protagonista);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var protagonista = await _context.Protagonistas.FindAsync(id);
        if (protagonista is null)
            throw new KeyNotFoundException($"No se encontró el protagonista con Id {id}.");

        _context.Protagonistas.Remove(protagonista);
        await _context.SaveChangesAsync();
    }

    // Le pasamos la persona como segundo parámetro para mapear sin usar propiedades de navegación
    private ProtagonistaEntity MapToEntity(Protagonista protagonista, Persona? persona)
    {
        return new ProtagonistaEntity
        {
            Id = protagonista.Id,
            Promesa = protagonista.Promesa,
            IdPersona = protagonista.IdPersona,
            IdRama = protagonista.IdRama,
            IdPatrulla = protagonista.IdPatrulla,
            IdUsuario = protagonista.IdUsuario,
            Persona = persona != null ? new PersonaEntity
            {
                Id = persona.Id,
                Dni = persona.Dni,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                FechaNacimiento = persona.FechaNacimiento
            } : null
        };
    }

    private Protagonista MapToModel(ProtagonistaEntity protagonistaEntity)
    {
        return new Protagonista
        {
            Id = protagonistaEntity.Id ?? 0,
            Promesa = protagonistaEntity.Promesa,
            IdPersona = protagonistaEntity.IdPersona,
            IdRama = protagonistaEntity.IdRama,
            IdPatrulla = protagonistaEntity.IdPatrulla,
            IdUsuario = protagonistaEntity.IdUsuario,
        };
    }
}