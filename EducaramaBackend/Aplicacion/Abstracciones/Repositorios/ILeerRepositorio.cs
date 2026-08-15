namespace Aplicacion.Abstracciones.Repositorios;

public interface ILeerRepositorio<TEntity> where TEntity : class
{
    Task<TEntity?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<TEntity>> ObtenerTodosAsync();
}