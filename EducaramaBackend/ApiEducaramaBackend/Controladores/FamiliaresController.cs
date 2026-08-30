using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Familiares.CasosDeUso;
using Aplicacion.Familiares.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/familiares")]
public class FamiliaresController : ControllerBase
{
    private readonly ICrearUseCase<RegistroFamiliarCompletoDTO> _registrarCompletoUseCase;
    private readonly ICrearUseCase<AsignarFamiliarExistenteDTO> _asignarExistenteUseCase;
    private readonly ObtenerFamiliaresPorProtagonistaUseCase _obtenerPorProtagonistaUseCase;
    private readonly EliminarVinculoFamiliarUseCase _eliminarVinculoUseCase;

    public FamiliaresController(
        ICrearUseCase<RegistroFamiliarCompletoDTO> registrarCompletoUseCase,
        ICrearUseCase<AsignarFamiliarExistenteDTO> asignarExistenteUseCase,
        ObtenerFamiliaresPorProtagonistaUseCase obtenerPorProtagonistaUseCase,
        EliminarVinculoFamiliarUseCase eliminarVinculoUseCase)
    {
        _registrarCompletoUseCase = registrarCompletoUseCase;
        _asignarExistenteUseCase = asignarExistenteUseCase;
        _obtenerPorProtagonistaUseCase = obtenerPorProtagonistaUseCase;
        _eliminarVinculoUseCase = eliminarVinculoUseCase;
    }

    [HttpPost("registro-completo")]
    public async Task<IActionResult> PostRegistroCompleto([FromBody] RegistroFamiliarCompletoDTO dto)
    {
        try
        {
            await _registrarCompletoUseCase.AgregarAsync(dto);
            return StatusCode(201, new { mensaje = "Familiar registrado y vinculado exitosamente." });
        }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
        catch (Exception ex) { return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); }
    }

    [HttpPost("asignar-existente")]
    public async Task<IActionResult> PostAsignarExistente([FromBody] AsignarFamiliarExistenteDTO dto)
    {
        try
        {
            await _asignarExistenteUseCase.AgregarAsync(dto);
            return StatusCode(201, new { mensaje = "Familiar vinculado exitosamente." });
        }
        catch (Exception ex) { return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); }
    }

    [HttpGet("protagonista/{idProtagonista:int}")]
    public async Task<IActionResult> GetPorProtagonista(int idProtagonista)
    {
        var familiares = await _obtenerPorProtagonistaUseCase.EjecutarAsync(idProtagonista);
        return Ok(familiares);
    }

    [HttpDelete("{idFamiliar:int}/protagonista/{idProtagonista:int}")]
    public async Task<IActionResult> DeleteVinculo(int idFamiliar, int idProtagonista)
    {
        try
        {
            await _eliminarVinculoUseCase.EjecutarAsync(idProtagonista, idFamiliar);
            return Ok(new { mensaje = "Vínculo familiar eliminado correctamente." });
        }
        catch (Exception ex) { return StatusCode(500, new { error = "Error interno", detalle = ex.Message }); }
    }
}