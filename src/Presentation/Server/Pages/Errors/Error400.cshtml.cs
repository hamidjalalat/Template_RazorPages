namespace Server.Pages.Errors;

public class Error400Model : Infrastructure.BasePageModel
{
	public Error400Model() : base()
	{
	}

	public System.Threading.Tasks.Task OnGetAsync()
	{
		return System.Threading.Tasks.Task.CompletedTask;
	}
}
