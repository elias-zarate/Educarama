using Aplicacion.Usuarios.CasosDeUso;
using Aplicacion.Usuarios.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly LoginUsuarioUseCase _loginUseCase;

    public AuthController(LoginUsuarioUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        try
        {
            var perfil = await _loginUseCase.IniciarSesionAsync(dto);
            return Ok(new { mensaje = "Inicio de sesión exitoso", usuario = perfil });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno", detalle = ex.Message });
        }
    }
}