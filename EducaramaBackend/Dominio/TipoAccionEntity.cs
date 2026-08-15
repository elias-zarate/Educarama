namespace Dominio;

public class TipoAccionEntity
{
    public int Id { get; set; }

    public required string Tipo
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El tipo de acción es obligatorio.", nameof(value));
            }

            if (value.Length > 25)
            {
                throw new ArgumentException("El tipo de acción no puede superar los 25 caracteres.", nameof(value));
            }

            field = value;
        }
    }
}