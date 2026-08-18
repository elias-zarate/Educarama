using System;
using System.Threading.Tasks;
using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Protagonistas.DTOs;
using Dominio;

namespace Aplicacion.Protagonistas.CasosDeUso;

public class RegistrarProtagonistaUseCase : ICrearUseCase<RegistroProtagonistaDTO>
{
    private readonly ICrearRepositorio<PersonaEntity> _personaRepositorio;
    private readonly ICrearRepositorio<ProtagonistaEntity> _protagonistaRepositorio;
    private readonly IMapper<RegistroProtagonistaDTO, PersonaEntity> _personaMapper;
    private readonly IMapper<RegistroProtagonistaDTO, ProtagonistaEntity> _protagonistaMapper;

    public RegistrarProtagonistaUseCase(
        ICrearRepositorio<PersonaEntity> personaRepositorio,
        ICrearRepositorio<ProtagonistaEntity> protagonistaRepositorio,
        IMapper<RegistroProtagonistaDTO, PersonaEntity> personaMapper,
        IMapper<RegistroProtagonistaDTO, ProtagonistaEntity> protagonistaMapper)
    {
        _personaRepositorio = personaRepositorio;
        _protagonistaRepositorio = protagonistaRepositorio;
        _personaMapper = personaMapper;
        _protagonistaMapper = protagonistaMapper;
    }

    public async Task AgregarAsync(RegistroProtagonistaDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var persona = _personaMapper.Map(dto);
        await _personaRepositorio.AgregarAsync(persona);

        var protagonista = _protagonistaMapper.Map(dto);
        //protagonista.IdPersona = persona.Id;

        await _protagonistaRepositorio.AgregarAsync(protagonista);
    }
}