using ViewModels.Pages.Account;

namespace Server.Pages.Account;

[Infrastructure.Security.CustomAuthorize]
public class ChangeCellPhoneNumberModel : Infrastructure.BasePageModel
{
	public ChangeCellPhoneNumberModel() : base()
	{
		ViewModel = new();
	}

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ChangeCellPhoneNumberViewModel ViewModel { get; set; }

	public void OnGet()
	{
	}
}
