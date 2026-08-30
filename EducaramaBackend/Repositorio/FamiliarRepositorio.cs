using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorio;

public class FamiliarRepositorio : ICrearRepositorio<FamiliarEntity>, IActualizarRepositorio<FamiliarEntity>
{
    private readonly EducaramaContext _context;

    public FamiliarRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(FamiliarEntity entidad)
    {
        var familiar = new Familiar
        {
            IdPersona = entidad.IdPersona ?? 0,
            EmailContacto = entidad.EmailContacto,
            TelefonoContacto = entidad.TelefonoContacto
        };

        await _context.Familiares.AddAsync(familiar);
        await _context.SaveChangesAsync();

        // Le devolvemos el ID generado a la entidad
        entidad.Id = familiar.Id;
    }

    public async Task ActualizarAsync(FamiliarEntity entidad)
    {
        if (entidad.Id == 0)
            throw new ArgumentException("El ID del familiar no es válido.");

        var familiar = await _context.Familiares.FindAsync(entidad.Id);
        if (familiar == null)
            throw new KeyNotFoundException($"No se encontró el familiar con ID {entidad.Id}.");

        familiar.EmailContacto = entidad.EmailContacto;
        familiar.TelefonoContacto = entidad.TelefonoContacto;

        _context.Familiares.Update(familiar);
        await _context.SaveChangesAsync();
    }
}