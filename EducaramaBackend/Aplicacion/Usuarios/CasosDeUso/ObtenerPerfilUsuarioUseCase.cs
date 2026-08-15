using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Abstracciones;
using Aplicacion.Abstracciones.CasosDeUso;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Usuarios.DTOs;
using Dominio;

namespace Aplicacion.Usuarios.CasosDeUso;

public class ObtenerPerfilUsuarioUseCase : ILeerUseCase<PerfilUsuarioDTO>
{
    private readonly ILeerRepositorio<UsuarioEntity> _repositorio;
    private readonly IMapper<UsuarioEntity, PerfilUsuarioDTO> _mapper;
    
    public ObtenerPerfilUsuarioUseCase(
        ILeerRepositorio<UsuarioEntity> repositorio,
        IMapper<UsuarioEntity, PerfilUsuarioDTO> mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<PerfilUsuarioDTO?> ObtenerPorIdAsync(int id)
    {
        var entidad = await _repositorio.ObtenerPorIdAsync(id);
        return entidad != null ? _mapper.Map(entidad) : null;
    }

    public async Task<IEnumerable<PerfilUsuarioDTO>> ObtenerTodosAsync()
    {
        var entidades = await _repositorio.ObtenerTodosAsync();
        return entidades.Select(e => _mapper.Map(e));
    }
}