using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Layouts;

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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Cms.Admin.Layouts.IndexItemViewModel> ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		ViewModel =
			await
			DatabaseContext.Layouts
			.OrderByDescending(current => current.UpdateDateTime)
			.Select(current => new ViewModels.Pages.Features.Cms.Admin.Layouts.IndexItemViewModel
			{
				Id = current.Id,

				Title = current.Title,
				Theme = current.Theme,

				Type = current.Type,
				IsActive = current.IsActive,
				Ordering = current.Ordering,

				PageCount = current.Pages.Count,

				UpdateDateTime = current.UpdateDateTime,
				InsertDateTime = current.InsertDateTime,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
