namespace Dominio;

public class CategoriaRamaEntity
{
    public int Id { get; set; }

    public required string Nombre
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre de la categoría es obligatorio.", nameof(value));
            }

            if (value.Length > 100)
            {
                throw new ArgumentException("El nombre de la categoría no puede superar los 100 caracteres.", nameof(value));
            }

            field = value;
        }
    }
}