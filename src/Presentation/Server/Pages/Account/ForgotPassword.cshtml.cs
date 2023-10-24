using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

public class ForgotPasswordModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public ForgotPasswordModel
		(Persistence.DatabaseContext databaseContext,

		Services.Features.Identity.UserNotificationService userNotificationService,
		Services.Features.Common.ApplicationSettingService applicationSettingService,

		DNTCaptcha.Core.IDNTCaptchaValidatorService captchaValidatorService,
		Microsoft.Extensions.Options.IOptions<DNTCaptcha.Core.DNTCaptchaOptions> captchaOptions) : base(databaseContext: databaseContext)
	{
		ViewModel = new();

		CaptchaOptions = captchaOptions.Value;
		CaptchaValidatorService = captchaValidatorService;

		UserNotificationService = userNotificationService;
		ApplicationSettingService = applicationSettingService;
	}
	#endregion /Constructor

	#region Properties

	private DNTCaptcha.Core.DNTCaptchaOptions CaptchaOptions { get; init; }
	private DNTCaptcha.Core.IDNTCaptchaValidatorService CaptchaValidatorService { get; init; }
	private Services.Features.Identity.UserNotificationService UserNotificationService { get; init; }
	public Services.Features.Common.ApplicationSettingService ApplicationSettingService { get; init; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.SendAgainUserEmailAddressVerificationKeyViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		// **************************************************
		var applicationSetting =
			await
			ApplicationSettingService.GetInstanceAsync();

		if (applicationSetting.IsCaptchaImageEnabled)
		{
			if (CaptchaValidatorService.HasRequestValidCaptchaEntry() == false)
			{
				ModelState.AddModelError(CaptchaOptions.CaptchaComponent
					.CaptchaInputName, Resources.Messages.Errors.CaptchaImageIsNotCorrect);
			}
		}
		// **************************************************

		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var usernameOrEmailAddress =
			ViewModel.UsernameOrEmailAddress.Fix()!.ToLower();

		var foundedUser =
			await
			DatabaseContext.Users
			.Where(current => current.Username != null
				&& current.Username.ToLower() == usernameOrEmailAddress)
			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			foundedUser =
				await
				DatabaseContext.Users
				.Where(current => current.EmailAddress.ToLower() == usernameOrEmailAddress)
				.FirstOrDefaultAsync();

			if (foundedUser is null)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors
					.ThereIsNotAnyUserWithThisUsernameOrEmailAddress);

				AddPageError(message: errorMessage);

				return Page();
			}
		}
		// **************************************************

		// **************************************************
		foundedUser.ResetVerificationKey();

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		try
		{
			await UserNotificationService
				.SendEmailForResettingPasswordAsync(user: foundedUser);
		}
		catch { }
		// **************************************************

		// **************************************************
		var successMessage =
			Resources.Messages.Successes.CheckYourMailbox;

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage
			(pageName: Constants.CommonRouting.Login);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
