namespace Dominio;

public class SenderoEntity
{
    public int Id { get; set; }

    public required string Tipo
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El tipo de sendero es obligatorio.", nameof(value));
            }

            if (value.Length > 100)
            {
                throw new ArgumentException("El tipo de sendero no puede superar los 100 caracteres.", nameof(value));
            }

            field = value;
        }
    }
}