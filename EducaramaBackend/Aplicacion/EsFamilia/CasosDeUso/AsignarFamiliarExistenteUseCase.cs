using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Familiares.DTOs;
using Dominio;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Familiares.CasosDeUso;

public class AsignarFamiliarExistenteUseCase : ICrearUseCase<AsignarFamiliarExistenteDTO>
{
    private readonly IEsFamiliaRepositorio _esFamiliaRepositorio;

    public AsignarFamiliarExistenteUseCase(IEsFamiliaRepositorio esFamiliaRepositorio)
    {
        _esFamiliaRepositorio = esFamiliaRepositorio;
    }

    public async Task AgregarAsync(AsignarFamiliarExistenteDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var esFamilia = new EsFamiliaEntity
        {
            IdProtagonista = dto.IdProtagonista,
            IdFamiliar = dto.IdFamiliar,
            Vinculo = dto.Vinculo,
            EsContactoDeEmergencia = dto.EsContactoDeEmergencia
        };

        await _esFamiliaRepositorio.AgregarAsync(esFamilia);
    }
}