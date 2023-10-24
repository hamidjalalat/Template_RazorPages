using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Layouts;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Programmer)]
public class CreateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public CreateModel(Persistence.DatabaseContext
		databaseContext) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Cms.Admin.Layouts.CreateViewModel ViewModel { get; set; }

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
		var title =
			ViewModel.Title.Fix()!.ToLower();

		var foundedAny =
			await
			DatabaseContext.Layouts
			.Where(current => current.Title.ToLower() == title)
			.AnyAsync();

		if (foundedAny)
		{
			// **************************************************
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.Title);

			AddPageError(message: errorMessage);
			// **************************************************

			return Page();
		}
		// **************************************************

		// **************************************************
		var newEntity =
			new Domain.Features.Cms.Layout(title: title,
			type: Domain.Features.Cms.Enums.LayoutTypeEnum.Custom)
			{
				IsActive = ViewModel.IsActive,
				Ordering = ViewModel.Ordering,

				Title = title,

				Theme = ViewModel.Theme,

				DisplayDefaultMenu1 = ViewModel.DisplayDefaultMenu1,
				DisplayDefaultMenu2 = ViewModel.DisplayDefaultMenu2,
				DisplayDefaultMenu3 = ViewModel.DisplayDefaultMenu3,
				DisplayDefaultFooter = ViewModel.DisplayDefaultFooter,

				ContainerCssClass = ViewModel.ContainerCssClass.Fix(),

				TopBody = ViewModel.TopBody.Fix(),
				StartCssClass = ViewModel.StartCssClass.Fix(),
				StartBody = ViewModel.StartBody.Fix(),
				MainCssClass = ViewModel.MainCssClass.Fix(),
				EndCssClass = ViewModel.EndCssClass.Fix(),
				EndBody = ViewModel.EndBody.Fix(),
				BottomBody = ViewModel.BottomBody.Fix(),

				CustomStyleSheets = ViewModel.CustomStyleSheets.Fix(),
				CustomScripts = ViewModel.CustomScripts.Fix(),
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
			Resources.DataDictionary.Layout);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
