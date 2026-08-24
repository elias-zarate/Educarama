using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Competencias.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/competencias")]
public class CompetenciasController : ControllerBase
{
    private readonly ILeerUseCase<CompetenciaDTO> _leerUseCase;

    public CompetenciasController(ILeerUseCase<CompetenciaDTO> leerUseCase)
    {
        _leerUseCase = leerUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var competencias = await _leerUseCase.ObtenerTodosAsync();
            return Ok(competencias);
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
            var competencia = await _leerUseCase.ObtenerPorIdAsync(id);
            if (competencia == null)
            {
                return NotFound(new { error = $"No se encontró la competencia con ID {id}." });
            }
            return Ok(competencia);
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