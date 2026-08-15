namespace Aplicacion.Abstracciones.CasosDeUso;

public interface IEliminarUseCase
{
    Task EliminarAsync(int id);
}