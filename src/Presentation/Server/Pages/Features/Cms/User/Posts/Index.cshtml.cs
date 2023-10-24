using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.User.Posts;

[Infrastructure.Security.CustomAuthorize]
public class IndexModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public IndexModel(Persistence.DatabaseContext databaseContext, Infrastructure.Security
		.AuthenticatedUserService authenticatedUserService) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
		AuthenticatedUserService = authenticatedUserService;
	}
	#endregion /Constructor

	#region Properties

	public Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; init; }
	public System.Collections.Generic.List<ViewModels.Pages.Features.Cms.User.Posts.IndexItemViewModel> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var userId =
			AuthenticatedUserService.UserId;

		ViewModel =
			await
			DatabaseContext.Posts

			.Where(current => current.UserId == userId)

			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)

			.OrderByDescending(current => current.UpdateDateTime)

			.Select(current => new ViewModels.Pages
				.Features.Cms.User.Posts.IndexItemViewModel
			{
				Id = current.Id,

				TypeId = current.TypeId,
				CategoryId = current.CategoryId,

				Hits = current.Hits,
				Score = current.Score,

				IsDraft = current.IsDraft,
				IsActive = current.IsActive,
				IsFeatured = current.IsFeatured,
				IsCommentingEnabled = current.IsCommentingEnabled,

				CommentCount = current.Comments.Count,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

				Title = current.Title,

				TypeName = current.Type!.Name,
				CategoryName = current.Category!.Name,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
