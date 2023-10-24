using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Constants.BaseTableItem;

namespace Server.Pages.Features.Identity.Admin.Users;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Supervisor)]
public class IndexModel :
	Infrastructure.BasePageModelWithPagination
{
	#region Constructor
	public IndexModel(Persistence.DatabaseContext databaseContext, Infrastructure.Security
		.AuthenticatedUserService authenticatedUserService) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
		AuthenticatedUserService = authenticatedUserService;
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Shared.PaginationAndtemsAndSearchViewModel
		<ViewModels.Pages.Features.Identity.Admin.Users.IndexItemViewModel,
		ViewModels.Pages.Features.Identity.Admin.Users.SearchViewModel> ViewModel
	{ get; set; }

	public Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; init; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public virtual async System.Threading.Tasks.Task OnGetAsync()
	{
		Initial();

		await SetItemsAsync();
		await SetSelectListsAsync();
	}
	#endregion /OnGetAsync()

	#region Initial()
	public override void Initial()
	{
		ViewModel.Pagination.PageIndex = 1;
		ViewModel.Pagination.PageSize = 10;

		ViewModel.Sorting.Expression =
			$"{nameof(Domain.Features.Identity.User.LastLoginDateTime)} {Dtat.Linq.Descending}";
	}
	#endregion /Initial()

	#region GotoFirst()
	public override void GotoFirst()
	{
		ViewModel.Pagination.PageIndex = 1;
	}
	#endregion /GotoFirst()

	#region GotoPrevious()
	public override void GotoPrevious()
	{
		if (ViewModel.Pagination.PageIndex > 1)
		{
			ViewModel.Pagination.PageIndex--;
		}
	}
	#endregion /GotoPrevious()

	#region GotoNext()
	public override void GotoNext()
	{
		if (ViewModel.Pagination.PageIndex < ViewModel.Pagination.PageCount)
		{
			ViewModel.Pagination.PageIndex++;
		}
	}
	#endregion /GotoNext()

	#region GotoLast()
	public override void GotoLast()
	{
		ViewModel.Pagination.PageIndex =
			ViewModel.Pagination.PageCount;
	}
	#endregion /GotoLast()

	#region SetItemsAsync()
	public override async System.Threading.Tasks.Task SetItemsAsync()
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
			return;
		}
		// **************************************************

		var result =
			DatabaseContext.Users.AsQueryable();

		result = result
			.Where(current => current.Role != null && current.Role.Code != null
			&& current.Role.Code <= (int)AuthenticatedUserService.RoleCode);

		#region Dynamic Search

		if (string.IsNullOrWhiteSpace(ViewModel.Search.Username) == false)
		{
			result = result
				.Where(current => current.Username != null &&
				current.Username.ToLower().Contains(ViewModel.Search.Username.ToLower()));
		}

		if (string.IsNullOrWhiteSpace(ViewModel.Search.EmailAddress) == false)
		{
			result = result
				.Where(current => current.EmailAddress
				.ToLower().Contains(ViewModel.Search.EmailAddress.ToLower()));
		}

		if (string.IsNullOrWhiteSpace(ViewModel.Search.NationalCode) == false)
		{
			result = result
				.Where(current => current.NationalCode != null && current
				.NationalCode.ToLower().Contains(ViewModel.Search.NationalCode.ToLower()));
		}

		if (string.IsNullOrWhiteSpace(ViewModel.Search.CellPhoneNumber) == false)
		{
			result = result
				.Where(current => current.CellPhoneNumber != null && current
					.CellPhoneNumber.ToLower().Contains(ViewModel.Search.CellPhoneNumber.ToLower()));
		}

		if (string.IsNullOrWhiteSpace(ViewModel.Search.AdminDescription) == false)
		{
			result = result
				.Where(current => current.AdminDescription != null && current
					.AdminDescription.ToLower().Contains(ViewModel.Search.AdminDescription.ToLower()));
		}

		if (ViewModel.Search.RoleId.HasValue)
		{
			result = result
				.Where(current => current.RoleId == ViewModel.Search.RoleId);
		}

		if (ViewModel.Search.GenderId.HasValue)
		{
			result = result
				.Where(current => current.GenderId == ViewModel.Search.GenderId);
		}

		if (ViewModel.Search.IsActive.HasValue)
		{
			result = result
				.Where(current => current.IsActive
					== ViewModel.Search.IsActive.Value);
		}

		if (ViewModel.Search.IsDeleted.HasValue)
		{
			result = result
				.Where(current => current.IsDeleted
					== ViewModel.Search.IsDeleted.Value);
		}

		if (ViewModel.Search.IsFeatured.HasValue)
		{
			result = result
				.Where(current => current.IsFeatured
					== ViewModel.Search.IsFeatured.Value);
		}

		if (ViewModel.Search.IsVerified.HasValue)
		{
			result = result
				.Where(current => current.IsVerified
					== ViewModel.Search.IsVerified.Value);
		}

		if (ViewModel.Search.IsEmailAddressVerified.HasValue)
		{
			result = result
				.Where(current => current.IsEmailAddressVerified
					== ViewModel.Search.IsEmailAddressVerified.Value);
		}

		if (ViewModel.Search.IsNationalCodeVerified.HasValue)
		{
			result = result
				.Where(current => current.IsNationalCodeVerified
					== ViewModel.Search.IsNationalCodeVerified.Value);
		}

		if (ViewModel.Search.IsVisibleInContactUsPage.HasValue)
		{
			result = result
				.Where(current => current.IsVisibleInContactUsPage
					== ViewModel.Search.IsVisibleInContactUsPage.Value);
		}

		if (ViewModel.Search.IsCellPhoneNumberVerified.HasValue)
		{
			result = result
				.Where(current => current.IsCellPhoneNumberVerified
					== ViewModel.Search.IsCellPhoneNumberVerified.Value);
		}

		#endregion /Dynamic Search

		ViewModel.Pagination.RecordCount =
			await
			result.CountAsync();

		result =
			result
			.Order(expression: ViewModel.Sorting.Expression!);

		//result =
		//	result
		//	.OrderByDescending(current => current.LastLoginDateTime);

		var items =
			await
			result
			.Select(current => new ViewModels.Pages
				.Features.Identity.Admin.Users.IndexItemViewModel
			{
				Id = current.Id,

				Ordering = current.Ordering,

				Username = current.Username,
				EmailAddress = current.EmailAddress,
				NationalCode = current.NationalCode,
				CellPhoneNumber = current.CellPhoneNumber,

				DeleteDateTime = current.DeleteDateTime,
				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
				LastLoginDateTime = current.LastLoginDateTime,

				IsActive = current.IsActive,
				IsDeleted = current.IsDeleted,
				IsFeatured = current.IsFeatured,
				IsVerified = current.IsVerified,
				IsUndeletable = current.IsUndeletable,
				IsEmailAddressVerified = current.IsEmailAddressVerified,
				IsNationalCodeVerified = current.IsNationalCodeVerified,
				IsVisibleInContactUsPage = current.IsVisibleInContactUsPage,
				IsCellPhoneNumberVerified = current.IsCellPhoneNumberVerified,

				RoleId = current.RoleId,
				GenderId = current.GenderId,

				PostCount =
					current.Posts.Count,

				CommentCount =
					current.PostComments.Count,

#pragma warning disable CS8602

				Hits =
					current.LocalizedUsers.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).Hits,

				LastName =
					current.LocalizedUsers.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).LastName,

				FirstName =
					current.LocalizedUsers.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).FirstName,

				RoleTitle =
					current.Role.LocalizedBaseTableItems.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).Title,

				GenderPrefix =
					current.Gender.LocalizedBaseTableItems.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).Title,

#pragma warning restore CS8602
			})

			.Skip(count: ViewModel.Pagination.Skip)
			.Take(count: ViewModel.Pagination.Take)

			.ToListAsync()
			;

		ViewModel.Items = items;
	}
	#endregion /SetItemsAsync()

	#region SetSelectListsAsync()
	public override async System.Threading.Tasks.Task SetSelectListsAsync()
	{
		ViewModel.Search.IsActiveSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsActive);

		ViewModel.Search.IsDeletedSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsDeleted);

		ViewModel.Search.IsFeaturedSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsFeatured);

		ViewModel.Search.IsVerifiedSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsVerified);

		ViewModel.Search.IsEmailAddressVerifiedSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsEmailAddressVerified);

		ViewModel.Search.IsNationalCodeVerifiedSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsNationalCodeVerified);

		ViewModel.Search.IsVisibleInContactUsPageSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsVisibleInContactUsPage);

		ViewModel.Search.IsCellPhoneNumberVerifiedSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsCellPhoneNumberVerified);

		ViewModel.Search.RolesSelectList =
			await
			Infrastructure.SelectLists.GetActiveItemsAsync
			(databaseContext: DatabaseContext, baseTableEnum:
			Domain.Features.Common.Enums.BaseTableEnum.Role,
			selectedValue: ViewModel.Search.RoleId,
			maxIncludeCode: (int)AuthenticatedUserService.RoleCode);

		ViewModel.Search.GendersSelectList =
			await
			Infrastructure.SelectLists.GetActiveItemsAsync
			(databaseContext: DatabaseContext, baseTableEnum:
			Domain.Features.Common.Enums.BaseTableEnum.Gender,
			selectedValue: ViewModel.Search.GenderId);

		// **************************************************
		// Sorting
		// **************************************************
		string expression;
		string expressionString;

		ViewModels.Shared.IdNameViewModel<string?> item;

		var list = new System.Collections.Generic
			.List<ViewModels.Shared.IdNameViewModel<string?>>();
		// **************************************************

		// **************************************************
		expression =
			$"{nameof(Domain.Features.Identity.User.LastLoginDateTime)} {Dtat.Linq.Ascending}";

		expressionString =
			$"{Resources.DataDictionary.LastLoginDateTime} ({Resources.DataDictionary.Ascending})";

		item = new ViewModels.Shared.IdNameViewModel
			<string?>(id: expression, keyName: expressionString);

		list.Add(item: item);
		// **************************************************

		// **************************************************
		expression =
			$"{nameof(Domain.Features.Identity.User.LastLoginDateTime)} {Dtat.Linq.Descending}";

		expressionString =
			$"{Resources.DataDictionary.LastLoginDateTime} ({Resources.DataDictionary.Descending})";

		item = new ViewModels.Shared.IdNameViewModel
			<string?>(id: expression, keyName: expressionString);

		list.Add(item: item);
		// **************************************************

		ViewModel.Sorting.SelectList =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list, selectedValue: ViewModel.Sorting.Expression,
			dataTextField: ViewModels.Shared.IdNameViewModel<int>.DataTextField,
			dataValueField: ViewModels.Shared.IdNameViewModel<int>.DataValueField);
		// **************************************************
		// /Sorting
		// **************************************************
	}
	#endregion /SetSelectListsAsync()

	#endregion /Methods
}
