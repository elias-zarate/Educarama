using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Patrullas.CasosDeUso;
using Aplicacion.Patrullas.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/patrullas")]
public class PatrullasController : ControllerBase
{
    private readonly ICrearUseCase<RegistroPatrullaDTO> _crearUseCase;
    private readonly ILeerUseCase<PatrullaDTO> _leerUseCase;
    private readonly ILeerPatrullasPorRamaUseCase _leerPorRamaUseCase;
    private readonly IActualizarUseCase<PatrullaDTO> _actualizarUseCase;
    private readonly IEliminarUseCase _eliminarUseCase;

    public PatrullasController(ICrearUseCase<RegistroPatrullaDTO> crearUseCase,
        ILeerUseCase<PatrullaDTO> leerUseCase,
        ILeerPatrullasPorRamaUseCase leerPorRamaUseCase,
        IActualizarUseCase<PatrullaDTO> actualizarUseCase,
        IEliminarUseCase eliminarUseCase)
    {
        _leerUseCase = leerUseCase;
        _leerPorRamaUseCase = leerPorRamaUseCase;
        _crearUseCase = crearUseCase;
        _actualizarUseCase = actualizarUseCase;
        _eliminarUseCase = eliminarUseCase;
        
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var patrullas = await _leerUseCase.ObtenerTodosAsync();
            return Ok(patrullas);
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
            var patrulla = await _leerUseCase.ObtenerPorIdAsync(id);
            return Ok(patrulla);
        }
        catch (ArgumentException ex) // Tu caso de uso tira ArgumentException si es null
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
            var patrullas = await _leerPorRamaUseCase.ObtenerPatrullasPorRamaAsync(idRama);
            return Ok(patrullas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    [HttpGet("filtradas")]
    public async Task<IActionResult> ObtenerPorContexto(
    [FromQuery] int idUsuarioLogueado,
    [FromQuery] int? idRama,
    [FromServices] ObtenerPatrullasPorContextoUseCase useCase)
    {
        try
        {
            var patrullas = await useCase.ObtenerAsync(idUsuarioLogueado, idRama);
            return Ok(patrullas);
        }
        catch (UnauthorizedAccessException ex) { return StatusCode(403, new { error = ex.Message }); }
        catch (Exception ex) { return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegistroPatrullaDTO dto)
    {
        try
        {
            await _crearUseCase.AgregarAsync(dto);
            return StatusCode(201, new { mensaje = "Patrulla creada exitosamente" });
        }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
        catch (UnauthorizedAccessException ex) { return StatusCode(403, new { error = ex.Message }); } // <-- NUEVO (403 Forbidden)
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); } // <-- NUEVO (404 Not Found)
        catch (Exception ex) { return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] PatrullaDTO dto)
    {
        try
        {
            await _actualizarUseCase.ActualizarAsync(dto, id);
            return Ok(new { mensaje = "Patrulla actualizada exitosamente" });
        }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
        catch (Exception ex) { return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _eliminarUseCase.EliminarAsync(id);
            return Ok(new { mensaje = "Patrulla eliminada exitosamente" });
        }
        catch (Exception ex) { return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); }
    }
}