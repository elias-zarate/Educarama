using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Competencias.DTOs;
using Dominio;

namespace Aplicacion.Competencias.CasosDeUso;

public class ObtenerCompetenciasUseCase : ILeerUseCase<CompetenciaDTO>
{
    private readonly ILeerRepositorio<CompetenciaEntity> _repositorio;
    private readonly IMapper<CompetenciaEntity, CompetenciaDTO> _mapper;

    public ObtenerCompetenciasUseCase(
        ILeerRepositorio<CompetenciaEntity> repositorio,
        IMapper<CompetenciaEntity, CompetenciaDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<CompetenciaDTO?> ObtenerPorIdAsync(int id)
    {
        var entidad = await _repositorio.ObtenerPorIdAsync(id);
        return entidad != null ? _mapper.Map(entidad) : null;
    }

    public async Task<IEnumerable<CompetenciaDTO>> ObtenerTodosAsync()
    {
        var entidades = await _repositorio.ObtenerTodosAsync();
        return entidades.Select(_mapper.Map);
    }
}