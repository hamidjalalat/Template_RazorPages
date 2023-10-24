using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.Users;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Supervisor)]
public class DetailsModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public DetailsModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	public ViewModels.Pages.Features.Identity.Admin.Users.DetailsOrDeleteViewModel ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
	<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? id)
	{
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
		var localizedUser =
			await
			DatabaseContext.LocalizedUsers
			.Where(current => current.UserId == id.Value)
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefaultAsync();
		// **************************************************

		// **************************************************
		ViewModel = new()
		{
			//Hits
			//PostCount
			//CommentCount

			Id = foundedUser.Id,

			RoleId = foundedUser.RoleId,
			GenderId = foundedUser.GenderId,

			IsActive = foundedUser.IsActive,
			IsDeleted = foundedUser.IsDeleted,
			IsFeatured = foundedUser.IsFeatured,
			IsTestData = foundedUser.IsTestData,
			IsVerified = foundedUser.IsVerified,
			IsUndeletable = foundedUser.IsUndeletable,
			IsProfilePublic = foundedUser.IsProfilePublic,
			IsEmailAddressVerified = foundedUser.IsEmailAddressVerified,
			IsNationalCodeVerified = foundedUser.IsNationalCodeVerified,
			IsVisibleInContactUsPage = foundedUser.IsVisibleInContactUsPage,
			IsCellPhoneNumberVerified = foundedUser.IsCellPhoneNumberVerified,

			RegisterType = foundedUser.RegisterType,

			Score = foundedUser.Score,
			Ordering = foundedUser.Ordering,
			MaxPostCountInProfilePage = foundedUser.MaxPostCountInProfilePage,

			InsertDateTime = foundedUser.InsertDateTime,
			UpdateDateTime = foundedUser.UpdateDateTime,
			DeleteDateTime = foundedUser.DeleteDateTime,
			LastLoginDateTime = foundedUser.LastLoginDateTime,
			LastChangePasswordDateTime = foundedUser.LastChangePasswordDateTime,

			Username = foundedUser.Username,
			ImageUrl = foundedUser.ImageUrl,
			RegisterIP = foundedUser.RegisterIP,
			EmailAddress = foundedUser.EmailAddress,
			NationalCode = foundedUser.NationalCode,
			CoverImageUrl = foundedUser.CoverImageUrl,
			CellPhoneNumber = foundedUser.CellPhoneNumber,
			AdminDescription = foundedUser.AdminDescription,

			LastName = localizedUser?.LastName,
			FirstName = localizedUser?.FirstName,
			Description = localizedUser?.Description,
			TitleInContactUsPage = localizedUser?.TitleInContactUsPage,

			RoleTitle =
				foundedUser.Role?
				.LocalizedBaseTableItems
				.Where(current => current.CultureId == currentCulture.Id)
				.FirstOrDefault()?.Title,

			GenderTitle =
				foundedUser.Gender?
				.LocalizedBaseTableItems
				.Where(current => current.CultureId == currentCulture.Id)
				.FirstOrDefault()?.Title,
		};

		if (localizedUser is not null)
		{
			ViewModel.Hits = localizedUser.Hits;
		}

		ViewModel.PostCount =
			DatabaseContext.Posts
			.Where(current => current.UserId == foundedUser.Id)
			.Count();

		ViewModel.CommentCount =
			DatabaseContext.PostComments
			.Where(current => current.UserId == foundedUser.Id)
			.Count();

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
