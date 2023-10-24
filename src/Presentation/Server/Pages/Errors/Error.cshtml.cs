namespace Server.Pages.Errors;

public class ErrorModel : Infrastructure.BasePageModel
{
	public ErrorModel() : base()
	{
	}

	public System.Threading.Tasks.Task OnGetAsync()
	{
		return System.Threading.Tasks.Task.CompletedTask;
	}
}
