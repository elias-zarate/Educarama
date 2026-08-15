namespace Dominio;

public class AvisoEntity
{
    public int Id { get; set; }

    public required string Titulo
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El título es obligatorio.", nameof(value));
            }
            if (value.Length > 200)
            {
                throw new ArgumentException("El título no puede superar los 200 caracteres.", nameof(value));
            }
            field = value;
        }
    }

    public required string Descripcion
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("La descripción es obligatoria.", nameof(value));
            }
            field = value;
        }
    }

    public DateOnly FechaInicio
    {
        get => field;
        set => field = value;
    }

    public DateOnly? FechaFin
    {
        get => field;
        set
        {
            if (value.HasValue && value.Value < FechaInicio)
            {
                throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.", nameof(value));
            }
            field = value;
        }
    }

    public required string Prioridad
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("La prioridad es obligatoria.", nameof(value));
            }
            field = value;
        }
    }

    public bool Activo
    {
        get => field;
        set => field = value;
    }
}