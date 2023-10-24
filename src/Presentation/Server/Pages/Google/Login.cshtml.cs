using Microsoft.AspNetCore.Authentication;

namespace Server.Pages.Google;

public class LoginModel :
	Infrastructure.BasePageModel
{
	public LoginModel() : base()
	{
	}

	public async System.Threading.Tasks.Task OnGet()
	{
		var redirectUri =
			"/google/response";

		var authenticationProperties = new Microsoft
			.AspNetCore.Authentication.AuthenticationProperties
			{
				RedirectUri = redirectUri,
			};

		// using Microsoft.AspNetCore.Authentication;
		await HttpContext.ChallengeAsync
			(scheme: Microsoft.AspNetCore.Authentication.Google.GoogleDefaults.AuthenticationScheme,
			properties: authenticationProperties);
	}
}
