namespace Aplicacion.Abstracciones.CasosDeUso;

public interface ICrearUseCase<TDTO>
{
    Task AgregarAsync(TDTO dto);
}
