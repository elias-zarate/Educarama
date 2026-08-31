namespace Dominio;

public class RamaEntity
{
    public int Id { get; set; }

    public required string Nombre
    {
        get => field;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre de la rama es obligatorio.", nameof(value));
            }

            if (value.Length > 100)
            {
                throw new ArgumentException("El nombre de la rama no puede superar los 100 caracteres.", nameof(value));
            }

            field = value;
        }
    }
    public string? CodigoAcceso
    {
        get => field;
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 20)
            {
                throw new ArgumentException("El código de acceso no puede superar los 20 caracteres.", nameof(value));
            }
            field = value;
        }
    }

    public int IdGrupo { get; set; }
    public int IdCategoriaRama{ get; set; }
}