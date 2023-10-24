using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Hrm.User.UserSites;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.SimpleUser)]
public class IndexModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public IndexModel
		(Persistence.DatabaseContext databaseContext,
		Infrastructure.Security.AuthenticatedUserService authenticatedUserService) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
		AuthenticatedUserService = authenticatedUserService;
	}
	#endregion /Constructor

	#region Properties

	public Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; }
	public System.Collections.Generic.List<ViewModels.Pages.Features.Hrm.User.UserSites.IndexItemViewModel> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		ViewModel =
			await
			DatabaseContext.UserSites

			.Where(current => current.UserId == AuthenticatedUserService.UserId)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.SiteUrl)

			.Select(current => new ViewModels.Pages
				.Features.Hrm.User.UserSites.IndexItemViewModel
			{
				Id = current.Id,

				SiteUrl = current.SiteUrl,

				IsActive = current.IsActive,

				Ordering = current.Ordering,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
