namespace Dominio;

public class PersonaEntity
{
    public int? Id { get; set; }

    public int Dni
    {
        get => field;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("El DNI debe ser un valor válido mayor a cero.", nameof(value));
            }
            field = value;
        }
    }

    public string? Nombre
    {
        get => field;
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 100)
            {
                throw new ArgumentException("El nombre no puede superar los 100 caracteres.", nameof(value));
            }
            field = value;
        }
    }

    public string? Apellido
    {
        get => field;
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 100)
            {
                throw new ArgumentException("El apellido no puede superar los 100 caracteres.", nameof(value));
            }
            field = value;
        }
    }

    public DateOnly FechaNacimiento
    {
        get => field;
        set
        {
            if (value > DateOnly.FromDateTime(DateTime.Today))
            {
                throw new ArgumentException("La fecha de nacimiento no puede ser una fecha futura.", nameof(value));
            }
            field = value;
        }
    }
}