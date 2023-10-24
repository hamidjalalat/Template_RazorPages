using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Layouts;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Programmer)]
public class UpdateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateModel(Persistence.DatabaseContext
		databaseContext) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Cms.Admin.Layouts.UpdateViewModel ViewModel { get; set; }

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
			DatabaseContext.Layouts
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages.Features.Cms.Admin.Layouts.UpdateViewModel()
			{
				Id = current.Id,

				IsActive = current.IsActive,
				Ordering = current.Ordering,

				Title = current.Title,
				Theme = current.Theme,

				DisplayDefaultMenu1 = current.DisplayDefaultMenu1,
				DisplayDefaultMenu2 = current.DisplayDefaultMenu2,
				DisplayDefaultMenu3 = current.DisplayDefaultMenu3,
				DisplayDefaultFooter = current.DisplayDefaultFooter,

				ContainerCssClass = current.ContainerCssClass,

				TopBody = current.TopBody,
				StartCssClass = current.StartCssClass,
				StartBody = current.StartBody,
				MainCssClass = current.MainCssClass,
				EndCssClass = current.EndCssClass,
				EndBody = current.EndBody,
				BottomBody = current.BottomBody,

				CustomStyleSheets = current.CustomStyleSheets,
				CustomScripts = current.CustomScripts,
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
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.Layouts
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var title =
			ViewModel.Title.Fix()!.ToLower();

		var foundedAny =
			await
			DatabaseContext.Layouts
			.Where(current => current.Id != ViewModel.Id)
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
		foundedItem.SetUpdateDateTime();

		foundedItem.IsActive = ViewModel.IsActive;
		foundedItem.Ordering = ViewModel.Ordering;

		foundedItem.Title = title;
		foundedItem.Theme = ViewModel.Theme;

		foundedItem.DisplayDefaultMenu1 = ViewModel.DisplayDefaultMenu1;
		foundedItem.DisplayDefaultMenu2 = ViewModel.DisplayDefaultMenu2;
		foundedItem.DisplayDefaultMenu3 = ViewModel.DisplayDefaultMenu3;
		foundedItem.DisplayDefaultFooter = ViewModel.DisplayDefaultFooter;

		foundedItem.ContainerCssClass = ViewModel.ContainerCssClass;

		foundedItem.TopBody = ViewModel.TopBody;
		foundedItem.StartCssClass = ViewModel.StartCssClass;
		foundedItem.StartBody = ViewModel.StartBody;
		foundedItem.MainCssClass = ViewModel.MainCssClass;
		foundedItem.EndCssClass = ViewModel.EndCssClass;
		foundedItem.EndBody = ViewModel.EndBody;
		foundedItem.BottomBody = ViewModel.BottomBody;

		foundedItem.CustomStyleSheets = ViewModel.CustomStyleSheets;
		foundedItem.CustomScripts = ViewModel.CustomScripts;
		// **************************************************

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Layout);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
