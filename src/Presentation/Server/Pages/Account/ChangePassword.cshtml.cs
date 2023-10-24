using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

[Infrastructure.Security.CustomAuthorize]
public class ChangePasswordModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public ChangePasswordModel(Persistence
		.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.ChangePasswordViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (User is null ||
			User.Identity is null ||
			User.Identity.IsAuthenticated == false ||
			string.IsNullOrWhiteSpace(value: User.Identity.Name))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
		}

		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var userEmailAddress =
			User.Identity.Name.ToLower();

		var foundedUser =
			await
			DatabaseContext.Users

			.Where(current => current.EmailAddress.ToLower() == userEmailAddress.ToLower())

			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
		}
		// **************************************************

		// **************************************************
		var currentPassword =
			ViewModel.CurrentPassword.Fix();

		string? currentPasswordHash = null;

		if (currentPassword is not null)
		{
			currentPasswordHash =
				Dtat.Security.Hashing.GetSha256(value: currentPassword);
		}

		if (string.Compare(strA: foundedUser.Password,
			strB: currentPasswordHash, ignoreCase: false) != 0)
		{
			var errorMessage = string.Format(format:
				Resources.Messages.Errors.CurrentPasswordIsNotCorrect);

			AddPageError(message: errorMessage);

			return Page();
		}

		// **************************************************
		var newPassword =
			ViewModel.NewPassword.Fix()!;

		foundedUser
			.SetPassword(password: newPassword);

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = Resources
			.Messages.Successes.PasswordChanged;

		AddPageSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
