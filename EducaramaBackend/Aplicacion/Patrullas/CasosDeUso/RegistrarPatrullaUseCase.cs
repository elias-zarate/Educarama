using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Patrullas.DTOs;
using Aplicacion.Usuarios.CasosDeUso;
using Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Patrullas.CasosDeUso;

public class RegistrarPatrullaUseCase : ICrearUseCase<RegistroPatrullaDTO>
{
    private readonly ICrearRepositorio<PatrullaEntity> _patrullaRepositorio;
    private readonly ILeerRepositorio<RamaEntity> _ramaRepositorio; // Para traer la rama y ver a qué grupo pertenece
    private readonly ObtenerContextoUsuarioUseCase _obtenerContextoUseCase; // Nuestro guardián de permisos

    public RegistrarPatrullaUseCase(
        ICrearRepositorio<PatrullaEntity> patrullaRepositorio,
        ILeerRepositorio<RamaEntity> ramaRepositorio,
        ObtenerContextoUsuarioUseCase obtenerContextoUseCase)
    {
        _patrullaRepositorio = patrullaRepositorio;
        _ramaRepositorio = ramaRepositorio;
        _obtenerContextoUseCase = obtenerContextoUseCase;
    }

    public async Task AgregarAsync(RegistroPatrullaDTO dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
            throw new ArgumentException("El nombre de la patrulla es obligatorio.");

        // 1. Obtenemos el contexto (permisos) de quien hace la petición
        var contexto = await _obtenerContextoUseCase.ObtenerAsync(dto.IdUsuarioLogueado);

        // 2. Traemos la rama a la que le quieren meter la patrulla
        var ramaDestino = await _ramaRepositorio.ObtenerPorIdAsync(dto.IdRama);
        if (ramaDestino == null)
            throw new KeyNotFoundException("La rama especificada no existe en el sistema.");

        // 3. REGLAS DE SEGURIDAD (El Blindaje)
        if (contexto.EsJefeDeGrupo)
        {
            // El Jefe de Grupo solo puede operar en ramas de su propio grupo
            if (ramaDestino.IdGrupo != contexto.IdGrupo)
                throw new UnauthorizedAccessException("No puedes crear patrullas en una rama que pertenece a otro grupo scout.");
        }
        else if (contexto.EsRolLimitado) // Jefe de Rama o Educador
        {
            // Solo pueden crear patrullas dentro de SU propia rama
            if (contexto.IdRama == null || contexto.IdRama != dto.IdRama)
                throw new UnauthorizedAccessException("Permisos insuficientes: Solo puedes crear patrullas dentro de la rama que tienes asignada.");
        }
        else
        {
            throw new UnauthorizedAccessException("Tu rol no tiene permisos para crear patrullas.");
        }

        // 4. Si pasó todos los controles de seguridad, guardamos la patrulla
        var entidad = new PatrullaEntity { Nombre = dto.Nombre, IdRama = dto.IdRama };
        await _patrullaRepositorio.AgregarAsync(entidad);
    }
}