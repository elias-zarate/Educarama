using System.Threading.Tasks;
using Aplicacion.Abstracciones.Repositorios;
using Aplicacion.Usuarios.DTOs;
using Aplicacion.Usuarios.Mappers;
using Dominio;

namespace Aplicacion.Usuarios.CasosDeUso;

public class ObtenerPerfilPorEmailUseCase
{
    private readonly ILeerUsuarioPorEmailRepositorio _usuarioRepositorio;
    private readonly UsuarioEntityToPerfilDtoMapper _mapper;

    public ObtenerPerfilPorEmailUseCase(
        ILeerUsuarioPorEmailRepositorio usuarioRepositorio,
        UsuarioEntityToPerfilDtoMapper mapper)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _mapper = mapper;
    }

    public async Task<PerfilUsuarioDTO> ObtenerPerfilAsync(string email)
    {
        var usuarioEntity = await _usuarioRepositorio.ObtenerPorEmailAsync(email);

        if (usuarioEntity == null)
            throw new KeyNotFoundException("Usuario no encontrado.");

        // Acá usa el mapper que ya tenías armado para transformar la entidad al DTO de perfil
        return _mapper.Map(usuarioEntity);
    }
}