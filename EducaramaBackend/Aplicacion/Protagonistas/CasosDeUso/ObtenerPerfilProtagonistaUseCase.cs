using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Protagonistas.DTOs;
using Dominio;

namespace Aplicacion.Protagonistas.CasosDeUso;

public class ObtenerPerfilProtagonistaUseCase : ILeerUseCase<PerfilProtagonistaDTO>
{
    private readonly ILeerRepositorio<ProtagonistaEntity> _repositorio;
    private readonly IMapper<ProtagonistaEntity, PerfilProtagonistaDTO> _mapper;

    public ObtenerPerfilProtagonistaUseCase(
        ILeerRepositorio<ProtagonistaEntity> repositorio,
        IMapper<ProtagonistaEntity, PerfilProtagonistaDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<PerfilProtagonistaDTO?> ObtenerPorIdAsync(int id)
    {
        var entidad = await _repositorio.ObtenerPorIdAsync(id);
        return entidad != null ? _mapper.Map(entidad) : null;
    }

    public async Task<IEnumerable<PerfilProtagonistaDTO>> ObtenerTodosAsync()
    {
        var entidades = await _repositorio.ObtenerTodosAsync();
        return entidades.Select(e => _mapper.Map(e));
    }
}