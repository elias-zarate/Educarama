namespace Dominio;

public class UsuarioEntity
{
    public int Id { get; set; }

    public required string Nombre
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre de usuario es obligatorio.", nameof(value));
            }

            if (value.Length > 100)
            {
                throw new ArgumentException("El nombre no puede superar los 100 caracteres.", nameof(value));
            }

            field = value;
        }
    }

    public required string Email
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El email es obligatorio.", nameof(value));
            }

            if (value.Length > 100)
            {
                throw new ArgumentException("El email no puede superar los 100 caracteres.", nameof(value));
            }

            field = value;
        }
    }

    public required string Contrasenia
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("La contraseña es obligatoria.", nameof(value));
            }

            if (value.Length > 100)
            {
                throw new ArgumentException("La contraseña no puede superar los 100 caracteres.", nameof(value));
            }

            field = value;
        }
    }

    public virtual ICollection<AsignacionRolEntity>? AsignacionesRoles { get; set; }
}