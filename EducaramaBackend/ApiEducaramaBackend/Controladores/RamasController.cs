using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Ramas.CasosDeUso;
using Aplicacion.Ramas.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RamasController : ControllerBase
{
    private readonly GenerarCodigoAccesoRamaUseCase _generarCodigoUseCase;
    private readonly ILeerUseCase<RamaDTO> _leerRamasUseCase;

    public RamasController(
        GenerarCodigoAccesoRamaUseCase generarCodigoUseCase,
        ILeerUseCase<RamaDTO> leerRamasUseCase)
    {
        _generarCodigoUseCase = generarCodigoUseCase;
        _leerRamasUseCase = leerRamasUseCase;
    }

    // Endpoint GET que estaba buscando tu Javascript
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var ramas = await _leerRamasUseCase.ObtenerTodosAsync();
            return Ok(ramas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    // El endpoint POST que ya tenías
    [HttpPost("{id}/generar-codigo")]
    public async Task<IActionResult> GenerarCodigoAcceso(int id)
    {
        try
        {
            string codigo = await _generarCodigoUseCase.GenerarCodigoAsync(id);
            return Ok(new { mensaje = "Código generado con éxito", codigoAcceso = codigo });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}