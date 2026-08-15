namespace Dominio;

public class AccionEntity
{
    public int Id { get; set; }

    public required string Descripcion
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("La descripción de la acción es obligatoria.", nameof(value));
            }

            field = value;
        }
    }

    public int IdCompetencia { get; set; }
    public int CodigoCompetencia { get; set; }
    public int IdTipoAccion { get; set; }
}