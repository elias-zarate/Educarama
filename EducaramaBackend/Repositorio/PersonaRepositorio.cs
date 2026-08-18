using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio;

public class PersonaRepositorio : ICrearRepositorio<PersonaEntity>, 
    IActualizarRepositorio<PersonaEntity>
{
    private readonly EducaramaContext _context;

    public PersonaRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(PersonaEntity entidad)
    {
        var persona = MapToModel(entidad);
        await _context.Personas.AddAsync(persona);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(PersonaEntity entidad)
    {

        if(entidad.Id is null)
        {
            throw new ArgumentNullException(nameof(entidad.Id), "El Id de la entidad no puede ser nulo.");
        }
        var persona = MapToModel(entidad);


        _context.Personas.Update(persona);
        await _context.SaveChangesAsync();
    }

    #region
    private static Persona MapToModel(PersonaEntity entidad)
    {
        return new Persona
        {
            Dni = entidad.Dni,
            Nombre = entidad.Nombre!,
            Apellido = entidad.Apellido!,
            FechaNacimiento = entidad.FechaNacimiento
        };
    }
    #endregion
}
