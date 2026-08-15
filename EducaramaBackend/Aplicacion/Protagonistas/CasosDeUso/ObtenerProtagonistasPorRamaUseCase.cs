using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Protagonistas.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Protagonistas.CasosDeUso;

public class ObtenerProtagonistasPorRamaUseCase
{
    private readonly ILeerProtagonistasPorRamaRepositorio _repositorio;
    private readonly IMapper<ProtagonistaEntity, PerfilProtagonistaDTO> _mapper;

    public ObtenerProtagonistasPorRamaUseCase(
        ILeerProtagonistasPorRamaRepositorio repositorio, IMapper<ProtagonistaEntity, 
            PerfilProtagonistaDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PerfilProtagonistaDTO>> EjecutarAsync(int idRama)
    {
        var entidades = await _repositorio.ObtenerPorRamaAsync(idRama);

        return entidades.Select(e => _mapper.Map(e));
    }
}
