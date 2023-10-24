using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Common.Admin.BaseTableItems;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Supervisor)]
public class IndexModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public IndexModel(Persistence.DatabaseContext
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
	public System.Collections.Generic.List<ViewModels.Pages.Features.Common.Admin.BaseTableItems.IndexItemViewModel> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? baseTableId)
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
			var title = localizedBaseTable.Title;

			if (string.IsNullOrWhiteSpace(value: title) == false)
			{
				BaseTableTitle = title;
			}
		}
		// **************************************************

		ViewModel =
			await
			DatabaseContext.BaseTableItems

			.Where(current => current.BaseTableId == baseTableId)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Code)

			.Select(current => new ViewModels.Pages
				.Features.Common.Admin.BaseTableItems.IndexItemViewModel
			{
				Id = current.Id,

				Color = current.Color,
				Icon = current.Icon,
				KeyName = current.KeyName,

				IsActive = current.IsActive,
				IsTestData = current.IsTestData,

				Code = current.Code,
				Ordering = current.Ordering,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

				ItemCount =
					current.Users_Role.Count +
					current.Users_Gender.Count +
					current.Users_Religion.Count +
					current.Users_MaritalStatus.Count +
					current.Users_LastEducationDegree.Count +
					current.Users_MilitaryServiceStatus.Count
					,

#pragma warning disable CS8602

				Title =
					(current.LocalizedBaseTableItems == null
					||
					current.LocalizedBaseTableItems.Count == 0
					)
					?
					null
					:
					current.LocalizedBaseTableItems.FirstOrDefault
						(other => other.CultureId == currentCulture.Id).Title,

#pragma warning restore CS8602
			})
			.ToListAsync()
			;

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
