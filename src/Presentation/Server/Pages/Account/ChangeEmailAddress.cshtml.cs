using ViewModels.Pages.Account;

namespace Server.Pages.Account;

[Infrastructure.Security.CustomAuthorize]
public class ChangeEmailAddressModel : Infrastructure.BasePageModel
{
	public ChangeEmailAddressModel() : base()
	{
		ViewModel = new();
	}

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ChangeEmailAddressViewModel ViewModel { get; set; }

	public void OnGet()
	{
	}
}
