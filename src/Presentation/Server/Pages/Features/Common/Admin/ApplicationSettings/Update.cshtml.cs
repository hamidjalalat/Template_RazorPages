using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Common.Admin.ApplicationSettings;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Administrator)]
public class UpdateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? CulturesSelectList { get; set; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Common.Admin.ApplicationSettings.UpdateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		CulturesSelectList =
			await
			Infrastructure.SelectLists.GetCulturesAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		var result =
			await
			DatabaseContext.ApplicationSettings

			.Select(current => new ViewModels.Pages
				.Features.Common.Admin.ApplicationSettings.UpdateViewModel()
			{
				Id = current.Id,

				Theme = current.Theme,
				LoginOption = current.LoginOption,

				DefaultCultureId = current.DefaultCultureId,

				MasterPassword = current.MasterPassword,
				GoogleAnalyticsCode = current.GoogleAnalyticsCode,
				GoogleTagManagerCode = current.GoogleTagManagerCode,

				FavoriteUserProfileLevel = current.FavoriteUserProfileLevel,

				IsLoginVisible = current.IsLoginVisible,
				IsContactUsEnabled = current.IsContactUsEnabled,
				IsCaptchaImageEnabled = current.IsCaptchaImageEnabled,
				IsRegistrationEnabled = current.IsRegistrationEnabled,
				IsSearchInNavbarEnabled = current.IsSearchInNavbarEnabled,
				DigitCountInCaptchaImage = current.DigitCountInCaptchaImage,
				ActivateUserAfterRegistration = current.ActivateUserAfterRegistration,
				IsGoogleAuthenticationEnabled = current.IsGoogleAuthenticationEnabled,
			})
			.FirstOrDefaultAsync();

		if (result is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		ViewModel = result;

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		CulturesSelectList =
			await
			Infrastructure.SelectLists.GetCulturesAsync
			(databaseContext: DatabaseContext, selectedValue: ViewModel.DefaultCultureId);

		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.ApplicationSettings
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedItem.SetUpdateDateTime();

		foundedItem.Theme = ViewModel.Theme;
		foundedItem.LoginOption = ViewModel.LoginOption;

		foundedItem.DefaultCultureId = ViewModel.DefaultCultureId;

		foundedItem.MasterPassword = ViewModel.MasterPassword.Fix();
		foundedItem.GoogleAnalyticsCode = ViewModel.GoogleAnalyticsCode.Fix();
		foundedItem.GoogleTagManagerCode = ViewModel.GoogleTagManagerCode.Fix();

		foundedItem.FavoriteUserProfileLevel = ViewModel.FavoriteUserProfileLevel;

		foundedItem.IsLoginVisible = ViewModel.IsLoginVisible;
		foundedItem.IsContactUsEnabled = ViewModel.IsContactUsEnabled;
		foundedItem.IsCaptchaImageEnabled = ViewModel.IsCaptchaImageEnabled;
		foundedItem.IsRegistrationEnabled = ViewModel.IsRegistrationEnabled;
		foundedItem.IsSearchInNavbarEnabled = ViewModel.IsSearchInNavbarEnabled;
		foundedItem.DigitCountInCaptchaImage = ViewModel.DigitCountInCaptchaImage;
		foundedItem.ActivateUserAfterRegistration = ViewModel.ActivateUserAfterRegistration;
		foundedItem.IsGoogleAuthenticationEnabled = ViewModel.IsGoogleAuthenticationEnabled;
		// **************************************************

		await DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Data);

		AddPageSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
