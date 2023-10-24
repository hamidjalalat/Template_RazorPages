using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.User.Posts;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.SimpleUser)]
public class CreateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public CreateModel(
		Persistence.DatabaseContext databaseContext,
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
	public ViewModels.Pages.Features.Cms.User.Posts.CreateViewModel ViewModel { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? PostTypesSelectList { get; set; }
	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? PostCategoriesSelectList { get; set; }

	public Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; init; }
	public Services.Features.Identity.UserNotificationService UserNotificationService { get; init; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		PostTypesSelectList =
			await
			Infrastructure.SelectLists.GetPostTypesForUserAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		PostCategoriesSelectList =
			await
			Infrastructure.SelectLists.GetPostCategoriesForUserAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		await System.Threading.Tasks.Task.CompletedTask;
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
				Constants.CommonRouting.InternalServerError);
		}
		// **************************************************

		// **************************************************
		var userId =
			AuthenticatedUserService.UserId;

		if (userId is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
		}
		// **************************************************

		// **************************************************
		var newEntity =
			new Domain.Features.Cms.Post
			(cultureId: currentCulture.Id, userId: userId.Value,
			typeId: ViewModel.TypeId, ViewModel.CategoryId, title: ViewModel.Title.Fix()!)
			{
				Body = ViewModel.Body.Fix(),
				Author = ViewModel.Author.Fix(),
				ImageUrl = ViewModel.ImageUrl.Fix(),
				MovieUrl = ViewModel.ImageUrl.Fix(),
				Description = ViewModel.Description.Fix(),
				Introduction = ViewModel.Introduction.Fix(),
				CoverImageUrl = ViewModel.CoverImageUrl.Fix(),

				IsDraft = ViewModel.IsDraft,
				IsCommentingEnabled = ViewModel.IsCommentingEnabled,
				DisplayCommentsAfterVerification = ViewModel.DisplayCommentsAfterVerification,
			};

		var entityEntry =
			await
			DatabaseContext.AddAsync(entity: newEntity);

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.Created,
			Resources.DataDictionary.Post);

		AddToastSuccess(message: successMessage);
		// **************************************************

		// **************************************************
		var userEmailAddress =
			AuthenticatedUserService.EmailAddress;

		if (string.IsNullOrWhiteSpace(value: userEmailAddress) == false)
		{
			await
			UserNotificationService
				.NotifyAllActiveManagersAfterNewPostPublishedAsync
				(newPost: newEntity, userEmailAddress: userEmailAddress);
		}
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
