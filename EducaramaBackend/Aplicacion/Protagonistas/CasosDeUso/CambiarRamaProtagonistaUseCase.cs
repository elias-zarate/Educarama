using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Protagonistas.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Protagonistas.CasosDeUso;

public class CambiarRamaProtagonistaUseCase : IActualizarUseCase<CambiarRamaProtagonistaDTO>
{
    private readonly ILeerRepositorio<ProtagonistaEntity> _repositorioLeer;
    private readonly IActualizarRepositorio<ProtagonistaEntity> _repositorioActualizar;

    public CambiarRamaProtagonistaUseCase(
        ILeerRepositorio<ProtagonistaEntity> repositorioLeer, 
        IActualizarRepositorio<ProtagonistaEntity> repositorioActualizar)
    {
        _repositorioLeer = repositorioLeer;
        _repositorioActualizar = repositorioActualizar;
    }

    public async Task ActualizarAsync(CambiarRamaProtagonistaDTO dto, int id)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto), "El DTO no puede ser nulo");

        var entidad = await _repositorioLeer.ObtenerPorIdAsync(id);

        if (entidad is null)
            throw new KeyNotFoundException($"No se encontró un protagonista con el ID: {id}");

        entidad.IdRama = dto.IdRama;
        entidad.IdPatrulla = null;

        await _repositorioActualizar.ActualizarAsync(entidad);
    }
}
