namespace Dominio;

public class PatrullaEntity
{
    public int Id { get; set; }

    public required string Nombre
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre de la patrulla es obligatorio.", nameof(value));
            }

            if (value.Length > 100)
            {
                throw new ArgumentException("El nombre de la patrulla no puede superar los 100 caracteres.", nameof(value));
            }

            field = value;
        }
    }

    public int IdRama { get; set; }
}