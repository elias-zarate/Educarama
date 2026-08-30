using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Patrullas.DTOs;
using Dominio;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Patrullas.CasosDeUso;

public class RegistrarPatrullaUseCase : ICrearUseCase<RegistroPatrullaDTO>
{
    private readonly ICrearRepositorio<PatrullaEntity> _repositorio;

    public RegistrarPatrullaUseCase(ICrearRepositorio<PatrullaEntity> repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task AgregarAsync(RegistroPatrullaDTO dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
            throw new ArgumentException("El nombre de la patrulla es obligatorio.");

        var entidad = new PatrullaEntity { Nombre = dto.Nombre, IdRama = dto.IdRama };
        await _repositorio.AgregarAsync(entidad);
    }
}