namespace Aplicacion.Abstracciones.Repositorios;

public interface ICrearRepositorio<TEntity> where TEntity : class
{
    Task AgregarAsync(TEntity entidad);
}