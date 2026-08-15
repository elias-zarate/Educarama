namespace Aplicacion.Abstracciones.CasosDeUso;

public interface IActualizarUseCase<TDTO>
{
    Task ActualizarAsync(TDTO dto, int id);
}