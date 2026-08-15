namespace Dominio;

public class EsFamiliaEntity
{
	public int IdProtagonista { get; set; }
	public int IdFamiliar { get; set; }

	public required string Vinculo
	{
		get => field;
		set
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException("El vínculo familiar es obligatorio.", nameof(value));
			}

			if (value.Length > 100)
			{
				throw new ArgumentException("El vínculo no puede superar los 100 caracteres.", nameof(value));
			}

			field = value;
		}
	}

	public bool? EsContactoDeEmergencia { get; set; }
}