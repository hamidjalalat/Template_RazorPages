namespace Infrastructure.Settings;

public class UnexpectedErrorLoggerSetting : object
{
	#region Constructor
	public UnexpectedErrorLoggerSetting() : base()
	{
		MailSetting = new();
	}
	#endregion /Constructor

	#region Properties

	public string? LogPath { get; set; }
	public string? SecretKey { get; set; }
	public bool LogErrorToFile { get; set; }
	public bool SendErrorByEmail { get; set; }

	public Dtat.Net.Mail.MailSetting MailSetting { get; init; }

	#endregion /Properties
}
