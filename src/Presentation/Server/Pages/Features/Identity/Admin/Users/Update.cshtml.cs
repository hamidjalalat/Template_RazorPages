using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.Users;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Supervisor)]
public class UpdateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateModel(Persistence.DatabaseContext databaseContext, Infrastructure.Security
		.AuthenticatedUserService authenticatedUserService) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
		AuthenticatedUserService = authenticatedUserService;
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Identity.Admin.Users.UpdateViewModel ViewModel { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? RolesSelectList { get; set; }
	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? GendersSelectList { get; set; }
	public Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? id)
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

		// **************************************************
		// **************************************************
		// **************************************************
		if (id is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}
		// **************************************************

		// **************************************************
		var foundedUser =
			await
			DatabaseContext.Users
			.Where(current => current.Id == id.Value)
			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedUser.RoleId.HasValue)
		{
			var role =
				await
				Services.Features.Identity.RoleService.GetByIdAsync
				(databaseContext: DatabaseContext, id: foundedUser.RoleId);

			if (role is null)
			{
				foundedUser.RoleId = null;

				await DatabaseContext.SaveChangesAsync();

				return RedirectToPage(pageName:
					Constants.CommonRouting.BadRequest);
			}

			if (role.Code > (int)AuthenticatedUserService.RoleCode)
			{
				return RedirectToPage(pageName:
					Constants.CommonRouting.Forbidden);
			}
		}

		var foundedLocalizedUser =
			await
			DatabaseContext.LocalizedUsers
			.Where(current => current.UserId == foundedUser.Id)
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefaultAsync();
		// **************************************************

		// **************************************************
		ViewModel.Id = foundedUser.Id;
		ViewModel.RoleId = foundedUser.RoleId;
		ViewModel.GenderId = foundedUser.GenderId;

		ViewModel.Score = foundedUser.Score;
		ViewModel.Ordering = foundedUser.Ordering;

		ViewModel.Username = foundedUser.Username;
		ViewModel.EmailAddress = foundedUser.EmailAddress;
		ViewModel.NationalCode = foundedUser.NationalCode;
		ViewModel.CellPhoneNumber = foundedUser.CellPhoneNumber;
		ViewModel.AdminDescription = foundedUser.AdminDescription;

		ViewModel.IsActive = foundedUser.IsActive;
		ViewModel.IsDeleted = foundedUser.IsDeleted;
		ViewModel.IsFeatured = foundedUser.IsFeatured;
		ViewModel.IsVerified = foundedUser.IsVerified;
		ViewModel.IsProfilePublic = foundedUser.IsProfilePublic;
		ViewModel.IsEmailAddressVerified = foundedUser.IsEmailAddressVerified;
		ViewModel.IsNationalCodeVerified = foundedUser.IsNationalCodeVerified;
		ViewModel.IsVisibleInContactUsPage = foundedUser.IsVisibleInContactUsPage;
		ViewModel.IsCellPhoneNumberVerified = foundedUser.IsCellPhoneNumberVerified;

		if (foundedLocalizedUser is not null)
		{
			ViewModel.Hits = foundedLocalizedUser.Hits;
			ViewModel.LastName = foundedLocalizedUser.LastName;
			ViewModel.FirstName = foundedLocalizedUser.FirstName;
			ViewModel.TitleInContactUsPage = foundedLocalizedUser.TitleInContactUsPage;
		}
		// **************************************************

		await UpdateSelectListsAsync
			(roleId: ViewModel.RoleId, genderId: ViewModel.GenderId);

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
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
		var foundedUser =
			await
			DatabaseContext.Users
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var emailAddress =
			ViewModel.EmailAddress.Fix()!.ToLower();

		var isEmailAddressFound =
			await
			DatabaseContext.Users
			.Where(current => current.Id != ViewModel.Id)
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
				.Where(current => current.Id != ViewModel.Id)
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
				.Where(current => current.Id != ViewModel.Id)
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
				.Where(current => current.Id != ViewModel.Id)
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

		// **************************************************
		if (isUsernameFound || isEmailAddressFound ||
			isCellPhoneNumberFound || isNationalCodeFound)
		{
			await UpdateSelectListsAsync
				(roleId: ViewModel.RoleId, genderId: ViewModel.GenderId);

			return Page();
		}
		// **************************************************

		// **************************************************
		foundedUser.SetUpdateDateTime();

		foundedUser.Username = username;

		foundedUser.Score = ViewModel.Score;
		foundedUser.Ordering = ViewModel.Ordering;

		foundedUser.RoleId = ViewModel.RoleId;
		foundedUser.GenderId = ViewModel.GenderId;

		foundedUser.NationalCode = nationalCode;
		foundedUser.CellPhoneNumber = cellPhoneNumber;

		foundedUser.IsActive = ViewModel.IsActive;
		foundedUser.IsFeatured = ViewModel.IsFeatured;
		foundedUser.IsVerified = ViewModel.IsVerified;
		foundedUser.IsProfilePublic = ViewModel.IsProfilePublic;
		foundedUser.IsEmailAddressVerified = ViewModel.IsEmailAddressVerified;
		foundedUser.IsNationalCodeVerified = ViewModel.IsNationalCodeVerified;
		foundedUser.IsVisibleInContactUsPage = ViewModel.IsVisibleInContactUsPage;
		foundedUser.IsCellPhoneNumberVerified = ViewModel.IsCellPhoneNumberVerified;

		foundedUser.AdminDescription = ViewModel.AdminDescription.Fix();

		if (ViewModel.IsDeleted)
		{
			foundedUser.Delete();
		}
		else
		{
			foundedUser.Undelete();
		}
		// **************************************************

		// **************************************************
		var localizedItem =
			await
			DatabaseContext.LocalizedUsers
			.Where(current => current.UserId == foundedUser.Id)
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefaultAsync();

		if (localizedItem is not null)
		{
			localizedItem.SetUpdateDateTime();

			localizedItem.Hits = ViewModel.Hits;

			localizedItem.TitleInContactUsPage =
				ViewModel.TitleInContactUsPage.Fix();

			localizedItem.LastName = ViewModel.LastName.Fix()!;
			localizedItem.FirstName = ViewModel.FirstName.Fix()!;
		}
		else
		{
			localizedItem =
				new Domain.Features.Identity.LocalizedUser
				(cultureId: currentCulture.Id, userId: foundedUser.Id,
				firstName: ViewModel.FirstName.Fix()!, ViewModel.LastName.Fix()!)
				{
					Hits = ViewModel.Hits,

					TitleInContactUsPage =
						ViewModel.TitleInContactUsPage.Fix(),
				};

			await DatabaseContext.AddAsync(entity: localizedItem);
		}
		// **************************************************

		await DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.Updated,
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
