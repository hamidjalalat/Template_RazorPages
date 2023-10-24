namespace Infrastructure;

public interface IBasePageModelWithPagination
{
	void Initial();
	void GotoLast();
	void GotoNext();
	void GotoFirst();
	void GotoPrevious();

	System.Threading.Tasks.Task SetItemsAsync();
	System.Threading.Tasks.Task SetSelectListsAsync();

	System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> OnPostGotoFirstAsync();
	System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> OnPostGotoPreviousAsync();
	System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> OnPostGotoNextAsync();
	System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> OnPostGotoLastAsync();
}
