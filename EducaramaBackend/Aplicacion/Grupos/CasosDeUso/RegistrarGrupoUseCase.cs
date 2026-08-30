using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Grupos.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Grupos.CasosDeUso;

public class RegistrarGrupoUseCase : ICrearUseCase<RegistroGrupoDTO>
{
    private readonly ICrearRepositorio<GrupoEntity> _grupoRepositorio;
    private readonly ICrearRepositorio<RamaEntity> _ramaRepositorio;
    private readonly IAsignarGrupoUsuarioRepositorio _asignacionRepositorio;

    public RegistrarGrupoUseCase(
        ICrearRepositorio<GrupoEntity> grupoRepositorio,
        ICrearRepositorio<RamaEntity> ramaRepositorio,
        IAsignarGrupoUsuarioRepositorio asignacionRepositorio)
    {
        _grupoRepositorio = grupoRepositorio;
        _ramaRepositorio = ramaRepositorio;
        _asignacionRepositorio = asignacionRepositorio;
    }

    public async Task AgregarAsync(RegistroGrupoDTO dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.NombreGrupo))
            throw new ArgumentException("El nombre del grupo es obligatorio.");

        var nuevoGrupo = new GrupoEntity
        {
            Nombre = dto.NombreGrupo,
            Codigo = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()
        };

        await _grupoRepositorio.AgregarAsync(nuevoGrupo);

        var ramasBase = new List<RamaEntity>
        {
            new RamaEntity { Nombre = "Manada", IdCategoriaRama = (int)CategoriasRama.LobatosYLobeznas, IdGrupo = nuevoGrupo.Id, CodigoAcceso = Guid.NewGuid().ToString("N").Substring(0,6).ToUpper() },
            new RamaEntity { Nombre = "Unidad Scout", IdCategoriaRama = (int)CategoriasRama.Scouts, IdGrupo = nuevoGrupo.Id, CodigoAcceso = Guid.NewGuid().ToString("N").Substring(0,6).ToUpper() },
            new RamaEntity { Nombre = "Caminantes", IdCategoriaRama = (int)CategoriasRama.Caminantes, IdGrupo = nuevoGrupo.Id, CodigoAcceso = Guid.NewGuid().ToString("N").Substring(0,6).ToUpper() },
            new RamaEntity { Nombre = "Rovers", IdCategoriaRama = (int)CategoriasRama.Rovers, IdGrupo = nuevoGrupo.Id, CodigoAcceso = Guid.NewGuid().ToString("N").Substring(0,6).ToUpper() }
        };

        foreach (var rama in ramasBase)
        {
            await _ramaRepositorio.AgregarAsync(rama);
        }

        await _asignacionRepositorio.AsignarRolYGrupoAsync(dto.IdUsuario, nuevoGrupo.Id, (int)RolesSistema.JefeDeGrupo);
    }
}