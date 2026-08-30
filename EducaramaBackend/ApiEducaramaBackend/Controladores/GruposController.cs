using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Grupos.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/grupos")]
public class GruposController : ControllerBase
{
    private readonly ILeerUseCase<GrupoDTO> _leerUseCase;
    private readonly ICrearUseCase<RegistroGrupoDTO> _crearUseCase;

    public GruposController(
        ILeerUseCase<GrupoDTO> leerUseCase,
        ICrearUseCase<RegistroGrupoDTO> crearUseCase)
    {
        _leerUseCase = leerUseCase;
        _crearUseCase = crearUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var grupos = await _leerUseCase.ObtenerTodosAsync();
            return Ok(grupos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegistroGrupoDTO dto)
    {
        try
        {
            await _crearUseCase.AgregarAsync(dto);
            return StatusCode(201, new { mensaje = "Grupo creado y ramas inicializadas con éxito." });
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