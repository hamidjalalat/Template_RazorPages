using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Common.Admin.BaseTables;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Supervisor)]
public class DetailsModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public DetailsModel(Persistence.DatabaseContext
		databaseContext) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	public ViewModels.Pages.Features.Common.Admin.BaseTables.DetailsOrDeleteViewModel ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
	<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? id)
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

		if (id is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var result =
			await
			DatabaseContext.BaseTables

			.Where(current => current.Id == id.Value)

			.Select(current => new ViewModels.Pages
				.Features.Common.Admin.BaseTables.DetailsOrDeleteViewModel()
			{
				Id = current.Id,

				//Name = current.Name,

				IsActive = current.IsActive,

				//Code = current.Code,
				Ordering = current.Ordering,
				//UserCount = current.Users.Count,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

#pragma warning disable CS8602

				//Title = current.LocalizedRoles.FirstOrDefault
				//	(other => other.CultureId == currentCulture.Id).Title,

				//Description = current.LocalizedRoles.FirstOrDefault
				//	(other => other.CultureId == currentCulture.Id).Description,

#pragma warning restore CS8602

			})
			.FirstOrDefaultAsync();

		if (result is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		ViewModel = result;

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
