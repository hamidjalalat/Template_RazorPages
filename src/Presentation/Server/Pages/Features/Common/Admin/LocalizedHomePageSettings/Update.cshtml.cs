using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Common.Admin.LocalizedHomePageSettings;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Administrator)]
public class UpdateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Common.Admin.LocalizedHomePageSettings.UpdateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
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
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.LocalizedHomePageSettings
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			foundedItem = new Domain.Features.Common
				.LocalizedHomePageSetting(cultureId: currentCulture.Id);

			await DatabaseContext.AddAsync(entity: foundedItem);

			await DatabaseContext.SaveChangesAsync();
		}
		// **************************************************

		ViewModel =
			new ViewModels.Pages.Features.Common.Admin
			.LocalizedHomePageSettings.UpdateViewModel()
			{
				Id = foundedItem.Id,

				Hits = foundedItem.Hits,

				TopBody = foundedItem.TopBody,
				BottomBody = foundedItem.BottomBody,

				Title = foundedItem.Title,
				Author = foundedItem.Author,
				ImageUrl = foundedItem.ImageUrl,
				Keywords = foundedItem.Keywords,
				Description = foundedItem.Description,
			};

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
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
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.LocalizedHomePageSettings
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedItem.SetUpdateDateTime();

		foundedItem.Hits = ViewModel.Hits;

		foundedItem.TopBody	= ViewModel.TopBody.Fix();
		foundedItem.BottomBody = ViewModel.BottomBody.Fix();

		foundedItem.Title = ViewModel.Title.Fix();
		foundedItem.Author = ViewModel.Author.Fix();
		foundedItem.ImageUrl = ViewModel.ImageUrl.Fix();
		foundedItem.Keywords = ViewModel.Keywords.Fix();
		foundedItem.Description = ViewModel.Description.Fix();
		// **************************************************

		await DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Data);

		AddPageSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
