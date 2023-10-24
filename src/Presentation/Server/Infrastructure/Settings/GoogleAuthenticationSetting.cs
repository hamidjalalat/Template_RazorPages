namespace Infrastructure.Settings;

public class GoogleAuthenticationSetting : object
{
	public GoogleAuthenticationSetting() : base()
	{
	}

	public string? ClientId { get; set; }

	public string? ClientSecret { get; set; }
}
