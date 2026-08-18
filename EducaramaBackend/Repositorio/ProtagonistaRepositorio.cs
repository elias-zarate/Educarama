using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio;

public class ProtagonistaRepositorio : ICrearRepositorio<ProtagonistaEntity>,
    ILeerRepositorio<ProtagonistaEntity>,
    IActualizarRepositorio<ProtagonistaEntity>,
    IEliminarRepositorio
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

    public async Task<ProtagonistaEntity> ObtenerPorIdAsync(int id)
    {
        var protagonista = await _context.Protagonistas.FirstOrDefaultAsync(p => p.Id == id);

        if (protagonista is null)
            throw new KeyNotFoundException($"No se encontró el protagonista con Id {id}.");

        return MapToEntity(protagonista);
    }

    public async Task<IEnumerable<ProtagonistaEntity>> ObtenerTodosAsync()
    {
        var protagonistas = await _context.Protagonistas.ToListAsync();
        return protagonistas.Select(MapToEntity);
    }

    public async Task ActualizarAsync(ProtagonistaEntity protagonistaEntity)
    {
        if (protagonistaEntity.Id is null)
            throw new ArgumentException("El Id de la entidad no coincide con el Id proporcionado.");

        var protagonista = await _context.Protagonistas.FindAsync(protagonistaEntity    .Id);

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

    #region
    private ProtagonistaEntity MapToEntity(Protagonista protagonista)
    {
        return new ProtagonistaEntity
        {
            Id = protagonista.Id,
            Promesa = protagonista.Promesa,
            IdPersona = protagonista.IdPersona,
            IdRama = protagonista.IdRama,
            IdPatrulla = protagonista.IdPatrulla,
            IdUsuario = protagonista.IdUsuario,
        };
    }

    private Protagonista MapToModel(ProtagonistaEntity protagonistaEntity)
    {
        return new Protagonista
        {
            Promesa = protagonistaEntity.Promesa,
            IdPersona = protagonistaEntity.IdPersona,
            IdRama = protagonistaEntity.IdRama,
            IdPatrulla = protagonistaEntity.IdPatrulla,
            IdUsuario = protagonistaEntity.IdUsuario,
        };
    }
    #endregion
}
