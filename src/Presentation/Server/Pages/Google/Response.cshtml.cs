using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace Server.Pages.Google;

public class ResponseModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public ResponseModel
		(Persistence.DatabaseContext databaseContext,
		Infrastructure.Security.UserManagerService userManagerService,
		Services.Features.Common.HttpContextService httpContextService,
		Services.Features.Identity.UserNotificationService userNotificationService,
		Services.Features.Common.ApplicationSettingService applicationSettingService) : base(databaseContext: databaseContext)
	{
		HttpContextService = httpContextService;
		UserManagerService = userManagerService;
		UserNotificationService = userNotificationService;
		ApplicationSettingService = applicationSettingService;
	}
	#endregion /Constructor

	#region Properties

	private Infrastructure.Security.UserManagerService UserManagerService { get; init; }
	private Services.Features.Common.HttpContextService HttpContextService { get; init; }
	private Services.Features.Identity.UserNotificationService UserNotificationService { get; init; }
	private Services.Features.Common.ApplicationSettingService ApplicationSettingService { get; init; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		// **************************************************
		var remoteIP =
			HttpContextService.GetRemoteIpAddress();

		if (string.IsNullOrWhiteSpace(value: remoteIP))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}
		// **************************************************

		// **************************************************
		var emailAddress =
			await
			GetEmailAddressFromGoogleAsync();

		if (string.IsNullOrWhiteSpace(value: emailAddress))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.RootIndex);
		}
		// **************************************************

		// using Microsoft.AspNetCore.Authentication;
		await HttpContext.SignOutAsync(scheme:
			Infrastructure.Security.Constants.Scheme.Default);

		var user =
			await
			GetUserAndCreateItIfNowExistsAsync
			(emailAddress: emailAddress, remoteIP: remoteIP);

		if (user is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.RootIndex);
		}

		var result =
			await
			UserManagerService.LoginAsync
			(user: user, rememberMe: false, log: true,
			loginType: Domain.Features.Identity.Enums.AuthenticationTypeEnum.Google);

		if (result)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Dashboard);
		}
		else
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.RootIndex);
		}
	}
	#endregion /OnGetAsync()

	#region GetEmailAddressFromGoogleAsync()
	private async System.Threading.Tasks.Task<string?> GetEmailAddressFromGoogleAsync()
	{
		var result =
			// using Microsoft.AspNetCore.Authentication;
			await
			HttpContext.AuthenticateAsync
			(scheme: Infrastructure.Security.Constants.Scheme.Default);

		if (result is null)
		{
			return null;
		}

		var claimsPrincipal = result.Principal;

		if (claimsPrincipal is null)
		{
			return null;
		}

		var claimsIdentities =
			claimsPrincipal.Identities;

		if (claimsIdentities is null)
		{
			return null;
		}

		var claimsIdentitiesList =
			claimsIdentities.ToList();

		if (claimsIdentitiesList.Count == 0)
		{
			return null;
		}

		var claims = new System.Collections
			.Generic.List<System.Security.Claims.Claim>();

		foreach (var claimsIdentity in claimsIdentitiesList)
		{
			foreach (var claim in claimsIdentity.Claims)
			{
				claims.Add(item: claim);
			}
		}

		var emailAddressClaim =
			claims
			.Where(current => current.Type.ToLower() == System.Security.Claims.ClaimTypes.Email)
			.FirstOrDefault();

		if (emailAddressClaim is null)
		{
			return null;
		}

		var emailAddress =
			emailAddressClaim.Value;

		if (string.IsNullOrWhiteSpace(value: emailAddress))
		{
			return null;
		}

		emailAddress =
			emailAddress.ToLower();

		return emailAddress;
	}
	#endregion /GetEmailAddressFromGoogleAsync()

	#region GetUserAndCreateItIfNowExistsAsync()
	private async System.Threading.Tasks
		.Task<Domain.Features.Identity.User?>
		GetUserAndCreateItIfNowExistsAsync(string emailAddress, string remoteIP)
	{
		emailAddress =
			emailAddress.Fix()!.ToLower();

		var user =
			await
			DatabaseContext.Users
			.Where(current => current.EmailAddress.ToLower() == emailAddress)
			.FirstOrDefaultAsync();

		if (user is not null)
		{
			if (user.IsActive == false)
			{
				return null;
			}

			return user;
		}

		var applicationSetting =
			await
			ApplicationSettingService.GetInstanceAsync();

		// **************************************************
		user = new Domain.Features.Identity.User
			(emailAddress: emailAddress, registerIP: remoteIP,
			registerType: Domain.Features.Identity.Enums.AuthenticationTypeEnum.Google)
		{
			IsEmailAddressVerified = true,

			IsActive = applicationSetting
				.ActivateUserAfterRegistration,

			RoleId =
					Constants.BaseTableItem.Role.SimpleUser,
		};

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
				.NotifyAllActiveManagersAfterUserRegistrationAsync(newUser: user);
		}
		catch { }

		if (user.IsActive == false)
		{
			return null;
		}

		return user;
	}
	#endregion /GetUserAndCreateItIfNowExistsAsync()

	#endregion /Methods
}
