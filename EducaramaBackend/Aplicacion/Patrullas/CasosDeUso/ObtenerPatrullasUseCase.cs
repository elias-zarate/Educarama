using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Patrullas.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Patrullas.CasosDeUso;

public class ObtenerPatrullasUseCase : ILeerUseCase<PatrullaDTO>
{
    ILeerRepositorio<PatrullaEntity> _repositorio;
    IMapper<PatrullaEntity, PatrullaDTO> _mapper;

    public ObtenerPatrullasUseCase(ILeerRepositorio<PatrullaEntity> repositorio,
        IMapper<PatrullaEntity, PatrullaDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<PatrullaDTO> ObtenerPorIdAsync(int id)
    {
        var patrullaEntity = await _repositorio.ObtenerPorIdAsync(id);
        if (patrullaEntity is null)
            throw new ArgumentException($"No se encontró la patrulla con id {id}");

        return _mapper.Map(patrullaEntity);

    }

    public async Task<IEnumerable<PatrullaDTO>> ObtenerTodosAsync()
    {
        var patrullasEntities = await _repositorio.ObtenerTodosAsync();
        return patrullasEntities.Select(_mapper.Map);
    }
}

    

