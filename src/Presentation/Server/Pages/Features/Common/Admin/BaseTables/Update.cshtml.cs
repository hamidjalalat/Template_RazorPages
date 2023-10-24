using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Features.Common;

namespace Server.Pages.Features.Common.Admin.BaseTables;

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
	public ViewModels.Pages.Features.Common.Admin.BaseTables.UpdateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? id)
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
		if (id is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var foundedItem =
			await
			DatabaseContext.BaseTables
			.Where(current => current.Id == id.Value)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		ViewModel.Id = foundedItem.Id;

		ViewModel.Code = foundedItem.Code;
		ViewModel.Type = foundedItem.Type;
		ViewModel.Ordering = foundedItem.Ordering;

		ViewModel.IsActive = foundedItem.IsActive;
		ViewModel.IsTestData = foundedItem.IsTestData;

		ViewModel.Icon = foundedItem.Icon;
		ViewModel.Color = foundedItem.Color;
		ViewModel.ImageUrl = foundedItem.ImageUrl;
		ViewModel.CoverImageUrl = foundedItem.CoverImageUrl;

		ViewModel.Title =
			foundedItem.LocalizedBaseTables?.FirstOrDefault
			(current => current.CultureId == currentCulture.Id)?.Title;

		ViewModel.Description =
			foundedItem.LocalizedBaseTables?.FirstOrDefault
			(current => current.CultureId == currentCulture.Id)?.Description;
		// **************************************************

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

		// **************************************************
		var foundedBaseTable =
			await
			DatabaseContext.BaseTables
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedBaseTable is null)
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
		var isCodeFound = false;

		isCodeFound =
			await
			DatabaseContext.BaseTables
			.Where(current => current.Id != ViewModel.Id)
			.Where(current => current.Code == ViewModel.Code)
			.AnyAsync();

		if (isCodeFound)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.Code);

			AddPageError(message: errorMessage);
		}
		// **************************************************

		// **************************************************
		// این قسمت خیلی خاص است
		// **************************************************
		var title =
			ViewModel.Title.Fix()!;

		var isTitleFound = false;

		var specialLocalizedBaseTable =
			await
			DatabaseContext.LocalizedBaseTables

			.Where(current => current.CultureId == currentCulture.Id)
			.Where(current => current.BaseTableId == foundedBaseTable.Id)

			.FirstOrDefaultAsync();

		if (specialLocalizedBaseTable is null)
		{
			isTitleFound =
				await
				DatabaseContext.LocalizedBaseTables

				.Where(current => current.BaseTableId == foundedBaseTable.Id)

				.Where(current => current.CultureId == currentCulture.Id)
				.Where(current => current.Title.ToLower() == title.ToLower())

				.AnyAsync();
		}
		else
		{
			isTitleFound =
				await
				DatabaseContext.LocalizedBaseTables

				.Where(current => current.Id != specialLocalizedBaseTable.Id)

				.Where(current => current.CultureId == currentCulture.Id)
				.Where(current => current.Title.ToLower() == title.ToLower())

				.AnyAsync();
		}

		if (isTitleFound)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.Title);

			AddPageError(message: errorMessage);
		}
		// **************************************************

		if (isCodeFound || isTitleFound)
		{
			return Page();
		}

		// **************************************************
		foundedBaseTable.SetUpdateDateTime();

		//foundedItem.Code = ViewModel.Code;
		//foundedItem.Type = ViewModel.Type;

		foundedBaseTable.Ordering = ViewModel.Ordering;

		foundedBaseTable.IsActive = ViewModel.IsActive;
		foundedBaseTable.IsTestData = ViewModel.IsTestData;

		foundedBaseTable.Icon = ViewModel.Icon;
		foundedBaseTable.Color = ViewModel.Color;
		foundedBaseTable.ImageUrl = ViewModel.ImageUrl;
		foundedBaseTable.CoverImageUrl = ViewModel.CoverImageUrl;
		// **************************************************

		// **************************************************
		var localizedItem =
			await
			DatabaseContext.LocalizedBaseTables
			.Where(current => current.BaseTableId == foundedBaseTable.Id)
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefaultAsync();

		if (localizedItem is not null)
		{
			localizedItem.SetUpdateDateTime();

			localizedItem.Title = title;
			localizedItem.Description = ViewModel.Description.Fix();
		}
		else
		{
			localizedItem =
				new Domain.Features.Common.LocalizedBaseTable
				(cultureId: currentCulture.Id, baseTableId: foundedBaseTable.Id, title: title)
				{
					Description = ViewModel.Description.Fix(),
				};

			await DatabaseContext.AddAsync(entity: localizedItem);
		}
		// **************************************************

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.BaseTable);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
