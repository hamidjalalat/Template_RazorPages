using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Server.Pages.Account;

public class RegisterModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public RegisterModel
		(Persistence.DatabaseContext databaseContext,

		Services.Features.Identity.UserNotificationService userNotificationService,

		Services.Features.Common.HttpContextService httpContextService,
		Services.Features.Common.ApplicationSettingService applicationSettingService,

		DNTCaptcha.Core.IDNTCaptchaValidatorService captchaValidatorService,
		Microsoft.Extensions.Options.IOptions<DNTCaptcha.Core.DNTCaptchaOptions> captchaOptions) : base(databaseContext: databaseContext)
	{
		ViewModel = new();

		CaptchaOptions = captchaOptions.Value;
		CaptchaValidatorService = captchaValidatorService;

		HttpContextService = httpContextService;
		UserNotificationService = userNotificationService;

		ApplicationSettingService = applicationSettingService;
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.RegisterViewModel ViewModel { get; set; }

	private DNTCaptcha.Core.DNTCaptchaOptions CaptchaOptions { get; init; }
	private DNTCaptcha.Core.IDNTCaptchaValidatorService CaptchaValidatorService { get; init; }

	private Services.Features.Identity.UserNotificationService UserNotificationService { get; init; }

	private Services.Features.Common.HttpContextService HttpContextService { get; init; }
	private Services.Features.Common.ApplicationSettingService ApplicationSettingService { get; init; }

	#endregion /Properties

	#region Methods

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		// **************************************************
		var applicationSetting =
			await
			ApplicationSettingService.GetInstanceAsync();

		if (applicationSetting.IsRegistrationEnabled == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var currentCulture =
			await
			DatabaseContext.Cultures

			.Where(current => current.Lcid == currentUICultureLcid)

			.FirstOrDefaultAsync();

		if (currentCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (currentCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		return Page();
	}

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		// **************************************************
		var RemoteIP =
			HttpContextService.GetRemoteIpAddress();

		if (string.IsNullOrWhiteSpace(value: RemoteIP))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}
		// **************************************************

		// **************************************************
		var applicationSetting =
			await
			ApplicationSettingService.GetInstanceAsync();

		if (applicationSetting.IsRegistrationEnabled == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var currentCulture =
			await
			DatabaseContext.Cultures

			.Where(current => current.Lcid == currentUICultureLcid)

			.FirstOrDefaultAsync();

		if (currentCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (currentCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
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
		var username =
			ViewModel.Username.Fix()!.ToLower();

		var isUsernameFound =
			await
			DatabaseContext.Users
			.Where(current => current.Username != null
				&& current.Username.ToLower() == username)
			.AnyAsync()
			;

		if (isUsernameFound)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.Username);

			AddPageError(message: errorMessage);
		}
		// **************************************************

		// **************************************************
		var emailAddress =
			ViewModel.EmailAddress.Fix()!.ToLower();

		var isEmailAddressFound =
			await
			DatabaseContext.Users
			.Where(current => current.EmailAddress.ToLower() == emailAddress)
			.AnyAsync();

		if (isEmailAddressFound)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.EmailAddress);

			AddPageError(message: errorMessage);
		}
		// **************************************************

		if (isUsernameFound || isEmailAddressFound)
		{
			return Page();
		}

		// **************************************************
		var password =
			ViewModel.Password.Fix()!;

		var user =
			new Domain.Features.Identity.User
			(emailAddress: emailAddress, registerIP: RemoteIP,
			registerType: Domain.Features.Identity.Enums.AuthenticationTypeEnum.Internal)
			{
				Username = username,

				IsActive = applicationSetting
					.ActivateUserAfterRegistration,

				RoleId =
					Constants.BaseTableItem.Role.SimpleUser,
			};

		user.SetPassword(password: password);

		var entityEntry =
			await
			DatabaseContext.AddAsync(entity: user);

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();
		// **************************************************

		try
		{
			await UserNotificationService
				.SendEmailVerificationKeyAsync(user: user);

			await UserNotificationService
				.NotifyAllActiveManagersAfterUserRegistrationAsync(newUser: user);
		}
		catch { }

		AddToastSuccess(message: Resources
			.Messages.Successes.RegistrationDone);

		// **************************************************
		// TODO: Send Verification Key To User Email Address
		// **************************************************

		return RedirectToPage
			(pageName: Constants.CommonRouting.Login);
	}

	#endregion /Methods
}
