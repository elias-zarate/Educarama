using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Desarrolla.DTOs;
using Dominio;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Desarrolla.CasosDeUso;

public class AsignarCompetenciaUseCase : ICrearUseCase<AsignarCompetenciaDTO>
{
    private readonly IDesarrollaRepositorio _desarrollaRepositorio;

    public AsignarCompetenciaUseCase(IDesarrollaRepositorio desarrollaRepositorio)
    {
        _desarrollaRepositorio = desarrollaRepositorio;
    }

    public async Task AgregarAsync(AsignarCompetenciaDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        var entidad = new DesarrollaEntity
        {
            IdProtagonista = dto.IdProtagonista,
            IdCompetencia = dto.IdCompetencia,
            CodigoCompetencia = dto.CodigoCompetencia,
            CompetenciaDesarrollada = false // Arranca en curso, no terminada
        };

        await _desarrollaRepositorio.AgregarAsync(entidad);
    }
}