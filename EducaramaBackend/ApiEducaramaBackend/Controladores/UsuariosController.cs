using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Ramas.CasosDeUso;
using Aplicacion.Ramas.DTOs;
using Aplicacion.Usuarios.CasosDeUso;
using Aplicacion.Usuarios.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly ICrearUseCase<RegistroUsuarioDTO> _crearUsuarioUseCase;
    private readonly ObtenerPerfilUsuarioUseCase _obtenerPerfilUsuarioUseCase;
    private readonly IActualizarUseCase<PerfilUsuarioDTO> _actualizarUsuarioUseCase;
    private readonly IEliminarUseCase _eliminarUseCase;

    public UsuariosController(ICrearUseCase<RegistroUsuarioDTO> crearUsuarioUseCase, 
        ObtenerPerfilUsuarioUseCase obtenerPerfilUsuarioUseCase, 
        IActualizarUseCase<PerfilUsuarioDTO> actualizarUsuarioUseCase, 
        IEliminarUseCase eliminarUseCase)
    {
        _crearUsuarioUseCase = crearUsuarioUseCase;
        _obtenerPerfilUsuarioUseCase = obtenerPerfilUsuarioUseCase;
        _actualizarUsuarioUseCase = actualizarUsuarioUseCase;
        _eliminarUseCase = eliminarUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegistroUsuarioDTO registroUsuario)
    {
        try
        {
            await _crearUsuarioUseCase.AgregarAsync(registroUsuario);
            return StatusCode(201, new { mensaje = "Usuario creado exitosamente" });
        }
        catch(ArgumentException ex)
        {
            return BadRequest(new {error = ex.Message});
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", 
                detalle = ex.Message });
        }
    }

    [HttpPost("unirse-grupo")]
    public async Task<IActionResult> UnirseAlGrupo(
    [FromBody] UnirseGrupoDTO dto,
    [FromServices] UnirseGrupoUseCase unirseGrupoUseCase)
    {
        try
        {
            await unirseGrupoUseCase.UnirseAsync(dto);
            return Ok(new { mensaje = "¡Usuario asignado al grupo exitosamente!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("unirse-rama")]
    public async Task<IActionResult> UnirseALaRama(
        [FromBody] UnirseRamaDTO dto,
        [FromServices] UnirseRamaUseCase unirseRamaUseCase)
    {
        try
        {
            await unirseRamaUseCase.UnirseAsync(dto);
            return Ok(new { mensaje = "¡Usuario asignado a la rama exitosamente!" });
        }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
        catch (UnauthorizedAccessException ex) { return StatusCode(403, new { error = ex.Message }); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
        catch (Exception ex) { return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var perfilUsuario = await _obtenerPerfilUsuarioUseCase.ObtenerPorIdAsync(id);
            return Ok(perfilUsuario);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Error interno del servidor",
                detalle = ex.Message
            });
        }
    }

    [HttpGet("perfil/{email}")]
    public async Task<IActionResult> ObtenerPerfil(
    string email,
    [FromServices] ObtenerPerfilPorEmailUseCase useCase)
    {
        try
        {
            var perfil = await useCase.ObtenerPerfilAsync(email);
            return Ok(perfil);
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromBody]PerfilUsuarioDTO perfilUsuarioDto, int id)
    {
        try
        {
            if(perfilUsuarioDto.IdUsuario != id)
            {
                return BadRequest(new { error = "El ID del usuario no coincide" });
            }

            await _actualizarUsuarioUseCase.ActualizarAsync(perfilUsuarioDto, id);

            return Ok(new { mensaje = "Usuario actualizado exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Error interno del servidor",
                detalle = ex.Message
            });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _eliminarUseCase.EliminarAsync(id);

            return Ok(new { mensaje = "Usuario eliminado exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Error interno del servidor",
                detalle = ex.Message
            });
        }
    }

}
