using System;

namespace Aplicacion.Usuarios.DTOs;

public class PerfilUsuarioDTO
{
    // Claves Primarias
    public int IdUsuario { get; set; }
    public int IdAsignacionRol { get; set; }

    // Datos del Usuario
    public required string Nombre { get; set; }
    public required string Email { get; set; }
    public required string Contrasenia { get; set; }

    // Datos del Rol
    public int IdRol { get; set; }
    public int? IdGrupo { get; set; }
    public int? IdRama { get; set; }
}