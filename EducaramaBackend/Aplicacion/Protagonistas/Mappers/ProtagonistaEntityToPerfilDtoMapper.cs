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

        // Si por alguna razón viene nula, evitamos usar Dni = 0 para no romper el dominio
        var persona = entidad.Persona ?? new PersonaEntity { Dni = 1, Nombre = "Sin nombre", Apellido = "Sin apellido" };

        return new PerfilProtagonistaDTO
        {
            IdProtagonista = entidad.Id ?? 0,
            IdPersona = entidad.IdPersona,
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