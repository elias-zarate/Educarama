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
    private readonly SolicitarRecuperacionUseCase _solicitarRecuperacionUseCase;
    private readonly RestablecerContraseniaUseCase _restablecerContraseniaUseCase;

    public AuthController(LoginUsuarioUseCase loginUseCase, SolicitarRecuperacionUseCase solicitarRecuperacionUseCase,
            RestablecerContraseniaUseCase restablecerContraseniaUseCase)
    {
        _loginUseCase = loginUseCase;
        _solicitarRecuperacionUseCase = solicitarRecuperacionUseCase;
        _restablecerContraseniaUseCase = restablecerContraseniaUseCase;
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

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutDTO dto, [FromServices] CerrarSesionUseCase cerrarSesionUseCase)
    {
        try
        {
            await cerrarSesionUseCase.CerrarSesionAsync(dto);
            // El backend responde 200 OK. La limpieza real (sessionStorage) la hace el frontend al recibir esto.
            return Ok(new { mensaje = "Sesión cerrada correctamente." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno al cerrar sesión", detalle = ex.Message });
        }
    }

    [HttpPost("recuperar-contrasenia")]
    public async Task<IActionResult> SolicitarRecuperacion([FromBody] RecuperarContraseniaDTO dto)
    {
        try
        {
            var token = await _solicitarRecuperacionUseCase.SolicitarAsync(dto);

            // MVP: Devolvemos el token en la respuesta para poder armar el frontend rápido.
            // En un entorno de producción estricto, acá devolverías un Ok genérico sin revelar el token.
            return Ok(new { mensaje = "Solicitud procesada con éxito.", token = token });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno al procesar la solicitud." });
        }
    }

    [HttpPost("restablecer")]
    public async Task<IActionResult> RestablecerContrasenia([FromBody] CambiarContraseniaDTO dto)
    {
        try
        {
            await _restablecerContraseniaUseCase.RestablecerAsync(dto);
            return Ok(new { mensaje = "La contraseña ha sido actualizada correctamente." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno al intentar restablecer la contraseña." });
        }
    }
}