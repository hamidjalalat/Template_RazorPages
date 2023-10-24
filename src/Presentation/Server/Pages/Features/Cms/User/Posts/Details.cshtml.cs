using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.User.Posts;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.SimpleUser)]
public class DetailsModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public DetailsModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	public ViewModels.Pages.Features.Cms.User.Posts.DetailsOrDeleteViewModel ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
	<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? id)
	{
		if (id is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var result =
			await
			DatabaseContext.Posts
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages
				.Features.Cms.User.Posts.DetailsOrDeleteViewModel()
			{
				Id = current.Id,
				Body = current.Body,
				Hits = current.Hits,
				Score = current.Score,
				Title = current.Title,
				Author = current.Author,
				IsDraft = current.IsDraft,
				MovieUrl = current.MovieUrl,
				ImageUrl = current.ImageUrl,
				IsActive = current.IsActive,
				CategoryId = current.CategoryId,
				IsFeatured = current.IsFeatured,
				Description = current.Description,
				Introduction = current.Introduction,
				AdminDescription = current.AdminDescription,
				IsCommentingEnabled = current.IsCommentingEnabled,
				DisplayCommentsAfterVerification = current.DisplayCommentsAfterVerification,

				CommentCount = current.Comments.Count,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

				TypeName = current.Type == null ? string.Empty : current.Type.Name,
				CategoryName = current.Category == null ? string.Empty : current.Category.Name,
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
