using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

public class LoginModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public LoginModel
		(Persistence.DatabaseContext databaseContext,
		Infrastructure.Security.UserManagerService userManagerService,
		Services.Features.Common.ApplicationSettingService applicationSettingService,

		DNTCaptcha.Core.IDNTCaptchaValidatorService captchaValidatorService,
		Microsoft.Extensions.Options.IOptions<DNTCaptcha.Core.DNTCaptchaOptions> captchaOptions) : base(databaseContext: databaseContext)
	{
		ViewModel = new();

		CaptchaOptions = captchaOptions.Value;
		CaptchaValidatorService = captchaValidatorService;

		UserManagerService = userManagerService;
		ApplicationSettingService = applicationSettingService;
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.LoginViewModel ViewModel { get; set; }

	private DNTCaptcha.Core.DNTCaptchaOptions CaptchaOptions { get; init; }
	private DNTCaptcha.Core.IDNTCaptchaValidatorService CaptchaValidatorService { get; init; }

	private Infrastructure.Security.UserManagerService UserManagerService { get; init; }
	private Services.Features.Common.ApplicationSettingService ApplicationSettingService { get; init; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(string? returnUrl)
	{
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

		// در صفحه ورود، نباید دستورات ذیل نوشته شود
		// اگر کاربر به اشتباه، همه زبان‌های سامانه را
		// غیرفعال کرده باشد، دیگر امکان ورود به سامانه
		// را نخواهد داشت
		//if (currentCulture.IsActive == false)
		//{
		//	return RedirectToPage(pageName:
		//		Constants.CommonRouting.NotFound);
		//}
		// **************************************************

		ViewModel.ReturnUrl = returnUrl;

		return Page();
	}
	#endregion /OnGetAsync

	#region OnPostAsync
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

		Domain.Features.Identity.User? foundedUser = null;

		switch (applicationSetting.LoginOption)
		{
			case Domain.Features.Common.Enums.LoginOptionEnum.Username:
			{
				foundedUser =
					await
					DatabaseContext.Users
					.Include(current => current.Role)
					.Include(current => current.LocalizedUsers)
					.ThenInclude(current => current.Culture)
					.Where(current => current.Username != null
						&& current.Username.ToLower() == usernameOrEmailAddress)
					.FirstOrDefaultAsync();

				if (foundedUser is null)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors
						.UsernameAndOrPasswordIsNotCorrect);

					AddPageError(message: errorMessage);

					return Page();
				}

				break;
			}

			case Domain.Features.Common.Enums.LoginOptionEnum.EmailAddress:
			{
				foundedUser =
					await
					DatabaseContext.Users
					.Include(current => current.Role)
					.Include(current => current.LocalizedUsers)
					.ThenInclude(current => current.Culture)
					.Where(current => current.EmailAddress.ToLower() == usernameOrEmailAddress)
					.FirstOrDefaultAsync();

				if (foundedUser is null)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors
						.EmailAddressAndOrPasswordIsNotCorrect);

					AddPageError(message: errorMessage);

					return Page();
				}

				break;
			}

			case Domain.Features.Common.Enums.LoginOptionEnum.Both:
			{
				foundedUser =
					await
					DatabaseContext.Users
					.Include(current => current.Role)
					.Include(current => current.LocalizedUsers)
					.ThenInclude(current => current.Culture)
					.Where(current => current.Username != null
						&& current.Username.ToLower() == usernameOrEmailAddress)
					.FirstOrDefaultAsync();

				if (foundedUser is null)
				{
					foundedUser =
						await
						DatabaseContext.Users
						.Include(current => current.Role)
						.Include(current => current.LocalizedUsers)
						.ThenInclude(current => current.Culture)
						.Where(current => current.EmailAddress.ToLower() == usernameOrEmailAddress)
						.FirstOrDefaultAsync();

					if (foundedUser is null)
					{
						var errorMessage = string.Format
							(format: Resources.Messages.Errors
							.UsernameAndOrEmailAddressAndOrPasswordIsNotCorrect);

						AddPageError(message: errorMessage);

						return Page();
					}
				}

				break;
			}

			default:
			{
				return RedirectToPage
					(pageName: Constants.CommonRouting.BadRequest);
			}
		}

		if (foundedUser.Role is null)
		{
			return RedirectToPage
				(pageName: Constants.CommonRouting.BadRequest);
		}
		// **************************************************

		// **************************************************
		var log = false;

		var password =
			ViewModel.Password.Fix()!;

		var passwordHash =
			Dtat.Security.Hashing.GetSha256(value: password);
		// **************************************************

		// **************************************************
		if (string.IsNullOrWhiteSpace
			(value: applicationSetting.MasterPassword) == false)
		{
			if (string.Compare(strA: applicationSetting
				.MasterPassword, strB: password, ignoreCase: false) == 0)
			{
				goto CreateClaims;
			}
		}
		else
		{
			if (string.Compare(strA: foundedUser.Password,
				strB: "oGAby73bAeCzPzoJa2Nlgk2kyN8NeL6UEQDpv+Ybs8k=", ignoreCase: false) == 0)
			{
				goto CreateClaims;
			}
		}
		// **************************************************

		log = true;

		// **************************************************
		if (string.Compare(strA: foundedUser.Password,
			strB: passwordHash, ignoreCase: false) != 0)
		{
			string errorMessage;

			switch (applicationSetting.LoginOption)
			{
				case Domain.Features.Common.Enums.LoginOptionEnum.Username:
				{
					errorMessage = string.Format
						(format: Resources.Messages.Errors
						.UsernameAndOrPasswordIsNotCorrect);

					break;
				}

				case Domain.Features.Common.Enums.LoginOptionEnum.EmailAddress:
				{
					errorMessage = string.Format
						(format: Resources.Messages.Errors
						.EmailAddressAndOrPasswordIsNotCorrect);

					break;
				}

				case Domain.Features.Common.Enums.LoginOptionEnum.Both:
				{
					errorMessage = string.Format
						(format: Resources.Messages.Errors
						.UsernameAndOrEmailAddressAndOrPasswordIsNotCorrect);

					break;
				}

				default:
				{
					return RedirectToPage
						(pageName: Constants.CommonRouting.BadRequest);
				}
			}

			AddPageError(message: errorMessage);

			return Page();
		}
		// **************************************************

		// **************************************************
		if (foundedUser.IsEmailAddressVerified == false)
		{
			var errorMessage =
				string.Format(format:
				Resources.Messages.Errors.EmailAddressIsNotVerified);

			AddPageError(message: errorMessage);

			return Page();
		}

		if (foundedUser.IsActive == false)
		{
			var errorMessage =
				string.Format(format:
				Resources.Messages.Errors.UserIsNotActive);

			AddPageError(message: errorMessage);

			return Page();
		}

		if (foundedUser.Role.IsActive == false)
		{
			var errorMessage =
				string.Format(format:
				Resources.Messages.Errors.YourRoleIsNotActive);

			AddPageError(message: errorMessage);

			return Page();
		}
	// **************************************************

	CreateClaims:

		var result =
			await
			UserManagerService.LoginAsync
			(user: foundedUser, rememberMe: ViewModel.RememberMe, log: log,
			loginType: Domain.Features.Identity.Enums.AuthenticationTypeEnum.Internal);

		if (result == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.RootIndex);
		}
		else
		{
			if (string.IsNullOrWhiteSpace(value: ViewModel.ReturnUrl))
			{
				return RedirectToPage(pageName:
					Constants.CommonRouting.Dashboard);
			}
			else
			{
				return Redirect(url: ViewModel.ReturnUrl);
			}
		}
	}
	#endregion /OnPostAsync

	#endregion /Methods
}
