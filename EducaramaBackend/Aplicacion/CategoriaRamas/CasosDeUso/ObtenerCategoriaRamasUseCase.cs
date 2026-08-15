using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.CategoriaRamas.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CategoriaRamas.CasosDeUso;

public class ObtenerCategoriaRamasUseCase : ILeerUseCase<CategoriaRamaDTO>
{
    private readonly ILeerRepositorio<CategoriaRamaEntity> _leerRepositorio;
    private readonly IMapper<CategoriaRamaEntity, CategoriaRamaDTO> _mapper;

    public ObtenerCategoriaRamasUseCase(
        ILeerRepositorio<CategoriaRamaEntity> leerRepositorio, 
        IMapper<CategoriaRamaEntity, CategoriaRamaDTO> mapper)
    {
        _leerRepositorio = leerRepositorio;
        _mapper = mapper;
    }

    public async Task<CategoriaRamaDTO?> ObtenerPorIdAsync(int id)
    {
        var categoriaRamaEntity = await _leerRepositorio.ObtenerPorIdAsync(id);

        if (categoriaRamaEntity == null)
            throw new KeyNotFoundException($"No se encontró la categoría de rama con ID {id}.");

        return _mapper.Map(categoriaRamaEntity);
    }

    public async Task<IEnumerable<CategoriaRamaDTO>> ObtenerTodosAsync()
    {
        var categoriaRamasEntities = await _leerRepositorio.ObtenerTodosAsync();
        return categoriaRamasEntities.Select(_mapper.Map);
    }
}
