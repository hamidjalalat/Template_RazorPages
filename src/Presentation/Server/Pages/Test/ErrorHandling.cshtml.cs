namespace Server.Pages.Test;

public class ErrorHandlingModel : Infrastructure.BasePageModel
{
	#region Constructor
	public ErrorHandlingModel
		(Services.Features.Common.UnexpectedErrorLoggerService unexpectedErrorLoggerService,
		Microsoft.Extensions.Options.IOptionsSnapshot<Infrastructure.Settings.ApplicationSettings> snapshotOptionsAccessor) : base()
	{
		UnexpectedErrorLoggerService =
			unexpectedErrorLoggerService;

		ApplicationSettings =
			snapshotOptionsAccessor.Value;
	}
	#endregion /Constructor

	#region Properties

	private Infrastructure.Settings.ApplicationSettings ApplicationSettings { get; init; }
	private Services.Features.Common.UnexpectedErrorLoggerService UnexpectedErrorLoggerService { get; init; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(string? secretKey)
	{
		if (string.IsNullOrWhiteSpace(value: secretKey))
		{
			return RedirectToPage
				(pageName: Constants.CommonRouting.BadRequest);
		}

		if (string.IsNullOrWhiteSpace
			(value: ApplicationSettings.UnexpectedErrorLoggerSetting.SecretKey))
		{
			return RedirectToPage
				(pageName: Constants.CommonRouting.BadRequest);
		}

		if (string.Compare(strA: secretKey, strB:
			ApplicationSettings.UnexpectedErrorLoggerSetting.SecretKey, ignoreCase: false) != 0)
		{
			return RedirectToPage
				(pageName: Constants.CommonRouting.BadRequest);
		}

		var innerException =
			new System.Exception(message: "Some Inner Exception!");

		var exceptoin = new System.Exception
			(message: "Some Exception!", innerException: innerException);

		//throw exceptoin;

		await UnexpectedErrorLoggerService.LogAsync(exception: exceptoin);

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
