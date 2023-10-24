namespace Server.Pages.Features.Common.Admin.LocalizedMailSettings;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Supervisor)]
public class TestModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public TestModel
		(Persistence.DatabaseContext databaseContext,
		Services.Features.Common.HttpContextService httpContextService,
		Services.Features.Common.LocalizedMailSettingService localizedMailSettingService) : base(databaseContext: databaseContext)
	{
		ViewModel = new();

		HttpContextService = httpContextService;
		LocalizedMailSettingService = localizedMailSettingService;
	}
	#endregion /Constructor

	#region Properties

	private Services.Features.Common.HttpContextService HttpContextService { get; init; }
	private Services.Features.Common.LocalizedMailSettingService LocalizedMailSettingService { get; init; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public System.Exception? Exception { get; set; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Common.Admin.LocalizedMailSettings.TestViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		ViewModel.EmailBody = "<b>Email Body Test</b>";
		ViewModel.EmailSubject = "Email Subject Test";
		ViewModel.RecipientEmailAddress = "DariushT@GMail.com";
		ViewModel.RecipientDisplayName = "Mr. Dariush Tasdighi";

		await System.Threading.Tasks.Task.CompletedTask;
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
	<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		try
		{
			var localizedMailSetting =
				await
				LocalizedMailSettingService.GetInstanceAsync();

			// **************************************************
			var hostUrl =
				HttpContextService.GetCurrentHostUrl();

			var body =
				$"{ViewModel.EmailBody}<hr /><p>Site URL: <a href='{hostUrl}'>{hostUrl}</a> - Version: {Infrastructure.Version.Value}</p>";

			var recipient =
				new System.Net.Mail.MailAddress
				(address: ViewModel.RecipientEmailAddress!,
				displayName: ViewModel.RecipientDisplayName);

			await Dtat.Net.Mail.Utility.SendAsync
				(recipient: recipient, subject: ViewModel.EmailSubject!,
				body: body, mailSetting: localizedMailSetting);
			// **************************************************

			// **************************************************
			var successMessage = string.Format
				(format: Resources.Messages.Successes.EmailSentSuccessfully);

			AddPageSuccess(message: successMessage);
			// **************************************************
		}
		catch (System.Exception ex)
		{
			var exception = ex;

			while (exception != null)
			{
				AddPageError
					(message: exception.Message);

				exception =
					exception.InnerException;
			}
		}

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
