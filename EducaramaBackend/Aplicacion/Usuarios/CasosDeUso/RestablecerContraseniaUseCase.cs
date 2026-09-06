using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Usuarios.DTOs;
using Dominio;

namespace Aplicacion.Usuarios.CasosDeUso;

public class RestablecerContraseniaUseCase
{
    private readonly ILeerUsuarioPorEmailRepositorio _leerRepositorio;
    private readonly IActualizarRepositorio<UsuarioEntity> _actualizarRepositorio;

    public RestablecerContraseniaUseCase(
        ILeerUsuarioPorEmailRepositorio leerRepositorio,
        IActualizarRepositorio<UsuarioEntity> actualizarRepositorio)
    {
        _leerRepositorio = leerRepositorio;
        _actualizarRepositorio = actualizarRepositorio;
    }

    public async Task RestablecerAsync(CambiarContraseniaDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "El DTO no puede ser nulo");

        var usuario = await _leerRepositorio.ObtenerPorEmailAsync(dto.Email);

        if (usuario == null)
            throw new KeyNotFoundException($"No se encontró un usuario con el email: {dto.Email}");

        // MVP: Validación simple del token simulado
        if (string.IsNullOrWhiteSpace(dto.Token))
            throw new ArgumentException("El token ingresado no es válido.");

        // La validación de longitud/vacío ya está cubierta por el setter de Contrasenia en UsuarioEntity
        usuario.Contrasenia = dto.NuevaContrasenia;

        await _actualizarRepositorio.ActualizarAsync(usuario);
    }
}