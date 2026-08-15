namespace Dominio;

public class DocumentoEntity
{
    public int Id { get; set; }

    public required string TipoDocumento
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El tipo de documento es obligatorio.", nameof(value));
            }

            if (value.Length > 25)
            {
                throw new ArgumentException("El tipo de documento no puede superar los 25 caracteres.", nameof(value));
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
                throw new ArgumentException("La descripción del documento es obligatoria.", nameof(value));
            }

            field = value;
        }
    }

    public int IdProtagonista { get; set; }
}