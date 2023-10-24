using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

[Infrastructure.Security.CustomAuthorize
	(Domain.Features.Identity.Enums.RoleEnum.SimpleUser)]

//[Infrastructure.Security.CustomAuthorize(minRole:
//	nameof(Domain.Features.Identity.Enums.RoleEnum.SimpleUser))]
public class UpdateProfileModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateProfileModel(Persistence.DatabaseContext
		databaseContext) : base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.UpdateProfileViewModel ViewModel { get; set; }
	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? GendersSelectList { get; set; }
	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? ReligionsSelectList { get; set; }
	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? MaritalStatusesSelectList { get; set; }
	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? EducationDegreesSelectList { get; set; }
	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? MilitaryServcieStatusesSelectList { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		if (User is null ||
			User.Identity is null ||
			User.Identity.IsAuthenticated == false ||
			string.IsNullOrWhiteSpace(value: User.Identity.Name))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
		}

		var userEmailAddress = User.Identity.Name;

		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var foundedUser =
			await
			DatabaseContext.Users
			.Include(current => current.LocalizedUsers)
			.ThenInclude(current => current.Culture)
			.Where(current => current.EmailAddress.ToLower() == userEmailAddress.ToLower())
			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		await UpdateSelectListsAsync(
			genderId: foundedUser.GenderId,
			religionId: foundedUser.ReligionId,
			maritalStatusId: foundedUser.MaritalStatusId,
			lastEducationDegreeId: foundedUser.LastEducationDegreeId,
			militaryServcieStatusId: foundedUser.MilitaryServiceStatusId);

		ViewModel.GenderId = foundedUser.GenderId;
		ViewModel.ReligionId = foundedUser.ReligionId;
		ViewModel.MaritalStatusId = foundedUser.MaritalStatusId;
		ViewModel.LastEducationDegreeId = foundedUser.LastEducationDegreeId;
		ViewModel.MilitaryServcieStatusId = foundedUser.MilitaryServiceStatusId;

		ViewModel.ImageUrl = foundedUser.ImageUrl;
		ViewModel.CoverImageUrl = foundedUser.CoverImageUrl;

		ViewModel.Username = foundedUser.Username;
		ViewModel.NationalCode = foundedUser.NationalCode;
		ViewModel.IsProfilePublic = foundedUser.IsProfilePublic;
		ViewModel.CellPhoneNumber = foundedUser.CellPhoneNumber;

		var foundedLocalizedUser =
			foundedUser.LocalizedUsers
			.Where(current => current.Culture is not null &&
				current.Culture.Lcid == currentUICultureLcid)
			.FirstOrDefault();

		if (foundedLocalizedUser is null)
		{
			return Page();
		}

		ViewModel.LastName = foundedLocalizedUser.LastName;
		ViewModel.FirstName = foundedLocalizedUser.FirstName;
		ViewModel.Description = foundedLocalizedUser.Description;

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
			await UpdateSelectListsAsync(
				genderId: ViewModel.GenderId,
				religionId: ViewModel.ReligionId,
				maritalStatusId: ViewModel.MaritalStatusId,
				lastEducationDegreeId: ViewModel.LastEducationDegreeId,
				militaryServcieStatusId: ViewModel.MilitaryServcieStatusId);

			return Page();
		}
		// **************************************************

		// **************************************************
		var userEmailAddress =
			User!.Identity!.Name!.ToLower();

		var foundedUser =
			await
			DatabaseContext.Users

			.Include(current => current.LocalizedUsers)
			.ThenInclude(current => current.Culture)

			.Where(current => current.EmailAddress.ToLower() == userEmailAddress)

			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
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

				.Where(current => current.Id != foundedUser.Id)

				.Where(current => current.Username != null
					&& current.Username.ToLower() == username.ToLower())

				.AnyAsync()
				;

			if (isUsernameFound)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.Username);

				AddPageError(message: errorMessage);
			}

			username = username.ToLower();
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

				.Where(current => current.Id != foundedUser.Id)

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

				.Where(current => current.Id != foundedUser.Id)

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
		if (isUsernameFound || isCellPhoneNumberFound || isNationalCodeFound)
		{
			await UpdateSelectListsAsync(
				genderId: ViewModel.GenderId,
				religionId: ViewModel.ReligionId,
				maritalStatusId: ViewModel.MaritalStatusId,
				lastEducationDegreeId: ViewModel.LastEducationDegreeId,
				militaryServcieStatusId: ViewModel.MilitaryServcieStatusId);

			return Page();
		}
		// **************************************************

		// **************************************************
		foundedUser.SetUpdateDateTime();

		foundedUser.Username = username;
		foundedUser.NationalCode = nationalCode;
		foundedUser.CellPhoneNumber = cellPhoneNumber;

		foundedUser.IsProfilePublic = ViewModel.IsProfilePublic;

		foundedUser.ImageUrl = ViewModel.ImageUrl.Fix();
		foundedUser.CoverImageUrl = ViewModel.CoverImageUrl.Fix();

		foundedUser.GenderId = ViewModel.GenderId;
		foundedUser.ReligionId = ViewModel.ReligionId;
		foundedUser.MaritalStatusId = ViewModel.MaritalStatusId;
		foundedUser.LastEducationDegreeId = ViewModel.LastEducationDegreeId;
		foundedUser.MilitaryServiceStatusId = ViewModel.MilitaryServcieStatusId;
		// **************************************************

		// **************************************************
		var foundedLocalizedUser =
			foundedUser.LocalizedUsers
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefault();

		if (foundedLocalizedUser is not null)
		{
			foundedLocalizedUser.LastName = ViewModel.LastName.Fix()!;
			foundedLocalizedUser.FirstName = ViewModel.FirstName.Fix()!;
			foundedLocalizedUser.Description = ViewModel.Description.Fix();
		}
		else
		{
			var localizedUser =
				new Domain.Features.Identity.LocalizedUser
				(cultureId: currentCulture.Id, userId: foundedUser.Id,
				firstName: ViewModel.FirstName.Fix()!, lastName: ViewModel.LastName.Fix()!)
				{
					Description = ViewModel.Description.Fix(),
				};

			await DatabaseContext.AddAsync(entity: localizedUser);
		}

		await DatabaseContext.SaveChangesAsync();

		await UpdateSelectListsAsync(
			genderId: ViewModel.GenderId,
			religionId: ViewModel.ReligionId,
			maritalStatusId: ViewModel.MaritalStatusId,
			lastEducationDegreeId: ViewModel.LastEducationDegreeId,
			militaryServcieStatusId: ViewModel.MilitaryServcieStatusId);

		// **************************************************
		var successMessage =
			Resources.Messages.Successes.UpdateProfile;

		AddPageSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#region UpdateSelectListsAsync()
	private async System.Threading.Tasks.Task UpdateSelectListsAsync(
		System.Guid? genderId,
		System.Guid? religionId,
		System.Guid? maritalStatusId,
		System.Guid? lastEducationDegreeId,
		System.Guid? militaryServcieStatusId)
	{
		GendersSelectList =
			await
			Infrastructure.SelectLists.GetActiveItemsAsync
			(databaseContext: DatabaseContext, baseTableEnum:
			Domain.Features.Common.Enums.BaseTableEnum.Gender,
			selectedValue: genderId);

		ReligionsSelectList =
			await
			Infrastructure.SelectLists.GetActiveItemsAsync
			(databaseContext: DatabaseContext, baseTableEnum:
			Domain.Features.Common.Enums.BaseTableEnum.Religion,
			selectedValue: religionId);

		MaritalStatusesSelectList =
			await
			Infrastructure.SelectLists.GetActiveItemsAsync
			(databaseContext: DatabaseContext, baseTableEnum:
			Domain.Features.Common.Enums.BaseTableEnum.MaritalStatus,
			selectedValue: maritalStatusId);

		EducationDegreesSelectList =
			await
			Infrastructure.SelectLists.GetActiveItemsAsync
			(databaseContext: DatabaseContext, baseTableEnum:
			Domain.Features.Common.Enums.BaseTableEnum.EducationDegree,
			selectedValue: lastEducationDegreeId);

		MilitaryServcieStatusesSelectList =
			await
			Infrastructure.SelectLists.GetActiveItemsAsync
			(databaseContext: DatabaseContext, baseTableEnum:
			Domain.Features.Common.Enums.BaseTableEnum.MilitaryServiceStatus,
			selectedValue: militaryServcieStatusId);
	}
	#endregion /UpdateSelectListsAsync()

	#endregion /Methods
}
