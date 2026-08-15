using System;

namespace Aplicacion.Usuarios.DTOs;

public class RegistroUsuarioDTO
{
    // Datos del Usuario
    public required string Nombre { get; set; }
    public required string Email { get; set; }
    public required string Contrasenia { get; set; } // Más adelante la encriptaremos

    // Datos de la Asignación de Rol
    public int IdRol { get; set; }
    public int? IdGrupo { get; set; }
    public int? IdRama { get; set; }
}