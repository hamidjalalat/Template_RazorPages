using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace Server.Pages.Account;

[Infrastructure.Security.CustomAuthorize]
public class LogoutModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	public LogoutModel(Persistence.DatabaseContext databaseContext, Infrastructure.Security
		.AuthenticatedUserService authenticatedUserService) : base(databaseContext: databaseContext)
	{
		AuthenticatedUserService = authenticatedUserService;
	}

	private Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; }

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGet()
	{
		// using Microsoft.AspNetCore.Authentication;
		await HttpContext.SignOutAsync(scheme:
			Infrastructure.Security.Constants.Scheme.Default);

		// **************************************************
		var sessionId =
			AuthenticatedUserService.SessionId;

		if (sessionId is not null)
		{
			var loginLog =
				await
				DatabaseContext.LoginLogs
				.Where(current => current.Id == sessionId.Value)
				.FirstOrDefaultAsync();

			if (loginLog is not null)
			{
				loginLog.LogoutDateTime = Dtat.DateTime.Now;

				await DatabaseContext.SaveChangesAsync();
			}
		}
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.RootIndex);
	}
}
