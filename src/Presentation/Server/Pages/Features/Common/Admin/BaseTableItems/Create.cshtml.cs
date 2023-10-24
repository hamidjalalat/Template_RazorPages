using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Common.Admin.BaseTableItems;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Administrator)]
public class CreateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public CreateModel(Persistence.DatabaseContext
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
	public ViewModels.Pages.Features.Common.Admin.BaseTableItems.CreateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync(System.Guid? baseTableId)
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? baseTableId)
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
		if (baseTableId is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var baseTable =
			await
			DatabaseContext.BaseTables
			.Include(current => current.LocalizedBaseTables)
			.Where(current => current.Id == baseTableId.Value)
			.FirstOrDefaultAsync();

		if (baseTable is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		BaseTable = baseTable;
		// **************************************************

		// **************************************************
		BaseTableTitle =
			BaseTable.Code.ToString();

		var localizedBaseTable =
			baseTable.LocalizedBaseTables
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

		return Page();
	}
	#endregion /OnGetAsync(System.Guid? baseTableId)

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync(System.Guid? baseTableId)
	{
		// **************************************************
		if (baseTableId is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var baseTable =
			await
			DatabaseContext.BaseTables
			.Include(current => current.LocalizedBaseTables)
			.Where(current => current.Id == baseTableId.Value)
			.FirstOrDefaultAsync();

		if (baseTable is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		BaseTable = baseTable;
		// **************************************************

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
		BaseTableTitle =
			BaseTable.Code.ToString();

		var localizedBaseTable =
			baseTable.LocalizedBaseTables
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
		var title =
			ViewModel.Title.Fix()!;

		var isTitleFound =
				await
				DatabaseContext.LocalizedBaseTableItems

				.Where(current => current.CultureId == currentCulture.Id)
				.Where(current => current.BaseTableItem!.BaseTableId == BaseTable.Id)

				.Where(current => current.Title.ToLower() == title.ToLower())

				.AnyAsync();

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

		var newEntity =
			new Domain.Features.Common.BaseTableItem
			(baseTableId: BaseTable.Id, keyName: keyName, code: ViewModel.Code)
			{
				//Id
				//Code
				//KeyName
				//BaseTable
				//BaseTableId
				//InsertDateTime
				//UpdateDateTime

				Ordering = ViewModel.Ordering,

				IsActive = ViewModel.IsActive,
				IsTestData = ViewModel.IsTestData,

				Icon = ViewModel.Icon.Fix(),
				Color = ViewModel.Color.Fix(),
				ImageUrl = ViewModel.ImageUrl.Fix(),
				CoverImageUrl = ViewModel.CoverImageUrl.Fix(),
			};

		var localizedBaseTableItem =
			new Domain.Features.Common
			.LocalizedBaseTableItem(cultureId: currentCulture.Id,
			baseTableItemId: new System.Guid(), title: title)
			{
				//Id
				//Title
				//Culture
				//CultureId
				//BaseTableItem
				//InsertDateTime
				//UpdateDateTime
				//BaseTableItemId
				Description = ViewModel.Description.Fix(),
			};

		newEntity.LocalizedBaseTableItems.Add(item: localizedBaseTableItem);

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
			Resources.DataDictionary.BaseTableItem);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			"Index", routeValues: new { baseTableId });
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
