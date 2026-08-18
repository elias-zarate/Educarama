using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Protagonistas.CasosDeUso;
using Aplicacion.Protagonistas.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/protagonistas")]
public class ProtagonistasController : ControllerBase
{
    private readonly ICrearUseCase<RegistroProtagonistaDTO> _crearUseCase;
    private readonly ILeerUseCase<PerfilProtagonistaDTO> _leerUseCase;
    private readonly IActualizarUseCase<PerfilProtagonistaDTO> _actualizarUseCase;
    private readonly IEliminarUseCase _eliminarUseCase;
    private readonly IActualizarUseCase<AsignarPatrullaDTO> _asignarPatrullaUseCase;
    private readonly IActualizarUseCase<CambiarRamaProtagonistaDTO> _cambiarRamaUseCase;
    private readonly ObtenerProtagonistasPorRamaUseCase _obtenerPorRamaUseCase;

    public ProtagonistasController(
        ICrearUseCase<RegistroProtagonistaDTO> crearUseCase,
        ILeerUseCase<PerfilProtagonistaDTO> leerUseCase,
        IActualizarUseCase<PerfilProtagonistaDTO> actualizarUseCase,
        IEliminarUseCase eliminarUseCase,
        IActualizarUseCase<AsignarPatrullaDTO> asignarPatrullaUseCase,
        IActualizarUseCase<CambiarRamaProtagonistaDTO> cambiarRamaUseCase,
        ObtenerProtagonistasPorRamaUseCase obtenerPorRamaUseCase)
    {
        _crearUseCase = crearUseCase;
        _leerUseCase = leerUseCase;
        _actualizarUseCase = actualizarUseCase;
        _eliminarUseCase = eliminarUseCase;
        _asignarPatrullaUseCase = asignarPatrullaUseCase;
        _cambiarRamaUseCase = cambiarRamaUseCase;
        _obtenerPorRamaUseCase = obtenerPorRamaUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var protagonistas = await _leerUseCase.ObtenerTodosAsync();
            return Ok(protagonistas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var protagonista = await _leerUseCase.ObtenerPorIdAsync(id);
            return Ok(protagonista);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    [HttpGet("por-rama/{idRama:int}")]
    public async Task<IActionResult> GetPorRama(int idRama)
    {
        try
        {
            var protagonistas = await _obtenerPorRamaUseCase.EjecutarAsync(idRama);
            return Ok(protagonistas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegistroProtagonistaDTO dto)
    {
        try
        {
            await _crearUseCase.AgregarAsync(dto);
            return StatusCode(201, new { mensaje = "Protagonista creado exitosamente" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] PerfilProtagonistaDTO dto)
    {
        try
        {
            await _actualizarUseCase.ActualizarAsync(dto, id);
            return Ok(new { mensaje = "Protagonista actualizado exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _eliminarUseCase.EliminarAsync(id);
            return Ok(new { mensaje = "Protagonista eliminado exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    [HttpPatch("{id:int}/patrulla")]
    public async Task<IActionResult> PatchPatrulla(int id, [FromBody] AsignarPatrullaDTO dto)
    {
        try
        {
            await _asignarPatrullaUseCase.ActualizarAsync(dto, id);
            return Ok(new { mensaje = "Patrulla asignada exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    [HttpPatch("{id:int}/rama")]
    public async Task<IActionResult> PatchRama(int id, [FromBody] CambiarRamaProtagonistaDTO dto)
    {
        try
        {
            await _cambiarRamaUseCase.ActualizarAsync(dto, id);
            return Ok(new { mensaje = "Rama cambiada exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }
}