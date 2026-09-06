using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Avisos.DTOs;
using Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Avisos.CasosDeUso;

public class ObtenerAvisosActivosUseCase
{
    private readonly ILeerAvisosActivosRepositorio _repositorio;
    private readonly IMapper<AvisoEntity, AvisoDTO> _mapper;

    public ObtenerAvisosActivosUseCase(
        ILeerAvisosActivosRepositorio repositorio,
        IMapper<AvisoEntity, AvisoDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AvisoDTO>> EjecutarAsync()
    {
        var avisosActivos = await _repositorio.ObtenerActivosAsync();
        return avisosActivos.Select(_mapper.Map);
    }
}