using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.Users;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Supervisor)]
public class CreateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public CreateModel
		(Persistence.DatabaseContext databaseContext,
		Services.Features.Common.HttpContextService httpContextService,
		Infrastructure.Security.AuthenticatedUserService authenticatedUserService) : base(databaseContext: databaseContext)
	{
		ViewModel = new();

		HttpContextService = httpContextService;
		AuthenticatedUserService = authenticatedUserService;
	}
	#endregion /Constructor

	#region Properties

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? RolesSelectList { get; set; }
	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? GendersSelectList { get; set; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Identity.Admin.Users.CreateViewModel ViewModel { get; set; }

	private Services.Features.Common.HttpContextService HttpContextService { get; }
	private Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
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
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		await UpdateSelectListsAsync(roleId: null, genderId: null);

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		// **************************************************
		var remoteIP =
			HttpContextService.GetRemoteIpAddress();

		if (string.IsNullOrWhiteSpace(value: remoteIP))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}
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
		if (ModelState.IsValid == false)
		{
			await UpdateSelectListsAsync
				(roleId: ViewModel.RoleId, genderId: ViewModel.GenderId);

			return Page();
		}
		// **************************************************

		// **************************************************
		// مهم
		// **************************************************
		var role =
			await
			Services.Features.Identity.RoleService.GetByIdAsync
			(databaseContext: DatabaseContext, id: ViewModel.RoleId);

		if (role is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		if (role.Code > (int)AuthenticatedUserService.RoleCode)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Forbidden);
		}
		// **************************************************

		// **************************************************
		var emailAddress =
			ViewModel.EmailAddress.Fix()!.ToLower();

		var isEmailAddressFound =
			await
			DatabaseContext.Users
			.Where(current => current.EmailAddress.ToLower() == emailAddress)
			.AnyAsync();

		if (isEmailAddressFound)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.EmailAddress);

			AddPageError(message: errorMessage);
		}
		// **************************************************

		// **************************************************
		var username =
			ViewModel.Username.Fix();

		var isUsernameFound = false;

		if (username is not null)
		{
			isUsernameFound =
				await
				DatabaseContext.Users
				.Where(current => current.Username != null
					&& current.Username.ToLower() == username.ToLower())
				.AnyAsync();

			if (isUsernameFound)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.Username);

				AddPageError(message: errorMessage);
			}

			username =
				username.ToLower();
		}
		// **************************************************

		// **************************************************
		var cellPhoneNumber =
			ViewModel.CellPhoneNumber.Fix();

		var isCellPhoneNumberFound = false;

		if (cellPhoneNumber is not null)
		{
			isCellPhoneNumberFound =
				await
				DatabaseContext.Users
				.Where(current => current.CellPhoneNumber != null
					&& current.CellPhoneNumber.ToLower() == cellPhoneNumber)
				.AnyAsync();

			if (isCellPhoneNumberFound)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.CellPhoneNumber);

				AddPageError(message: errorMessage);
			}
		}
		// **************************************************

		// **************************************************
		var nationalCode =
			ViewModel.NationalCode.Fix();

		var isNationalCodeFound = false;

		if (nationalCode is not null)
		{
			isNationalCodeFound =
				await
				DatabaseContext.Users
				.Where(current => current.NationalCode != null
					&& current.NationalCode.ToLower() == nationalCode)
				.AnyAsync();

			if (isNationalCodeFound)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.NationalCode);

				AddPageError(message: errorMessage);
			}
		}
		// **************************************************

		if (isUsernameFound || isEmailAddressFound ||
			isCellPhoneNumberFound || isNationalCodeFound)
		{
			await UpdateSelectListsAsync
				(roleId: ViewModel.RoleId, genderId: ViewModel.GenderId);

			return Page();
		}

		// **************************************************
		var roleId = ViewModel.RoleId!.Value;

		var user =
			new Domain.Features.Identity.User
			(emailAddress: emailAddress, registerIP: remoteIP, registerType:
			Domain.Features.Identity.Enums.AuthenticationTypeEnum.Internal)
			{
				RoleId = roleId,

				Score = ViewModel.Score,
				Ordering = ViewModel.Ordering,

				Username = username,
				NationalCode = nationalCode,
				CellPhoneNumber = cellPhoneNumber,
				AdminDescription = ViewModel.AdminDescription.Fix(),

				IsActive = ViewModel.IsActive,
				IsFeatured = ViewModel.IsFeatured,
				IsVerified = ViewModel.IsVerified,
				IsProfilePublic = ViewModel.IsProfilePublic,
				IsEmailAddressVerified = ViewModel.IsEmailAddressVerified,
				IsNationalCodeVerified = ViewModel.IsNationalCodeVerified,
				IsVisibleInContactUsPage = ViewModel.IsVisibleInContactUsPage,
				IsCellPhoneNumberVerified = ViewModel.IsCellPhoneNumberVerified,
			};

		var password =
			ViewModel.Password.Fix()!;

		user.SetPassword(password: password);

		if (ViewModel.IsDeleted)
		{
			user.Delete();
		}
		else
		{
			user.Undelete();
		}

		await DatabaseContext.AddAsync(entity: user);

		var localizedUser =
			new Domain.Features.Identity.LocalizedUser
			(cultureId: currentCulture.Id, userId: user.Id,
			firstName: ViewModel.FirstName.Fix()!, lastName: ViewModel.LastName.Fix()!)
			{
				Hits = ViewModel.Hits,
				TitleInContactUsPage =
					ViewModel.TitleInContactUsPage.Fix(),
			};

		await DatabaseContext.AddAsync(entity: localizedUser);

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.Created,
			Resources.DataDictionary.User);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#region UpdateSelectListsAsync()
	private async System.Threading.Tasks.Task UpdateSelectListsAsync(
		System.Guid? roleId,
		System.Guid? genderId)
	{
		GendersSelectList =
			await
			Infrastructure.SelectLists.GetActiveItemsAsync
			(databaseContext: DatabaseContext, baseTableEnum:
			Domain.Features.Common.Enums.BaseTableEnum.Gender,
			selectedValue: genderId);

		RolesSelectList =
			await
			Infrastructure.SelectLists.GetActiveItemsAsync
			(databaseContext: DatabaseContext, baseTableEnum:
			Domain.Features.Common.Enums.BaseTableEnum.Role,
			selectedValue: roleId, maxIncludeCode: (int)AuthenticatedUserService.RoleCode);
	}
	#endregion /UpdateSelectListsAsync()

	#endregion /Methods
}
