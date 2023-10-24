namespace Server.Pages;

[Infrastructure.Security.CustomAuthorize]
public class DashboardModel : Infrastructure.BasePageModel
{
	#region Constructor
	public DashboardModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties
	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
