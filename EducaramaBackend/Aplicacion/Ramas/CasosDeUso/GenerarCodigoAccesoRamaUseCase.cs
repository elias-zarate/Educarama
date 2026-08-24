using Aplicacion.Abstracciones.Repositorios;
using Dominio;

namespace Aplicacion.Ramas.CasosDeUso;

public class GenerarCodigoAccesoRamaUseCase
{
    private readonly ILeerRepositorio<RamaEntity> _repositorioLeer;
    private readonly IActualizarRepositorio<RamaEntity> _repositorioActualizar;

    public GenerarCodigoAccesoRamaUseCase(
        ILeerRepositorio<RamaEntity> repositorioLeer,
        IActualizarRepositorio<RamaEntity> repositorioActualizar)
    {
        _repositorioLeer = repositorioLeer;
        _repositorioActualizar = repositorioActualizar;
    }

    public async Task<string> GenerarCodigoAsync(int idRama)
    {
        // 1. Buscamos la rama existente por su ID
        var ramaEntity = await _repositorioLeer.ObtenerPorIdAsync(idRama);

        if (ramaEntity is null)
        {
            throw new KeyNotFoundException($"No se encontró la rama con ID {idRama}.");
        }

        // 2. Generamos el código alfanumérico estilo Classroom (6 caracteres)
        string codigoGenerado = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

        // 3. Se lo asignamos a la entidad (el Dominio valida que no supere los 20 caracteres)
        ramaEntity.CodigoAcceso = codigoGenerado;

        // 4. Guardamos la actualización en la BD
        await _repositorioActualizar.ActualizarAsync(ramaEntity);

        // Devolvemos el código para que el controlador lo responda
        return codigoGenerado;
    }
}