using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms;

public class PostsModel :
	Infrastructure.BasePageModelWithPagination
{
	#region Constructor
	public PostsModel(Services.Features.Cms.PostsService postsService,
		Persistence.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
		PostsService = postsService;
	}
	#endregion /Constructor

	#region Properties

	private Services.Features.Cms.PostsService PostsService { get; init; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Shared.PaginationAndtemsAndSearchViewModel
		<ViewModels.Pages.Features.Cms.PartialViews.DisplayPostCardViewModel,
		ViewModels.Pages.Features.Cms.SearchPostViewModel> ViewModel
	{ get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async
		System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc
			.IActionResult> OnGetAsync(string? culture = null, string? keywords = null)
	{
		// **************************************************
		culture = culture.Fix();

		if (culture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		culture = culture.Replace
			(oldValue: " ", newValue: string.Empty);
		// **************************************************

		// **************************************************
		var foundedCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.CultureName.ToLower() == culture.ToLower())
			.FirstOrDefaultAsync();

		if (foundedCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		ViewModel.Search.Body = keywords;
		// **************************************************

		Initial();

		await SetItemsAsync();
		await SetSelectListsAsync();

		return Page();
	}

	#endregion /OnGetAsync()
	#region Initial()
	public override void Initial()
	{
		ViewModel.Pagination.PageIndex = 1;
		ViewModel.Pagination.PageSize = 16;
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
			PostsService.GetByCulture();

		#region Dynamic Search

		if (string.IsNullOrWhiteSpace(ViewModel.Search.Body) == false)
		{
			var keywordsArray =
				ViewModel.Search.Body.Split(separator: ' ')
				.Distinct();

			foreach (var keyword in keywordsArray)
			{
				result =
					result
					.Where(current => current.Body != null
						&& current.Body.ToLower().Contains(keyword));
			}
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

		#endregion /Dynamic Search

		ViewModel.Pagination.RecordCount =
			await
			result.CountAsync();

		result = PostsService
			.OrderBy(request: result);

		var items =
			await
			result
			.Select(current => new ViewModels.Pages
				.Features.Cms.PartialViews.DisplayPostCardViewModel
			{
				Id = current.Id,

				Hits = current.Hits,
				IsVerified = current.IsVerified,

				Title = current.Title,
				ImageUrl = current.ImageUrl,
				Description = current.Description,

				Username = current.User!.Username,

				DisplayAuthor = true,
				Author = current.Author,

				DisplayTypeLink = true,
				TypeName = current.Type!.Name,
				TypeTitle = current.Type!.Title,

				DisplayCategoryLink = true,
				CategoryName = current.Category!.Name,
				CategoryTitle = current.Category!.Title,

				UpdateDateTime = current.UpdateDateTime,
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
		ViewModel.Pagination.PageSizes =
			new int[] { 4, 8, 12, 16, 32 };

		ViewModel.Search.TypesSelectList =
			await
			Infrastructure.SelectLists.GetPostTypesForUserAsync
			(databaseContext: DatabaseContext, selectedValue: ViewModel.Search.TypeId);

		ViewModel.Search.CategoriesSelectList =
			await
			Infrastructure.SelectLists.GetPostCategoriesForUserAsync
			(databaseContext: DatabaseContext, selectedValue: ViewModel.Search.CategoryId);
	}
	#endregion /SetSelectListsAsync()

	#endregion /Methods
}
