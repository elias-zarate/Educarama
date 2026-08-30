using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Familiares.DTOs;
using Dominio;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Familiares.CasosDeUso;

public class RegistrarFamiliarUseCase : ICrearUseCase<RegistroFamiliarCompletoDTO>
{
    private readonly ICrearRepositorio<PersonaEntity> _personaRepositorio;
    private readonly ICrearRepositorio<FamiliarEntity> _familiarRepositorio;
    private readonly IEsFamiliaRepositorio _esFamiliaRepositorio;

    public RegistrarFamiliarUseCase(
        ICrearRepositorio<PersonaEntity> personaRepositorio,
        ICrearRepositorio<FamiliarEntity> familiarRepositorio,
        IEsFamiliaRepositorio esFamiliaRepositorio)
    {
        _personaRepositorio = personaRepositorio;
        _familiarRepositorio = familiarRepositorio;
        _esFamiliaRepositorio = esFamiliaRepositorio;
    }

    public async Task AgregarAsync(RegistroFamiliarCompletoDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        // 1. Crear la Persona
        var persona = new PersonaEntity
        {
            Dni = dto.Dni,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            FechaNacimiento = dto.FechaNacimiento
        };
        await _personaRepositorio.AgregarAsync(persona);

        // 2. Crear el Familiar
        int? telefonoParsed = int.TryParse(dto.TelefonoContacto, out int tel) ? tel : null;

        var familiar = new FamiliarEntity
        {
            IdPersona = persona.Id,
            EmailContacto = dto.EmailContacto,
            TelefonoContacto = telefonoParsed
        };
        await _familiarRepositorio.AgregarAsync(familiar);

        // 3. Crear el vínculo en EsFamilia
        var esFamilia = new EsFamiliaEntity
        {
            IdProtagonista = dto.IdProtagonista,
            IdFamiliar = familiar.Id,
            Vinculo = dto.Vinculo,
            EsContactoDeEmergencia = dto.EsContactoDeEmergencia
        };
        await _esFamiliaRepositorio.AgregarAsync(esFamilia);
    }
}