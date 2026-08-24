using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Patrullas.DTOs;
using Dominio;

namespace Aplicacion.Patrullas.CasosDeUso;

public class ObtenerPatrullasUseCase : ILeerUseCase<PatrullaDTO>
{
    private readonly ILeerRepositorio<PatrullaEntity> _repositorio;
    private readonly IMapper<PatrullaEntity, PatrullaDTO> _mapper;

    public ObtenerPatrullasUseCase(ILeerRepositorio<PatrullaEntity> repositorio,
        IMapper<PatrullaEntity, PatrullaDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<PatrullaDTO?> ObtenerPorIdAsync(int id) // <-- Agregado el ?
    {
        var patrullaEntity = await _repositorio.ObtenerPorIdAsync(id);
        return patrullaEntity != null ? _mapper.Map(patrullaEntity) : null;
    }

    public async Task<IEnumerable<PatrullaDTO>> ObtenerTodosAsync()
    {
        var patrullasEntities = await _repositorio.ObtenerTodosAsync();
        return patrullasEntities.Select(_mapper.Map);
    }
}