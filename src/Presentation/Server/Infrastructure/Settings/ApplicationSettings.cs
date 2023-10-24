namespace Infrastructure.Settings;

public class ApplicationSettings : object
{
	#region Static Fields

	public static readonly string KeyName = nameof(ApplicationSettings);

	#endregion /Static Fields

	#region Constructor
	public ApplicationSettings() : base()
	{
		DatabaseSetting = new();
		GoogleAuthenticationSetting = new();
		UnexpectedErrorLoggerSetting = new();
	}
	#endregion /Constructor

	#region Properties

	public bool SiteHasSsl { get; set; }
	public string[]? ActivationKeys { get; set; }
	public string? CaptchaImageEncryptionKey { get; set; }


	public DatabaseSetting DatabaseSetting { get; init; }
	public GoogleAuthenticationSetting GoogleAuthenticationSetting { get; init; }
	public UnexpectedErrorLoggerSetting UnexpectedErrorLoggerSetting { get; init; }

	#endregion /Properties
}
