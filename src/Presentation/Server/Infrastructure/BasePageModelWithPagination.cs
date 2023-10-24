namespace Infrastructure;

public abstract class BasePageModelWithPagination :
	BasePageModelWithDatabaseContext, IBasePageModelWithPagination
{
	#region Constructor
	public BasePageModelWithPagination(Persistence
		.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
	}
	#endregion /Constructor

	#region Properties
	#endregion /Properties

	#region Methods

	#region OnPostGotoFirstAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostGotoFirstAsync()
	{
		if (ModelState.IsValid == false)
		{
			return RedirectToPage
				(pageName: Constants.CommonRouting.BadRequest);
		}

		GotoFirst();

		await SetItemsAsync();
		await SetSelectListsAsync();

		ModelState.Clear();

		return Page();
	}
	#endregion /OnPostGotoFirstAsync()

	#region OnPostGotoPreviousAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostGotoPreviousAsync()
	{
		if (ModelState.IsValid == false)
		{
			return RedirectToPage
				(pageName: Constants.CommonRouting.BadRequest);
		}

		GotoPrevious();

		await SetItemsAsync();
		await SetSelectListsAsync();

		ModelState.Clear();

		return Page();
	}
	#endregion /OnPostGotoPreviousAsync(

	#region OnPostGotoNextAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostGotoNextAsync()
	{
		if (ModelState.IsValid == false)
		{
			return RedirectToPage
				(pageName: Constants.CommonRouting.BadRequest);
		}

		GotoNext();

		await SetItemsAsync();
		await SetSelectListsAsync();

		ModelState.Clear();

		return Page();
	}
	#endregion /OnPostGotoNextAsync()

	#region OnPostGotoLastAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostGotoLastAsync()
	{
		if (ModelState.IsValid == false)
		{
			return RedirectToPage
				(pageName: Constants.CommonRouting.BadRequest);
		}

		GotoLast();

		await SetItemsAsync();
		await SetSelectListsAsync();

		ModelState.Clear();

		return Page();
	}
	#endregion /OnPostGotoLastAsync()

	#region Initial()
	public abstract void Initial();
	#endregion /Initial()

	#region GotoLast()
	public abstract void GotoLast();
	#endregion /GotoLast()

	#region GotoNext()
	public abstract void GotoNext();
	#endregion /GotoNext()

	#region GotoFirst()
	public abstract void GotoFirst();
	#endregion /GotoFirst()

	#region GotoPrevious()
	public abstract void GotoPrevious();
	#endregion /GotoPrevious()

	#region SetItemsAsync()
	public abstract System.Threading.Tasks.Task SetItemsAsync();
	#endregion /SetItemsAsync()

	#region SetSelectListsAsync()
	public abstract System.Threading.Tasks.Task SetSelectListsAsync();
	#endregion /SetSelectListsAsync()

	#endregion /Methods
}
