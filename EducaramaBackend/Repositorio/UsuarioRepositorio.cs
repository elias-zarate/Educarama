using Aplicacion.Abstracciones.Repositorios;
using Datos.EntidadesDb;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio;

public class UsuarioRepositorio : ICrearRepositorio<UsuarioEntity>, 
    ILeerRepositorio<UsuarioEntity>, IActualizarRepositorio<UsuarioEntity>, 
    IEliminarRepositorio, ILeerUsuarioPorEmailRepositorio
{
    private readonly EducaramaContext _context;

    public UsuarioRepositorio(EducaramaContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(UsuarioEntity entidad)
    {
        var usuario = MapToModel(entidad);
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        
        entidad.Id = usuario.Id; //se la agregué recien
    }

    public async Task<UsuarioEntity?> ObtenerPorIdAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario is null)
            throw new KeyNotFoundException($"No se encontró el usuario con Id {id}.");

        return MapToEntity(usuario);
    }

    public async Task<IEnumerable<UsuarioEntity>> ObtenerTodosAsync()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return usuarios.Select(MapToEntity);
    }

    public async Task ActualizarAsync(UsuarioEntity entidad)
    {
        var usuario = await _context.Usuarios.FindAsync(entidad.Id);
        if (usuario is null)
            throw new KeyNotFoundException($"No se encontró el usuario con Id {entidad.Id}.");

        usuario.Nombre = entidad.Nombre;
        usuario.Email = entidad.Email;
        usuario.Contrasenia = entidad.Contrasenia;

        await _context.SaveChangesAsync();
        
    }

    public async Task EliminarAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<UsuarioEntity> ObtenerPorEmailAsync(string email)
    {
        var usuario = await _context.Usuarios.Include(u => u.AsignacionRol).
            FirstOrDefaultAsync(u => u.Email == email);

        if (usuario is null)
            return null;

        var usuarioEntity = MapToEntity(usuario);

        var asignacion = usuario.AsignacionRol.FirstOrDefault();

        if (asignacion is not null)
        {
            usuarioEntity.AsignacionesRoles = new List<AsignacionRolEntity>
            {
                new AsignacionRolEntity
                {
                    Id = asignacion.Id,
                    IdUsuario = asignacion.IdUsuario,
                    IdRol = asignacion.IdRol,
                    IdGrupo = asignacion.IdGrupo,
                    IdRama = asignacion.IdRama
                }
            };
        }

        return usuarioEntity;
    }


    #region
    private static UsuarioEntity MapToEntity(Usuario usuario)
    {
        return new UsuarioEntity
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre!,
            Email = usuario.Email!,
            Contrasenia = usuario.Contrasenia!
        };
    }

    private static Usuario MapToModel(UsuarioEntity usuarioEntity)
    {
        return new Usuario
        {
            Nombre = usuarioEntity.Nombre,
            Email = usuarioEntity.Email,
            Contrasenia = usuarioEntity.Contrasenia
        };
    }

    
    #endregion
}
