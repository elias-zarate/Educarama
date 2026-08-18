using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Patrullas.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/patrullas")]
public class PatrullasController : ControllerBase
{
    private readonly ILeerUseCase<PatrullaDTO> _leerUseCase;
    private readonly ILeerPatrullasPorRamaUseCase _leerPorRamaUseCase;

    public PatrullasController(
        ILeerUseCase<PatrullaDTO> leerUseCase,
        ILeerPatrullasPorRamaUseCase leerPorRamaUseCase)
    {
        _leerUseCase = leerUseCase;
        _leerPorRamaUseCase = leerPorRamaUseCase;
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
}