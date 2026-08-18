using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.CategoriaRamas.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiEducaramaBackend.Controladores;

[ApiController]
[Route("api/categoria-ramas")]
public class CategoriaRamasController : ControllerBase
{
    private readonly ILeerUseCase<CategoriaRamaDTO> _leerUseCase;

    public CategoriaRamasController(ILeerUseCase<CategoriaRamaDTO> leerUseCase)
    {
        _leerUseCase = leerUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var categoriasRamas = await _leerUseCase.ObtenerTodosAsync();
            return Ok(categoriasRamas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Error interno del servidor",
                detalle = ex.Message
            });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var categoriaRama = await _leerUseCase.ObtenerPorIdAsync(id);
            if (categoriaRama == null)
            {
                return NotFound();
            }
            return Ok(categoriaRama);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Error interno del servidor",
                detalle = ex.Message
            });
        }
    }
}
