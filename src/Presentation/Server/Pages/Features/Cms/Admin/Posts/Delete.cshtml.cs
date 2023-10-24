using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Posts;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Administrator)]
public class DeleteModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public DeleteModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Cms.Admin.Posts.DetailsOrDeleteViewModel ViewModel { get; private set; }

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
				.Features.Cms.Admin.Posts.DetailsOrDeleteViewModel
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
				Ordering = current.Ordering,
				IsDeleted = current.IsDeleted,
				CategoryId = current.CategoryId,
				IsFeatured = current.IsFeatured,
				Description = current.Description,
				Introduction = current.Introduction,
				AdminDescription = current.AdminDescription,
				IsCommentingEnabled = current.IsCommentingEnabled,
				DoesSearchEnginesIndexIt = current.DoesSearchEnginesIndexIt,
				DoesSearchEnginesFollowIt = current.DoesSearchEnginesFollowIt,
				DisplayCommentsAfterVerification = current.DisplayCommentsAfterVerification,

				UserId = current.UserId,
				CommentCount = current.Comments.Count,
				DeleteDateTime = current.DeleteDateTime,
				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

				TypeName = current.Type == null ? string.Empty : current.Type.Name,
				CategoryName = current.Category == null ? string.Empty : current.Category.Name,
				UserFullName = current.User == null ? string.Empty : current.User.EmailAddress,
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

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync(System.Guid? id)
	{
		if (id is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.Posts
			.Where(current => current.Id == id.Value)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var hasAnyChildren =
			await
			DatabaseContext.Posts
			.Where(current => current.CategoryId == id.Value)
			.AnyAsync();

		if (hasAnyChildren)
		{
			// **************************************************
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.CascadeDelete,
				arg0: Resources.DataDictionary.Post);

			AddToastError(message: errorMessage);
			// **************************************************

			return RedirectToPage(pageName:
				Constants.CommonRouting.CurrentIndex);
		}
		// **************************************************

		// **************************************************
		var entityEntry =
			DatabaseContext.Remove(entity: foundedItem);

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Deleted,
			arg0: Resources.DataDictionary.Post);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
