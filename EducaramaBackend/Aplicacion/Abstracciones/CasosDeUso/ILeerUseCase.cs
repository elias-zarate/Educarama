using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Abstracciones.CasosDeUso;

public interface ILeerUseCase<TDTO>
{
    Task<TDTO?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<TDTO>> ObtenerTodosAsync();
}
