namespace Domain.Features.Common;

public class LocalizedMailSetting :
	Seedwork.LocalizedEntity,
	Dtat.Net.Mail.Abstractions.IMailSetting,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public LocalizedMailSetting
		(System.Guid cultureId) : base(cultureId: cultureId)
	{
		SmtpClientPortNumber = 25;
		SmtpClientTimeout = 100_000;
	}
	#endregion /Constructor

	#region Properties

	public bool Enabled { get; set; }



	public int SmtpClientTimeout { get; set; }

	public int SmtpClientPortNumber { get; set; }

	public bool SmtpClientSslEnabled { get; set; }

	public string? SmtpClientHostAddress { get; set; }



	public string? SmtpUsername { get; set; }

	public string? SmtpPassword { get; set; }

	public bool UseDefaultCredentials { get; set; }



	public string? SenderDisplayName { get; set; }

	public string? SenderEmailAddress { get; set; }



	public string? SupportDisplayName { get; set; }

	public string? SupportEmailAddress { get; set; }



	public string? BccAddresses { get; set; }

	public string? EmailSubjectTemplate { get; set; }

	#endregion /Properties
}
