using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Usuarios.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Aplicacion.Usuarios.CasosDeUso;

public class LoginUsuarioUseCase
{
    private readonly ILeerUsuarioPorEmailRepositorio _repositorio;
    private readonly IMapper<UsuarioEntity, PerfilUsuarioDTO> _mapper;

    public LoginUsuarioUseCase(ILeerUsuarioPorEmailRepositorio repositorio,
        IMapper<UsuarioEntity, PerfilUsuarioDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<PerfilUsuarioDTO> IniciarSesionAsync(LoginDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Contrasenia))
            throw new ArgumentNullException("El email y la contraseña son obligatorios");

        var usuario = await _repositorio.ObtenerPorEmailAsync(dto.Email);

        if (usuario is null)
            throw new UnauthorizedAccessException("No existe un usuario con ese correo.");

        if (usuario.Contrasenia != dto.Contrasenia)
            throw new UnauthorizedAccessException("La contraseña es incorrecta.");

        // Si todo está bien, mapeamos la entidad al Perfil DTO (que no expone la clave y tiene los roles)
        return _mapper.Map(usuario);
    }
}
