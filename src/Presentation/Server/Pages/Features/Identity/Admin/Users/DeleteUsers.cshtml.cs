using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.Users;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Supervisor)]
public class DeleteUsersModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public DeleteUsersModel(Persistence.DatabaseContext
		databaseContext) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Identity.Admin.Users.DeleteUsersViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public void OnGetAsync()
	{
		ViewModel.Value = 1;

		ViewModel.TimeUnit = Domain
			.Features.Common.Enums.TimeUnitEnum.Day;
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		// **************************************************
		if (ModelState.IsValid == false)
		{
			return Page();
		}
		// **************************************************

		// **************************************************
		var seconds = ViewModel.Value;

		switch(ViewModel.TimeUnit)
		{
			case Domain.Features.Common.Enums.TimeUnitEnum.Second:
			{
				seconds *= 1;

				break;
			}

			case Domain.Features.Common.Enums.TimeUnitEnum.Minute:
			{
				seconds *= 60;

				break;
			}

			case Domain.Features.Common.Enums.TimeUnitEnum.Hour:
			{
				seconds *= 60 * 60;

				break;
			}

			case Domain.Features.Common.Enums.TimeUnitEnum.Day:
			{
				seconds *= 60 * 60 * 24;

				break;
			}

			case Domain.Features.Common.Enums.TimeUnitEnum.Week:
			{
				seconds *= 60 * 60 * 24 * 7;

				break;
			}

			case Domain.Features.Common.Enums.TimeUnitEnum.Month:
			{
				seconds *= 60 * 60 * 30;

				break;
			}

			case Domain.Features.Common.Enums.TimeUnitEnum.Year:
			{
				seconds *= 60 * 60 * 24 * 365;

				break;
			}
		}

		var dateTime =
			Dtat.DateTime.Now.AddSeconds(seconds: -seconds);

		await
		DatabaseContext.LocalizedUsers
		.Where(current => current.User!.InsertDateTime < dateTime)
		.Where(current => current.User!.IsEmailAddressVerified == false)
		.ExecuteDeleteAsync();

		var affectedRows =
			await
			DatabaseContext.Users
			.Where(current => current.InsertDateTime < dateTime)
			.Where(current => current.IsEmailAddressVerified == false)
			.ExecuteDeleteAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.UserCountDeleted,
			affectedRows.ConvertDigitsToUnicode());

		AddPageSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
