using System.Threading.Tasks;

namespace Aplicacion.Abstracciones.Repositorios;

public interface IAsignarGrupoUsuarioRepositorio
{
    Task AsignarRolYGrupoAsync(int idUsuario, int idGrupo, int idRol);
}