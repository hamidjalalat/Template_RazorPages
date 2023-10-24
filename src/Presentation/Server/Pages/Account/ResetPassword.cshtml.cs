using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Services.Features.Common;

namespace Server.Pages.Account;

public class ResetPasswordModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public ResetPasswordModel
		(Persistence.DatabaseContext databaseContext,

		Services.Features.Common.ApplicationSettingService applicationSettingService,

		DNTCaptcha.Core.IDNTCaptchaValidatorService captchaValidatorService,
		Microsoft.Extensions.Options.IOptions<DNTCaptcha.Core.DNTCaptchaOptions> captchaOptions) : base(databaseContext: databaseContext)
	{
		ViewModel = new();

		CaptchaOptions = captchaOptions.Value;
		CaptchaValidatorService = captchaValidatorService;

		ApplicationSettingService = applicationSettingService;
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.ResetPasswordViewModel ViewModel { get; set; }

	private DNTCaptcha.Core.DNTCaptchaOptions CaptchaOptions { get; init; }
	private DNTCaptcha.Core.IDNTCaptchaValidatorService CaptchaValidatorService { get; init; }

	private Services.Features.Common.ApplicationSettingService ApplicationSettingService { get; init; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public Microsoft.AspNetCore.Mvc.IActionResult OnGetAsync(string? key = null)
	{
		// **************************************************
		key = key.Fix();

		if (key is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		key = key.Replace
			(oldValue: " ", newValue: string.Empty);
		// **************************************************

		// **************************************************
		try
		{
			ViewModel.Key =
				new System.Guid(g: key);
		}
		catch
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}
		// **************************************************

		return Page();
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
		var foundedUser =
			await
			DatabaseContext.Users
			.Where(current => current.EmailAddressVerificationKey == ViewModel.Key)
			.FirstOrDefaultAsync();

		if (foundedUser == null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}
		// **************************************************

		// **************************************************
		var newPassword =
			ViewModel.NewPassword.Fix()!;

		foundedUser.ResetVerificationKey();
		foundedUser.SetPassword(password: newPassword);

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var message = Resources
			.Messages.Successes.PasswordChanged;

		AddToastSuccess(message: message);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.Login);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
