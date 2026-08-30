using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Desarrolla.CasosDeUso;
using Aplicacion.Desarrolla.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/competencias-protagonista")]
public class DesarrollaController : ControllerBase
{
    private readonly ICrearUseCase<AsignarCompetenciaDTO> _asignarUseCase;
    private readonly ObtenerCompetenciasPorProtagonistaUseCase _obtenerPorProtagonistaUseCase;

    public DesarrollaController(
        ICrearUseCase<AsignarCompetenciaDTO> asignarUseCase,
        ObtenerCompetenciasPorProtagonistaUseCase obtenerPorProtagonistaUseCase)
    {
        _asignarUseCase = asignarUseCase;
        _obtenerPorProtagonistaUseCase = obtenerPorProtagonistaUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AsignarCompetenciaDTO dto)
    {
        try
        {
            await _asignarUseCase.AgregarAsync(dto);
            return StatusCode(201, new { mensaje = "Competencia asignada exitosamente al protagonista." });
        }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
        catch (Exception ex) { return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); }
    }

    [HttpGet("protagonista/{idProtagonista:int}")]
    public async Task<IActionResult> GetPorProtagonista(int idProtagonista)
    {
        try
        {
            var competencias = await _obtenerPorProtagonistaUseCase.EjecutarAsync(idProtagonista);
            return Ok(competencias);
        }
        catch (Exception ex) { return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); }
    }
}