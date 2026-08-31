using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Patrullas.DTOs;
using Aplicacion.Usuarios.CasosDeUso;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Patrullas.CasosDeUso;

public class ObtenerPatrullasPorContextoUseCase
{
    private readonly ILeerRepositorio<PatrullaEntity> _patrullaRepositorio;
    private readonly ILeerRepositorio<RamaEntity> _ramaRepositorio;
    private readonly ObtenerContextoUsuarioUseCase _obtenerContextoUseCase;

    public ObtenerPatrullasPorContextoUseCase(
        ILeerRepositorio<PatrullaEntity> patrullaRepositorio,
        ILeerRepositorio<RamaEntity> ramaRepositorio,
        ObtenerContextoUsuarioUseCase obtenerContextoUseCase)
    {
        _patrullaRepositorio = patrullaRepositorio;
        _ramaRepositorio = ramaRepositorio;
        _obtenerContextoUseCase = obtenerContextoUseCase;
    }

    public async Task<IEnumerable<PatrullaEntity>> ObtenerAsync(int idUsuarioLogueado, int? idRamaFiltro = null)
    {
        // 1. Obtenemos el contexto del usuario actual
        var contexto = await _obtenerContextoUseCase.ObtenerAsync(idUsuarioLogueado);

        // 2. Traemos todas las patrullas y ramas existentes
        var patrullas = await _patrullaRepositorio.ObtenerTodosAsync();
        var ramas = await _ramaRepositorio.ObtenerTodosAsync();

        // Cruzamos los datos en memoria para asociar cada patrulla con su rama y su grupo
        var query = from p in patrullas
                    join r in ramas on p.IdRama equals r.Id
                    select new { Patrulla = p, Rama = r };

        // 3. Aplicamos las reglas de filtrado según el rol
        if (contexto.EsJefeDeGrupo)
        {
            // El Jefe de Grupo ve todas las patrullas de su grupo scout
            query = query.Where(x => x.Rama.IdGrupo == contexto.IdGrupo);

            // Si además quiso filtrar por una rama específica de su grupo, la acotamos
            if (idRamaFiltro.HasValue)
            {
                query = query.Where(x => x.Patrulla.IdRama == idRamaFiltro.Value);
            }
        }
        else if (contexto.EsRolLimitado) // Jefe de Rama o Educador
        {
            // Forzosamente ven únicamente las patrullas de SU propia rama asignada
            if (contexto.IdRama == null)
                return Enumerable.Empty<PatrullaEntity>();

            query = query.Where(x => x.Patrulla.IdRama == contexto.IdRama.Value);
        }
        else
        {
            throw new UnauthorizedAccessException("Tu rol no tiene permisos para ver las patrullas.");
        }

        return query.Select(x => x.Patrulla);
    }
}