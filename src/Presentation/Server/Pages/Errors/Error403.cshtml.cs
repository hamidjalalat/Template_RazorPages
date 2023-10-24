namespace Server.Pages.Errors;

public class Error403Model : Infrastructure.BasePageModel
{
	public Error403Model() : base()
	{
	}

	public System.Threading.Tasks.Task OnGetAsync()
	{
		return System.Threading.Tasks.Task.CompletedTask;
	}
}
