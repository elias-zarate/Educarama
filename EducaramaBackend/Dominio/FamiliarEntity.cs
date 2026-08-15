namespace Dominio;

public class FamiliarEntity
{
	public int Id { get; set; }
	public int? TelefonoContacto { get; set; }

	public required string EmailContacto
	{
		get => field;
		set
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException("El email de contacto es obligatorio.", nameof(value));
			}

			if (value.Length > 200)
			{
				throw new ArgumentException("El email no puede superar los 200 caracteres.", nameof(value));
			}

			field = value;
		}
	}

	public int? IdPersona { get; set; }
}