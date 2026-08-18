using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Insignias.DTOs; 
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/obtenciones")]
public class ObtencionesController : ControllerBase
{
    private readonly ICrearUseCase<ObtenerInsigniaDTO> _crearUseCase;

    public ObtencionesController(ICrearUseCase<ObtenerInsigniaDTO> crearUseCase)
    {
        _crearUseCase = crearUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ObtenerInsigniaDTO dto)
    {
        try
        {
            await _crearUseCase.AgregarAsync(dto);
            return StatusCode(201, new { mensaje = "Insignia otorgada exitosamente" });
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