using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Posts;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Supervisor)]
public class IndexModel :
	Infrastructure.BasePageModelWithPagination
{
	#region Constructor
	public IndexModel(Persistence.DatabaseContext
		databaseContext) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Shared.PaginationAndtemsAndSearchViewModel
		<ViewModels.Pages.Features.Cms.Admin.Posts.IndexItemViewModel,
		ViewModels.Pages.Features.Cms.Admin.Posts.SearchViewModel> ViewModel
	{ get; set; }

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
			$"{nameof(Domain.Features.Cms.Post.UpdateDateTime)} {Dtat.Linq.Descending}";
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
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var result =
			DatabaseContext.Posts.AsQueryable();

		result = result
			.Where(current => current.Culture!.Lcid == currentUICultureLcid);

		#region Dynamic Search

		if (string.IsNullOrWhiteSpace(ViewModel.Search.Body) == false)
		{
			result = result
				.Where(current => current.Body != null &&
				current.Body.ToLower().Contains(ViewModel.Search.Body.ToLower()));
		}

		if (string.IsNullOrWhiteSpace(ViewModel.Search.Title) == false)
		{
			result = result
				.Where(current => current.Title
				.ToLower().Contains(ViewModel.Search.Title.ToLower()));
		}

		if (string.IsNullOrWhiteSpace(ViewModel.Search.Introduction) == false)
		{
			result = result
				.Where(current => current.Introduction != null && current
				.Introduction.ToLower().Contains(ViewModel.Search.Introduction.ToLower()));
		}

		if (string.IsNullOrWhiteSpace(ViewModel.Search.Description) == false)
		{
			result = result
				.Where(current => current.Description != null && current
					.Description.ToLower().Contains(ViewModel.Search.Description.ToLower()));
		}

		if (ViewModel.Search.TypeId.HasValue)
		{
			result = result
				.Where(current => current.TypeId == ViewModel.Search.TypeId);
		}

		if (ViewModel.Search.CategoryId.HasValue)
		{
			result = result
				.Where(current => current.CategoryId == ViewModel.Search.CategoryId);
		}

		if (ViewModel.Search.IsDraft.HasValue)
		{
			result = result
				.Where(current => current.IsDraft
					== ViewModel.Search.IsDraft.Value);
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

		#endregion /Dynamic Search

		ViewModel.Pagination.RecordCount =
			await
			result.CountAsync();

		result =
			result
			.Order(expression: ViewModel.Sorting.Expression!);

		//result =
		//	result
		//	.OrderByDescending(current => current.UpdateDateTime);

		var items =
			await
			result
			.Select(current => new ViewModels.Pages
				.Features.Cms.Admin.Posts.IndexItemViewModel
			{
				Id = current.Id,

				Title = current.Title,
				Author = current.Author,

				TypeId = current.TypeId,
				UserId = current.UserId,
				CategoryId = current.CategoryId,

				Hits = current.Hits,
				Score = current.Score,
				Ordering = current.Ordering,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
				DeleteDateTime = current.DeleteDateTime,

				IsDraft = current.IsDraft,
				IsActive = current.IsActive,
				IsDeleted = current.IsDeleted,
				IsFeatured = current.IsFeatured,
				IsCommentingEnabled = current.IsCommentingEnabled,

				TypeName = current.Type!.Name,
				CategoryName = current.Category!.Name,
				CommentCount = current.Comments.Count,
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
		ViewModel.Search.IsDraftSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsDraft);

		ViewModel.Search.IsActiveSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsActive);

		ViewModel.Search.IsDeletedSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsDeleted);

		ViewModel.Search.IsFeaturedSelectList = Infrastructure.SelectLists
			.GetBooleansForSearchAsync(selectedValue: ViewModel.Search.IsFeatured);

		ViewModel.Search.TypesSelectList =
			await
			Infrastructure.SelectLists.GetPostTypesForAdminAsync
			(databaseContext: DatabaseContext, selectedValue: ViewModel.Search.TypeId);

		ViewModel.Search.CategoriesSelectList =
			await
			Infrastructure.SelectLists.GetPostCategoriesForAdminAsync
			(databaseContext: DatabaseContext, selectedValue: ViewModel.Search.CategoryId);

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
			$"{nameof(Domain.Features.Cms.Post.UpdateDateTime)} {Dtat.Linq.Ascending}";

		expressionString =
			$"{Resources.DataDictionary.UpdateDateTime} ({Resources.DataDictionary.Ascending})";

		item = new ViewModels.Shared.IdNameViewModel
			<string?>(id: expression, keyName: expressionString);

		list.Add(item: item);
		// **************************************************

		// **************************************************
		expression =
			$"{nameof(Domain.Features.Cms.Post.UpdateDateTime)} {Dtat.Linq.Descending}";

		expressionString =
			$"{Resources.DataDictionary.UpdateDateTime} ({Resources.DataDictionary.Descending})";

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
