namespace Server.Pages.Errors;

public class Error500Model : Infrastructure.BasePageModel
{
	public Error500Model() : base()
	{
	}

	public System.Threading.Tasks.Task OnGetAsync()
	{
		return System.Threading.Tasks.Task.CompletedTask;
	}
}
