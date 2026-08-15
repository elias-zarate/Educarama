namespace Dominio;

public class CompetenciaEntity
{
    public int Id { get; set; }
    public int Codigo { get; set; }

    public required string Descripcion
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("La descripción de la competencia es obligatoria.", nameof(value));
            }

            if (value.Length > 300)
            {
                throw new ArgumentException("La descripción no puede superar los 300 caracteres.", nameof(value));
            }

            field = value;
        }
    }

    public int IdCategoriaRama { get; set; }
    public int IdSendero { get; set; }
}