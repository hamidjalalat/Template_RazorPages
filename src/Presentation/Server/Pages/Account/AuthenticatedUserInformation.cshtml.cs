namespace Server.Pages.Account;

[Infrastructure.Security.CustomAuthorize]
public class AuthenticatedUserInformationModel : Infrastructure.BasePageModel
{
	public AuthenticatedUserInformationModel() : base()
	{
	}

	public async System.Threading.Tasks.Task OnGet()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
}
