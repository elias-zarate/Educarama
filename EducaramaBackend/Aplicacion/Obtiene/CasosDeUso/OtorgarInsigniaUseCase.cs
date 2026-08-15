using System;
using System.Threading.Tasks;
using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Obtiene.DTOs;
using Dominio;

namespace Aplicacion.Obtiene.CasosDeUso;

public class OtorgarInsigniaUseCase : ICrearUseCase<ObtieneDTO>
{
    private readonly ICrearRepositorio<ObtieneEntity> _repositorio;
    private readonly IMapper<ObtieneDTO, ObtieneEntity> _mapper;

    public OtorgarInsigniaUseCase(
        ICrearRepositorio<ObtieneEntity> repositorio,
        IMapper<ObtieneDTO, ObtieneEntity> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task AgregarAsync(ObtieneDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        var entidad = _mapper.Map(dto);
        await _repositorio.AgregarAsync(entidad);
    }
}