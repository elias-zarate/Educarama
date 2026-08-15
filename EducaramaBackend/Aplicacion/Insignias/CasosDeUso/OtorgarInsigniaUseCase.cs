using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Insignias.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Insignias.CasosDeUso;

public class OtorgarInsigniaUseCase : ICrearUseCase<ObtenerInsigniaDTO>
{
    private readonly ICrearRepositorio<ObtieneEntity> _repositorio;
    private readonly IMapper<ObtenerInsigniaDTO, ObtieneEntity> _mapper;

    public OtorgarInsigniaUseCase(ICrearRepositorio<ObtieneEntity> repositorio, 
        IMapper<ObtenerInsigniaDTO, ObtieneEntity> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task AgregarAsync(ObtenerInsigniaDTO dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        var insigniaEntity = _mapper.Map(dto);

        await _repositorio.AgregarAsync(insigniaEntity);

    }
}
