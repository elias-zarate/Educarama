using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Ramas.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Ramas.CasosDeUso;

public class CrearRamaUseCase : ICrearUseCase<RegistroRamaDTO>
{
    private readonly ICrearRepositorio<RamaEntity> _repositorio;
    private readonly IMapper<RegistroRamaDTO, RamaEntity> _mapper;

    public CrearRamaUseCase(ICrearRepositorio<RamaEntity> repositorio, 
        IMapper<RegistroRamaDTO, RamaEntity> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task AgregarAsync(RegistroRamaDTO dto)
    {
        if(dto is null)
            throw new ArgumentNullException(nameof(dto));

        var entity = _mapper.Map(dto);
        await _repositorio.AgregarAsync(entity);
    }
}
