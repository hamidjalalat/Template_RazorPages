using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Features.Common;
using System.Net.WebSockets;

namespace Server.Pages.Features.Common.Admin.BaseTableItems;

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

		// **************************************************
		// یک سری دستور الکی برای جلوگیری از اخطار ویژوال استودیو
		BaseTableTitle = "Title";

		BaseTable =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.Gender,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.NotEnum);
		// **************************************************
	}
	#endregion /Constructor

	#region Properties

	public string BaseTableTitle { get; private set; }
	public Domain.Features.Common.BaseTable BaseTable { get; private set; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Common.Admin.BaseTableItems.UpdateViewModel ViewModel { get; set; }

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
			DatabaseContext.BaseTableItems
			.Include(current => current.BaseTable)
			.ThenInclude(baseTable => baseTable!.LocalizedBaseTables)
			.Include(current => current.LocalizedBaseTableItems)
			.Where(current => current.Id == id.Value)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		BaseTable = foundedItem.BaseTable!;

		BaseTableTitle =
			BaseTable.Code.ToString();

		var localizedBaseTable =
			BaseTable.LocalizedBaseTables
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefault();

		if (localizedBaseTable is not null)
		{
			var title = localizedBaseTable.Title;

			if (string.IsNullOrWhiteSpace(value: title) == false)
			{
				BaseTableTitle = title;
			}
		}
		// **************************************************

		// **************************************************
		ViewModel.Id = foundedItem.Id;

		ViewModel.Code = foundedItem.Code;
		ViewModel.Ordering = foundedItem.Ordering;

		ViewModel.IsActive = foundedItem.IsActive;
		ViewModel.IsTestData = foundedItem.IsTestData;

		ViewModel.Icon = foundedItem.Icon;
		ViewModel.Color = foundedItem.Color;
		ViewModel.KeyName = foundedItem.KeyName;
		ViewModel.ImageUrl = foundedItem.ImageUrl;
		ViewModel.CoverImageUrl = foundedItem.CoverImageUrl;

		ViewModel.Title =
			foundedItem.LocalizedBaseTableItems?.FirstOrDefault
			(current => current.CultureId == currentCulture.Id)?.Title;

		ViewModel.Description =
			foundedItem.LocalizedBaseTableItems?.FirstOrDefault
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
		var foundedItem =
			await
			DatabaseContext.BaseTableItems
			.Include(current => current.BaseTable)
			.ThenInclude(baseTable => baseTable!.LocalizedBaseTables)
			.Include(current => current.LocalizedBaseTableItems)
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		BaseTable = foundedItem.BaseTable!;

		BaseTableTitle =
			BaseTable.Code.ToString();

		var localizedBaseTable =
			BaseTable.LocalizedBaseTables
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefault();

		if (localizedBaseTable is not null)
		{
			var baseTableTitle = localizedBaseTable.Title;

			if (string.IsNullOrWhiteSpace(value: baseTableTitle) == false)
			{
				BaseTableTitle = baseTableTitle;
			}
		}
		// **************************************************

		// **************************************************
		if (BaseTable.Type == Domain.Features.Common.Enums.BaseTableTypeEnum.Enum)
		{
			if (ViewModel.Code is null)
			{
				var errorMessage =
					string.Format(format: Resources.Messages.Validations.Required,
					arg0: Resources.DataDictionary.Code);

				ModelState.AddModelError
					(key: nameof(ViewModel.Code), errorMessage: errorMessage);

				AddPageError(message: errorMessage);
			}
		}
		else
		{
			ViewModel.Code = null;
		}
		// **************************************************

		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var keyName =
			ViewModel.KeyName.Fix()!;

		var isKeyNameFound =
			await
			DatabaseContext.BaseTableItems
			.Where(current => current.Id != ViewModel.Id)
			.Where(current => current.BaseTableId == BaseTable.Id)
			.Where(current => current.KeyName.ToLower() == keyName.ToLower())
			.AnyAsync();

		if (isKeyNameFound)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.KeyName);

			AddPageError(message: errorMessage);
		}
		// **************************************************

		// **************************************************
		var isCodeFound = false;

		if (BaseTable.Type == Domain.Features.Common.Enums.BaseTableTypeEnum.Enum)
		{
			isCodeFound =
				await
				DatabaseContext.BaseTableItems
				.Where(current => current.Id != ViewModel.Id)
				.Where(current => current.Code == ViewModel.Code)
				.Where(current => current.BaseTableId == BaseTable.Id)
				.AnyAsync();

			if (isCodeFound)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.Code);

				AddPageError(message: errorMessage);
			}
		}
		// **************************************************

		// **************************************************
		// این قسمت خیلی خاص است
		// **************************************************
		var title =
			ViewModel.Title.Fix()!;

		var isTitleFound = false;

		var specialLocalizedBaseTableItem =
			await
			DatabaseContext.LocalizedBaseTableItems

			.Where(current => current.CultureId == currentCulture.Id)
			.Where(current => current.BaseTableItemId == foundedItem.Id)

			.FirstOrDefaultAsync();

		if (specialLocalizedBaseTableItem is null)
		{
			isTitleFound =
				await
				DatabaseContext.LocalizedBaseTableItems

				.Where(current => current.CultureId == currentCulture.Id)
				.Where(current => current.BaseTableItem!.BaseTableId == BaseTable.Id)

				.Where(current => current.Title.ToLower() == title.ToLower())

				.AnyAsync();
		}
		else
		{
			isTitleFound =
				await
				DatabaseContext.LocalizedBaseTableItems

				.Where(current => current.Id != specialLocalizedBaseTableItem.Id)

				.Where(current => current.CultureId == currentCulture.Id)
				.Where(current => current.BaseTableItem!.BaseTableId == BaseTable.Id)

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

		if (isKeyNameFound || isCodeFound || isTitleFound)
		{
			return Page();
		}

		// **************************************************
		foundedItem.SetUpdateDateTime();

		foundedItem.KeyName = keyName;

		foundedItem.Code = ViewModel.Code;
		foundedItem.Ordering = ViewModel.Ordering;

		foundedItem.IsActive = ViewModel.IsActive;
		foundedItem.IsTestData = ViewModel.IsTestData;

		foundedItem.Icon = ViewModel.Icon;
		foundedItem.Color = ViewModel.Color;
		foundedItem.ImageUrl = ViewModel.ImageUrl;
		foundedItem.CoverImageUrl = ViewModel.CoverImageUrl;
		// **************************************************

		// **************************************************
		var localizedItem =
			await
			DatabaseContext.LocalizedBaseTableItems
			.Where(current => current.CultureId == currentCulture.Id)
			.Where(current => current.BaseTableItemId == foundedItem.Id)
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
				new Domain.Features.Common.LocalizedBaseTableItem
				(cultureId: currentCulture.Id, baseTableItemId: foundedItem.Id, title: title)
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
			arg0: Resources.DataDictionary.BaseTableItem);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			"Index", routeValues: new { foundedItem.BaseTableId });
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
