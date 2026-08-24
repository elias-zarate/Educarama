using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Acciones.DTOs;
using Dominio;

namespace Aplicacion.Acciones.CasosDeUso;

public class ObtenerAccionesUseCase : ILeerUseCase<AccionDTO>
{
    private readonly ILeerRepositorio<AccionEntity> _repositorio;
    private readonly IMapper<AccionEntity, AccionDTO> _mapper;

    public ObtenerAccionesUseCase(
        ILeerRepositorio<AccionEntity> repositorio,
        IMapper<AccionEntity, AccionDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<AccionDTO?> ObtenerPorIdAsync(int id)
    {
        var entidad = await _repositorio.ObtenerPorIdAsync(id);
        return entidad != null ? _mapper.Map(entidad) : null;
    }

    public async Task<IEnumerable<AccionDTO>> ObtenerTodosAsync()
    {
        var entidades = await _repositorio.ObtenerTodosAsync();
        return entidades.Select(_mapper.Map);
    }
}