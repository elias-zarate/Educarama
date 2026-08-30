using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Patrullas.DTOs;
using Dominio;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Patrullas.CasosDeUso;

public class ActualizarPatrullaUseCase : IActualizarUseCase<PatrullaDTO>
{
    private readonly IActualizarRepositorio<PatrullaEntity> _repositorio;

    public ActualizarPatrullaUseCase(IActualizarRepositorio<PatrullaEntity> repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task ActualizarAsync(PatrullaDTO dto, int id)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        if (id != dto.Id) throw new ArgumentException("El ID de la ruta no coincide con el objeto.");

        var entidad = new PatrullaEntity { Id = id, Nombre = dto.Nombre, IdRama = dto.IdRama };
        await _repositorio.ActualizarAsync(entidad);
    }
}