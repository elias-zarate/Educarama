using System;
using Aplicacion.Abstracciones;
using Aplicacion.Protagonistas.DTOs;
using Dominio;

namespace Aplicacion.Protagonistas.Mappers;

public class ProtagonistaEntityToPerfilDtoMapper : IMapper<ProtagonistaEntity, PerfilProtagonistaDTO>
{
    public PerfilProtagonistaDTO Map(ProtagonistaEntity entidad)
    {
        if (entidad == null) throw new ArgumentNullException(nameof(entidad));

        // Validación defensiva: Si entidad.Persona viene nulo, creamos una vacía 
        // temporalmente para que no tire error al intentar leer sus propiedades.
        // (En la vida real, el Repositorio debería encargarse de traerla usando .Include())
        var persona = entidad.Persona ?? new PersonaEntity { Dni = 0 };

        return new PerfilProtagonistaDTO
        {
            IdProtagonista = entidad.Id,
            IdPersona = entidad.IdPersona,

            // Leemos los datos de la variable segura 'persona'
            Dni = persona.Dni,
            Nombre = persona.Nombre,
            Apellido = persona.Apellido,
            FechaNacimiento = persona.FechaNacimiento,

            Promesa = entidad.Promesa,
            IdRama = entidad.IdRama,
            IdPatrulla = entidad.IdPatrulla,
            IdUsuario = entidad.IdUsuario
        };
    }
}