using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

[Infrastructure.Security.CustomAuthorize]
public class LoginLogsModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public LoginLogsModel
		(Persistence.DatabaseContext databaseContext,
		Infrastructure.Security.AuthenticatedUserService authenticatedUserService) : base(databaseContext: databaseContext)
	{
		ViewModel = new();

		AuthenticatedUserService = authenticatedUserService;
	}
	#endregion /Constructor

	#region Properties

	private Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; init; }
	public System.Collections.Generic.List<Domain.Features.Identity.LoginLog> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		ViewModel =
			await
			DatabaseContext.LoginLogs
			.Where(current => current.UserId == AuthenticatedUserService.UserId!.Value)
			.OrderByDescending(current => current.InsertDateTime)

			.Skip(count: 0)
			.Take(count: 50)

			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
