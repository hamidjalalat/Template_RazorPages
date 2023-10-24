using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Hrm.User.UserSites;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.SimpleUser)]
public class CreateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public CreateModel
		(Persistence.DatabaseContext databaseContext,
		Infrastructure.Security.AuthenticatedUserService authenticatedUserService) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
		AuthenticatedUserService = authenticatedUserService;
	}
	#endregion /Constructor

	#region Properties

	public Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Hrm.User.UserSites.CreateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var siteUrl =
			ViewModel.SiteUrl.Fix()!;

		var isSiteUrlFound =
			await
			DatabaseContext.UserSites
			.Where(current => current.SiteUrl.ToLower() == siteUrl.ToLower())
			.Where(current => current.UserId == AuthenticatedUserService.UserId)
			.AnyAsync();

		if (isSiteUrlFound)
		{
			var errorMessage = string.Format
				(Resources.Messages.Errors.AlreadyExists,
				Resources.DataDictionary.SiteUrl);

			AddPageError(message: errorMessage);

			return Page();
		}
		// **************************************************

		// **************************************************
		var newEntity =
			new Domain.Features.Hrm.UserSite
			(userId: AuthenticatedUserService.UserId!.Value, siteUrl: siteUrl)
			{
				IsActive = ViewModel.IsActive,
				Ordering = ViewModel.Ordering,
			};

		await DatabaseContext.AddAsync(entity: newEntity);

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.Created,
			Resources.DataDictionary.UserSites);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
