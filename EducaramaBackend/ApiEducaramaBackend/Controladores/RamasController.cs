using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Ramas.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/ramas")]
public class RamasController : ControllerBase
{
    private readonly ILeerUseCase<RamaDTO> _leerUseCase;
    private readonly ICrearUseCase<RegistroRamaDTO> _crearUseCase;

    public RamasController(ILeerUseCase<RamaDTO> leerUseCase, 
        ICrearUseCase<RegistroRamaDTO> crearUseCase)
    {
        _leerUseCase = leerUseCase;
        _crearUseCase = crearUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var ramas = await _leerUseCase.ObtenerTodosAsync();
            return Ok(ramas);
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var rama = await _leerUseCase.ObtenerPorIdAsync(id);
            if (rama == null)
            {
                return NotFound();
            }
            return Ok(rama);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegistroRamaDTO registroRama)
    {
        try
        {
            await _crearUseCase.AgregarAsync(registroRama);
            return StatusCode(201, new { mensaje = "Rama creada exitosamente" });
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
}
