namespace Aplicacion.Abstracciones.Repositorios;

public interface IActualizarRepositorio<TEntity> where TEntity : class
{
    Task ActualizarAsync(TEntity entidad);
}