using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Acciones.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/acciones")]
public class AccionesController : ControllerBase
{
    private readonly ILeerUseCase<AccionDTO> _leerUseCase;

    public AccionesController(ILeerUseCase<AccionDTO> leerUseCase)
    {
        _leerUseCase = leerUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var acciones = await _leerUseCase.ObtenerTodosAsync();
            return Ok(acciones);
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
            var accion = await _leerUseCase.ObtenerPorIdAsync(id);
            if (accion == null)
            {
                return NotFound(new { error = $"No se encontró la acción con ID {id}." });
            }
            return Ok(accion);
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