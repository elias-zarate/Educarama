using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Usuarios.DTOs;
using Dominio;

namespace Aplicacion.Usuarios.CasosDeUso;

public class SolicitarRecuperacionUseCase
{
    private readonly ILeerUsuarioPorEmailRepositorio _repositorio;

    public SolicitarRecuperacionUseCase(ILeerUsuarioPorEmailRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<string> SolicitarAsync(RecuperarContraseniaDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "El DTO no puede ser nulo");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("El email es obligatorio");

        var usuario = await _repositorio.ObtenerPorEmailAsync(dto.Email);

        if (usuario == null)
            throw new KeyNotFoundException($"No se encontró un usuario con el email: {dto.Email}");

        // MVP: Generamos un token simulado de 6 caracteres.
        // TODO: Implementar servicio de envío de correos y persistencia del token.
        string tokenSimulado = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

        return tokenSimulado;
    }
}