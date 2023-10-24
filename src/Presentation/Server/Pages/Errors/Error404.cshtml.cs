namespace Server.Pages.Errors;

public class Error404Model : Infrastructure.BasePageModel
{
	public Error404Model() : base()
	{
	}

	public System.Threading.Tasks.Task OnGetAsync()
	{
		return System.Threading.Tasks.Task.CompletedTask;
	}
}
