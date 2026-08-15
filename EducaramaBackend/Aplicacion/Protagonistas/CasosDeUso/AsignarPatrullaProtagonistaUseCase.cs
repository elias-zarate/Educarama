using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Protagonistas.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Protagonistas.CasosDeUso;

public class AsignarPatrullaProtagonistaUseCase : IActualizarUseCase<AsignarPatrullaDTO>
{
    private readonly ILeerRepositorio<ProtagonistaEntity> _leerRepositorio;
    private readonly IActualizarRepositorio<ProtagonistaEntity> _actualizarRepositorio;

    public AsignarPatrullaProtagonistaUseCase(
        ILeerRepositorio<ProtagonistaEntity> leerRepositorio, 
        IActualizarRepositorio<ProtagonistaEntity> actualizarRepositorio)
    {
        _leerRepositorio = leerRepositorio;
        _actualizarRepositorio = actualizarRepositorio;
    }

    public async Task ActualizarAsync(AsignarPatrullaDTO dto, int id)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto), "El Dato es nulo");

        var entidad = await _leerRepositorio.ObtenerPorIdAsync(id);

        if (entidad is null)
            throw new KeyNotFoundException($"El protagonista con id {id} no existe");

        entidad.IdPatrulla = dto.IdPatrulla;

        await _actualizarRepositorio.ActualizarAsync(entidad, id);
    }
}
