using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Common.Admin.BaseTables;

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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Common.Admin.BaseTables.IndexItemViewModel> ViewModel { get; private set; }

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
			DatabaseContext.BaseTables

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Code)

			.Select(current => new ViewModels.Pages
				.Features.Common.Admin.BaseTables.IndexItemViewModel
			{
				Id = current.Id,

				Color = current.Color,
				Icon = current.Icon,

				IsActive = current.IsActive,
				IsTestData = current.IsTestData,

				Code = current.Code,
				Type = current.Type,
				Ordering = current.Ordering,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

				ItemCount = current.BaseTableItems.Count,

#pragma warning disable CS8602

				Title =
					(current.LocalizedBaseTables == null
					||
					current.LocalizedBaseTables.Count == 0
					)
					?
					null
					:
					current.LocalizedBaseTables.FirstOrDefault
						(other => other.CultureId == currentCulture.Id).Title,

#pragma warning restore CS8602
			})
			.ToListAsync()
			;

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
