using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Cumple.DTOs;
using Dominio;

namespace Aplicacion.Cumple.CasosDeUso;

public class RegistrarCumplimientoAccionUseCase : ICrearUseCase<CumpleDTO>
{
    private readonly ICrearRepositorio<CumpleEntity> _cumpleRepositorio;
    private readonly IMapper<CumpleDTO, CumpleEntity> _mapper;

    public RegistrarCumplimientoAccionUseCase(
        ICrearRepositorio<CumpleEntity> cumpleRepositorio, 
        IMapper<CumpleDTO, CumpleEntity> mapper)
    {
        _cumpleRepositorio = cumpleRepositorio;
        _mapper = mapper;
    }

    public async Task AgregarAsync(CumpleDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "No se recibieron los" +
                " datos necesarios para registrar el cumplimiento de la acción.");

        var entidad = _mapper.Map(dto);

        await _cumpleRepositorio.AgregarAsync(entidad);
    }
}
