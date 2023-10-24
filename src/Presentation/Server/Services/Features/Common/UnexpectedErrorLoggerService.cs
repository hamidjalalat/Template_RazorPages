namespace Services.Features.Common;

public class UnexpectedErrorLoggerService : object
{
	#region Static Fields

	private static readonly object lockedObject = new();

	#endregion /Static Fields

	#region Static Methods

	public static string GetErrorMessages(System.Exception exception)
	{
		var stringBuilder =
			new System.Text.StringBuilder();

		stringBuilder.Append
			(value: "<b>Exception:</b><br />");

		var currentException = exception;

		while (currentException is not null)
		{
			stringBuilder.Append
				(value: currentException.Message);

			stringBuilder.Append(value: "<br />");

			currentException =
				currentException.InnerException;

			if (currentException is not null)
			{
				stringBuilder.Append
					(value: "<b>Inner Exception:</b><br />");
			}
		}

		var result =
			stringBuilder.ToString();

		return result;
	}

	#endregion /Static Methods

	#region Constructor
	public UnexpectedErrorLoggerService
		(HttpContextService httpContextService,
		EmailTemplateService emailTemplateService,

		Microsoft.Extensions.Hosting.IHostEnvironment hostEnvironment,
		Microsoft.Extensions.Options.IOptionsSnapshot<Infrastructure.Settings.ApplicationSettings> snapshotOptionsAccessor) : base()
	{
		HostEnvironment =
			hostEnvironment;

		HttpContextService =
			httpContextService;

		EmailTemplateService =
			emailTemplateService;

		ApplicationSettings =
			snapshotOptionsAccessor.Value;
	}
	#endregion /Constructor

	#region Properties

	private HttpContextService HttpContextService { get; init; }
	private EmailTemplateService EmailTemplateService { get; init; }

	private Microsoft.Extensions.Hosting.IHostEnvironment HostEnvironment { get; init; }
	private Infrastructure.Settings.ApplicationSettings ApplicationSettings { get; init; }

	#endregion /Properties

	#region Methods

	public async System.Threading.Tasks.Task LogAsync(System.Exception exception)
	{
		var errorMessages =
			GetErrorMessages(exception: exception);

		var now =
			Dtat.DateTime.Now;

		var siteUrl =
			HttpContextService.GetCurrentHostUrl();

		var userIP =
			HttpContextService.GetRemoteIpAddress();

		// **************************************************
		var body =
			await
			EmailTemplateService
			.GetContentForUnexpectedErrorAsync();

		if (string.IsNullOrWhiteSpace(value: body))
		{
			body =
				$"<b>Error Messages:</b><br />{errorMessages}" +
				$"<hr />" +
				$"<br /><b>Timestamp:</b>{now:yyyy/MM/dd HH:mm:ss}" +
				$"<br /><b>User IP:</b>{userIP}" +
				$"<br /><b>Site Url:</b>{siteUrl}"
				;
		}
		else
		{
			body = body
				.Replace(oldValue: "{{USER_IP}}", newValue: userIP)
				.Replace(oldValue: "{{SITE_URL}}", newValue: siteUrl)
				.Replace(oldValue: "{{UNEXPECTED_ERROR_MESSAGES}}", newValue: errorMessages)
				.Replace(oldValue: "{{TIMESTAMP}}", newValue: now.ToString(format: "yyyy/MM/dd HH:mm:ss"))
				;
		}
		// **************************************************

		if (ApplicationSettings.UnexpectedErrorLoggerSetting.SendErrorByEmail)
		{
			var subject =
				"Unexpected Error!";

			await Dtat.Net.Mail.Utility
				.SendAsync(subject: subject, body: body,
				mailSetting: ApplicationSettings.UnexpectedErrorLoggerSetting.MailSetting);
		}

		lock (lockedObject)
		{
			if (ApplicationSettings.UnexpectedErrorLoggerSetting.LogErrorToFile)
			{
				var path = ApplicationSettings
					.UnexpectedErrorLoggerSetting.LogPath;

				if (string.IsNullOrWhiteSpace(value: path))
				{
					path = "/logs";
				}

				path = path.Replace
					(oldChar: '/', newChar: '\\');

				var physicalPath =
					$"{HostEnvironment.ContentRootPath}{path}";

				if (System.IO.Path.Exists(path: physicalPath) == false)
				{
					System.IO.Directory.CreateDirectory(path: physicalPath);
				}

				// **************************************************
				var dayString = now.Day.ToString()
					.PadLeft(totalWidth: 2, paddingChar: '0');

				var monthString = now.Month.ToString()
					.PadLeft(totalWidth: 2, paddingChar: '0');
				// **************************************************

				var fileName =
					$"log_{now.Year}_{monthString}_{dayString}.html";

				var physicalPathName =
					$"{physicalPath}\\{fileName}";

				System.IO.File.AppendAllText(path: physicalPathName,
					contents: body, encoding: System.Text.Encoding.UTF8);
			}
		}
	}

	#endregion /Methods
}
