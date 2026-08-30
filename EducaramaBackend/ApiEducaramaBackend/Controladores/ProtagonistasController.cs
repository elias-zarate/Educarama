using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Protagonistas.CasosDeUso;
using Aplicacion.Protagonistas.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/protagonistas")]
public class ProtagonistasController : ControllerBase
{
    private readonly ICrearUseCase<RegistroProtagonistaDTO> _crearUseCase;
    private readonly ILeerUseCase<PerfilProtagonistaDTO> _leerUseCase;
    private readonly IActualizarUseCase<PerfilProtagonistaDTO> _actualizarPerfilUseCase;
    private readonly EliminarProtagonistaUseCase _eliminarUseCase;
    private readonly ObtenerProtagonistasPorRamaUseCase _obtenerPorRamaUseCase;
    private readonly IActualizarUseCase<AsignarPatrullaDTO> _asignarPatrullaUseCase;
    private readonly IActualizarUseCase<CambiarRamaProtagonistaDTO> _cambiarRamaUseCase;

    public ProtagonistasController(
        ICrearUseCase<RegistroProtagonistaDTO> crearUseCase,
        ILeerUseCase<PerfilProtagonistaDTO> leerUseCase,
        IActualizarUseCase<PerfilProtagonistaDTO> actualizarPerfilUseCase,
        EliminarProtagonistaUseCase eliminarUseCase,
        ObtenerProtagonistasPorRamaUseCase obtenerPorRamaUseCase,
        IActualizarUseCase<AsignarPatrullaDTO> asignarPatrullaUseCase,
        IActualizarUseCase<CambiarRamaProtagonistaDTO> cambiarRamaUseCase)
    {
        _crearUseCase = crearUseCase;
        _leerUseCase = leerUseCase;
        _actualizarPerfilUseCase = actualizarPerfilUseCase;
        _eliminarUseCase = eliminarUseCase;
        _obtenerPorRamaUseCase = obtenerPorRamaUseCase;
        _asignarPatrullaUseCase = asignarPatrullaUseCase;
        _cambiarRamaUseCase = cambiarRamaUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegistroProtagonistaDTO dto)
    {
        try
        {
            await _crearUseCase.AgregarAsync(dto);
            return StatusCode(201, new { mensaje = "Protagonista registrado exitosamente." });
        }
        catch (Exception ex) { return StatusCode(500, new { error = "Error al registrar", detalle = ex.Message }); }
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var protagonistas = await _leerUseCase.ObtenerTodosAsync();
        return Ok(protagonistas);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var protagonista = await _leerUseCase.ObtenerPorIdAsync(id);
        if (protagonista == null) return NotFound(new { error = "Protagonista no encontrado." });
        return Ok(protagonista);
    }

    [HttpGet("rama/{idRama:int}")]
    public async Task<IActionResult> GetPorRama(int idRama)
    {
        var protagonistas = await _obtenerPorRamaUseCase.EjecutarAsync(idRama);
        return Ok(protagonistas);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] PerfilProtagonistaDTO dto)
    {
        try
        {
            await _actualizarPerfilUseCase.ActualizarAsync(dto, id);
            return Ok(new { mensaje = "Perfil actualizado correctamente." });
        }
        catch (KeyNotFoundException ex) 
        { 
            return NotFound(new { error = ex.Message }); 
        }
        catch (Exception ex) 
        { 
            return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); 
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _eliminarUseCase.EliminarAsync(id);
            return Ok(new { mensaje = "Protagonista eliminado correctamente." });
        }
        catch (KeyNotFoundException ex) 
        { 
            return NotFound(new { error = ex.Message }); 
        }
        catch (Exception ex) 
        { 
            return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); 
        }
    }

    [HttpPatch("{id:int}/patrulla")]
    public async Task<IActionResult> PatchPatrulla(int id, [FromBody] AsignarPatrullaDTO dto)
    {
        try
        {
            await _asignarPatrullaUseCase.ActualizarAsync(dto, id);
            return Ok(new { mensaje = "Patrulla asignada correctamente." });
        }

        catch (KeyNotFoundException ex) 
        { 
            return NotFound(new { error = ex.Message }); 
        }

        catch (Exception ex) 
        { 
            return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); 
        }
    }

    [HttpPatch("{id:int}/rama")]
    public async Task<IActionResult> PatchRama(int id, [FromBody] CambiarRamaProtagonistaDTO dto)
    {
        try
        {
            await _cambiarRamaUseCase.ActualizarAsync(dto, id);
            return Ok(new { mensaje = "Protagonista cambiado de rama correctamente." });
        }
        catch (KeyNotFoundException ex) 
        { 
            return NotFound(new { error = ex.Message }); 
        }
        catch (Exception ex) 
        { 
            return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); 
        }
    }
}