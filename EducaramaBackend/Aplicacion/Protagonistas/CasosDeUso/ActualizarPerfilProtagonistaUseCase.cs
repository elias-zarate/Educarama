using System;
using System.Threading.Tasks;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Protagonistas.DTOs;
using Dominio;

namespace Aplicacion.Protagonistas.CasosDeUso;

public class ActualizarPerfilProtagonistaUseCase : IActualizarUseCase<PerfilProtagonistaDTO>
{
    private readonly IActualizarRepositorio<PersonaEntity> _personaRepositorio;
    private readonly IActualizarRepositorio<ProtagonistaEntity> _protagonistaRepositorio;

    public ActualizarPerfilProtagonistaUseCase(
        IActualizarRepositorio<PersonaEntity> personaRepositorio,
        IActualizarRepositorio<ProtagonistaEntity> protagonistaRepositorio)
    {
        _personaRepositorio = personaRepositorio;
        _protagonistaRepositorio = protagonistaRepositorio;
    }

    public async Task ActualizarAsync(PerfilProtagonistaDTO dto, int idProtagonista)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var persona = new PersonaEntity
        {
            Id = dto.IdPersona,
            Dni = dto.Dni,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            FechaNacimiento = dto.FechaNacimiento
        };

        var protagonista = new ProtagonistaEntity
        {
            Id = idProtagonista,
            IdPersona = dto.IdPersona,
            Promesa = dto.Promesa,
            IdRama = dto.IdRama,
            IdPatrulla = dto.IdPatrulla,
            IdUsuario = dto.IdUsuario
        };

        await _personaRepositorio.ActualizarAsync(persona);
        await _protagonistaRepositorio.ActualizarAsync(protagonista);
    }
}