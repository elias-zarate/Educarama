using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Patrullas.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Patrullas.CasosDeUso;

public class ObtenerPatrullasPorRamaUseCase : ILeerPatrullasPorRamaUseCase
{
    private readonly ILeerPatrullasPorRamaRepositorio _repositorio;
    private readonly IMapper<PatrullaEntity, PatrullaDTO> _mapper;

    public ObtenerPatrullasPorRamaUseCase(
        ILeerPatrullasPorRamaRepositorio repositorio, 
        IMapper<PatrullaEntity, PatrullaDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatrullaDTO>> ObtenerPatrullasPorRamaAsync(int idRama)
    {
        var entidades = await _repositorio.ObtenerPatrullasPorRamaAsync(idRama);

        return entidades.Select(e => _mapper.Map(e));
    }
}
