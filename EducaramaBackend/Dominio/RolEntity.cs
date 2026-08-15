namespace Dominio;

public class RolEntity
{
    public int Id { get; set; }

    public required string Nombre
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre del rol es obligatorio.", nameof(value));
            }

            if (value.Length > 50)
            {
                throw new ArgumentException("El nombre del rol no puede superar los 50 caracteres.", nameof(value));
            }

            field = value;
        }
    }
}