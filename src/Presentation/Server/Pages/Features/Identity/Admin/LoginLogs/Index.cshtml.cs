using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.LoginLogs;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Supervisor)]
public class IndexModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public IndexModel(Persistence.DatabaseContext
		databaseContext) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	public System.Collections.Generic.List<ViewModels.Pages.Features.Identity.Admin.LoginLogs.IndexItemViewModel> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		// **************************************************
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var currentCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.Lcid == currentUICultureLcid)
			.FirstOrDefaultAsync();

		if (currentCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		ViewModel =
			await
			DatabaseContext.LoginLogs

			.OrderByDescending(current => current.InsertDateTime)

			.Select(current => new ViewModels.Pages
				.Features.Identity.Admin.LoginLogs.IndexItemViewModel
			{
				Id = current.Id,
				UserId = current.UserId,
				UserIP = current.UserIP,

				LoginType = current.LoginType,

				Username = current.User!.Username,
				EmailAddress = current.User!.EmailAddress,
				CellPhoneNumber = current.User!.CellPhoneNumber,

				LoginDateTime = current.InsertDateTime,
				LogoutDateTime = current.LogoutDateTime,

#pragma warning disable CS8602

				LastName =
					current.User!.LocalizedUsers.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).LastName,

				FirstName =
					current.User!.LocalizedUsers.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).FirstName,

				RoleTitle =
					current.User!.Role!.LocalizedBaseTableItems.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).Title,

				//GenderPrefix =
				//	current.User!.Gender!.LocalizedGenders.FirstOrDefault
				//	(other => other.CultureId == currentCulture.Id).Prefix,

#pragma warning restore CS8602

			})

			.Skip(count: 0)
			.Take(count: 200)

			.ToListAsync()
			;

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
