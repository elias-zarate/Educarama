namespace Dominio;

public class InsigniaEntity
{
    public int Id { get; set; }

    public required string Nombre
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre de la insignia es obligatorio.", nameof(value));
            }

            if (value.Length > 50)
            {
                throw new ArgumentException("El nombre no puede superar los 50 caracteres.", nameof(value));
            }

            field = value;
        }
    }

    public int IdCategoriaRama { get; set; }
}