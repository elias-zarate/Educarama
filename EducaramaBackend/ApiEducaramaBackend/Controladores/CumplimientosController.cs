using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Cumple.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/cumplimientos")]
public class CumplimientosController : ControllerBase
{
    private readonly ICrearUseCase<CumpleDTO> _crearUseCase;

    public CumplimientosController(ICrearUseCase<CumpleDTO> crearUseCase)
    {
        _crearUseCase = crearUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CumpleDTO dto)
    {
        try
        {
            await _crearUseCase.AgregarAsync(dto);
            return StatusCode(201, new { mensaje = "Cumplimiento de acción registrado exitosamente" });
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
}