using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Avisos.DTOs;
using Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Avisos.CasosDeUso;

public class ObtenerAvisosUseCase : ILeerUseCase<AvisoDTO>
{
    private readonly ILeerRepositorio<AvisoEntity> _repositorio;
    private readonly IMapper<AvisoEntity, AvisoDTO> _mapper;

    public ObtenerAvisosUseCase(
        ILeerRepositorio<AvisoEntity> repositorio,
        IMapper<AvisoEntity, AvisoDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<AvisoDTO?> ObtenerPorIdAsync(int id)
    {
        var entidad = await _repositorio.ObtenerPorIdAsync(id);
        return entidad != null ? _mapper.Map(entidad) : null;
    }

    public async Task<IEnumerable<AvisoDTO>> ObtenerTodosAsync()
    {
        var entidades = await _repositorio.ObtenerTodosAsync();
        return entidades.Select(_mapper.Map);
    }
}