namespace Dominio;

public class GrupoEntity
{
    public int Id { get; set; }

    public required string Codigo
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El código del grupo es obligatorio.", nameof(value));
            }

            if (value.Length > 50)
            {
                throw new ArgumentException("El código del grupo no puede superar los 50 caracteres.", nameof(value));
            }

            field = value;
        }
    }

    public required string Nombre
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre del grupo es obligatorio.", nameof(value));
            }

            if (value.Length > 100)
            {
                throw new ArgumentException("El nombre del grupo no puede superar los 100 caracteres.", nameof(value));
            }

            field = value;
        }
    }
}