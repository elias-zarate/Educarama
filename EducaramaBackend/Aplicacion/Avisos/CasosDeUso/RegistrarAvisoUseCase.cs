using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Avisos.DTOs;
using Dominio;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Avisos.CasosDeUso;

public class RegistrarAvisoUseCase : ICrearUseCase<RegistroAvisoDTO>
{
    private readonly ICrearRepositorio<AvisoEntity> _repositorio;
    private readonly IMapper<RegistroAvisoDTO, AvisoEntity> _mapper;

    public RegistrarAvisoUseCase(
        ICrearRepositorio<AvisoEntity> repositorio,
        IMapper<RegistroAvisoDTO, AvisoEntity> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task AgregarAsync(RegistroAvisoDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var entidad = _mapper.Map(dto);
        await _repositorio.AgregarAsync(entidad);
    }
}