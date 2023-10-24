namespace Dtat.Net.Mail;

public class MailSetting : Abstractions.IMailSetting
{
	public MailSetting() : base()
	{
		Enabled = true;
	}

	public bool Enabled { get; init; }



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
}
