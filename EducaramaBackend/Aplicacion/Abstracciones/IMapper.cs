namespace Aplicacion.Abstracciones;

public interface IMapper<TIn, TOut>
{
    TOut Map(TIn input);
}