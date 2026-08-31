using Dominio;
using System.Threading.Tasks;

namespace Aplicacion.Abstracciones.Repositorios;

public interface ILeerAsignacionPorUsuarioRepositorio
{
    Task<AsignacionRolEntity?> ObtenerPorUsuarioAsync(int idUsuario);
}