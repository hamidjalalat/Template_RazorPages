using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.User.Posts;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.SimpleUser)]
public class UpdateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateModel
		(Persistence.DatabaseContext databaseContext,
		Services.Features.Identity.UserNotificationService userNotificationService,
		Infrastructure.Security.AuthenticatedUserService authenticatedUserService) : base(databaseContext: databaseContext)
	{
		ViewModel = new();

		UserNotificationService = userNotificationService;
		AuthenticatedUserService = authenticatedUserService;
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Cms.User.Posts.UpdateViewModel ViewModel { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? PostTypesSelectList { get; set; }
	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? PostCategoriesSelectList { get; set; }

	public Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; init; }
	public Services.Features.Identity.UserNotificationService UserNotificationService { get; init; }

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
			.Features.Cms.User.Posts.UpdateViewModel()
			{
				Id = current.Id,

				TypeId = current.TypeId,
				CategoryId = current.CategoryId,

				Body = current.Body,
				Title = current.Title,
				Author = current.Author,
				MovieUrl = current.MovieUrl,
				ImageUrl = current.ImageUrl,
				Description = current.Description,
				Introduction = current.Introduction,
				CoverImageUrl = current.CoverImageUrl,

				IsDraft = current.IsDraft,
				IsCommentingEnabled = current.IsCommentingEnabled,
				DisplayCommentsAfterVerification = current.DisplayCommentsAfterVerification,
			})
			.FirstOrDefaultAsync();

		if (result is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		ViewModel = result;

		PostTypesSelectList =
			await
			Infrastructure.SelectLists.GetPostTypesForUserAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		PostCategoriesSelectList =
			await
			Infrastructure.SelectLists.GetPostCategoriesForUserAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			PostTypesSelectList =
				await
				Infrastructure.SelectLists.GetPostTypesForUserAsync
				(databaseContext: DatabaseContext, selectedValue: ViewModel.TypeId);

			PostCategoriesSelectList =
				await
				Infrastructure.SelectLists.GetPostCategoriesForUserAsync
				(databaseContext: DatabaseContext, selectedValue: ViewModel.CategoryId);

			return Page();
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.Posts
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedItem.TypeId = ViewModel.TypeId;
		foundedItem.CategoryId = ViewModel.CategoryId;

		foundedItem.Title = ViewModel.Title.Fix()!;

		foundedItem.Body = ViewModel.Body.Fix();
		foundedItem.Author = ViewModel.Author.Fix();
		foundedItem.MovieUrl = ViewModel.MovieUrl.Fix();
		foundedItem.ImageUrl = ViewModel.ImageUrl.Fix();
		foundedItem.Description = ViewModel.Description.Fix();
		foundedItem.Introduction = ViewModel.Introduction.Fix();
		foundedItem.CoverImageUrl = ViewModel.CoverImageUrl.Fix();

		foundedItem.IsDraft = ViewModel.IsDraft;
		foundedItem.IsCommentingEnabled = ViewModel.IsCommentingEnabled;
		foundedItem.DisplayCommentsAfterVerification = ViewModel.DisplayCommentsAfterVerification;
		// **************************************************

		// **************************************************
		foundedItem.IsActive = false;

		foundedItem.SetUpdateDateTime();
		// **************************************************

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Post);

		AddToastSuccess(message: successMessage);
		// **************************************************

		// **************************************************
		if (foundedItem.IsDraft == false)
		{
			var userEmailAddress =
				AuthenticatedUserService.EmailAddress;

			if (string.IsNullOrWhiteSpace(value: userEmailAddress) == false)
			{
				await
				UserNotificationService
					.NotifyAllActiveManagersAfterAOldPostModifiedAsync
					(modifiedPost: foundedItem, userEmailAddress: userEmailAddress);
			}
		}
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
