namespace Server.Pages.Account;

[Infrastructure.Security.CustomAuthorize]
public class AccessDeniedModel : Infrastructure.BasePageModel
{
	public AccessDeniedModel() : base()
	{
	}

	public System.Threading.Tasks.Task OnGetAsync()
	{
		return System.Threading.Tasks.Task.CompletedTask;
	}
}
