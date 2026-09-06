using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Avisos.CasosDeUso;
using Aplicacion.Avisos.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/avisos")]
public class AvisosController : ControllerBase
{
    private readonly ObtenerAvisosUseCase _obtenerUseCase;
    private readonly ObtenerAvisosActivosUseCase _obtenerActivosUseCase;
    private readonly ICrearUseCase<RegistroAvisoDTO> _registrarUseCase;

    public AvisosController(
        ObtenerAvisosUseCase obtenerUseCase,
        ObtenerAvisosActivosUseCase obtenerActivosUseCase,
        ICrearUseCase<RegistroAvisoDTO> registrarUseCase)
    {
        _obtenerUseCase = obtenerUseCase;
        _obtenerActivosUseCase = obtenerActivosUseCase;
        _registrarUseCase = registrarUseCase;
    }

    // GET: api/avisos
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var avisos = await _obtenerUseCase.ObtenerTodosAsync();
            return Ok(avisos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno", detalle = ex.Message });
        }
    }

    // GET: api/avisos/activos (El que va a usar el dashboard.html)
    [HttpGet("activos")]
    public async Task<IActionResult> GetActivos()
    {
        try
        {
            var avisos = await _obtenerActivosUseCase.EjecutarAsync();
            return Ok(avisos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno", detalle = ex.Message });
        }
    }

    // POST: api/avisos
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegistroAvisoDTO dto)
    {
        try
        {
            await _registrarUseCase.AgregarAsync(dto);
            return StatusCode(201, new { mensaje = "Aviso creado exitosamente." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno", detalle = ex.Message });
        }
    }
}