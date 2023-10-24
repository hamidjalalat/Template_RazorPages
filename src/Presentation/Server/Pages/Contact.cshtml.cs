using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages;

public class ContactModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public ContactModel
		(Persistence.DatabaseContext databaseContext,

		Services.Features.Common.HttpContextService httpContextService,
		Services.Features.Common.EmailTemplateService emailTemplateService,
		Services.Features.Common.ApplicationSettingService applicationSettingService,
		Services.Features.Common.LocalizedMailSettingService localizedMailSettingService,

		DNTCaptcha.Core.IDNTCaptchaValidatorService captchaValidatorService,
		Microsoft.Extensions.Options.IOptions<DNTCaptcha.Core.DNTCaptchaOptions> captchaOptions) : base(databaseContext: databaseContext)
	{
		ViewModel = new();

		CaptchaOptions = captchaOptions.Value;
		CaptchaValidatorService = captchaValidatorService;

		HttpContextService = httpContextService;
		EmailTemplateService = emailTemplateService;
		ApplicationSettingService = applicationSettingService;
		LocalizedMailSettingService = localizedMailSettingService;
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.ContactViewModel ViewModel { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? UsersSelectList { get; set; }

	private DNTCaptcha.Core.DNTCaptchaOptions CaptchaOptions { get; init; }
	private DNTCaptcha.Core.IDNTCaptchaValidatorService CaptchaValidatorService { get; init; }

	private Services.Features.Common.HttpContextService HttpContextService { get; init; }
	private Services.Features.Common.EmailTemplateService EmailTemplateService { get; init; }
	private Services.Features.Common.ApplicationSettingService ApplicationSettingService { get; init; }
	private Services.Features.Common.LocalizedMailSettingService LocalizedMailSettingService { get; init; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		// **************************************************
		var applicationSetting =
			await
			ApplicationSettingService.GetInstanceAsync();

		if (applicationSetting.IsContactUsEnabled == false)
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

		if (currentCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		UsersSelectList =
			await
			Infrastructure.SelectLists.GetUsersForContactUsPageAsync
			(databaseContext: DatabaseContext, selectedValue: null);
		// **************************************************

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		// **************************************************
		var applicationSetting =
			await
			ApplicationSettingService.GetInstanceAsync();

		if (applicationSetting.IsContactUsEnabled == false)
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

		if (currentCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		if (applicationSetting.IsCaptchaImageEnabled)
		{
			if (CaptchaValidatorService.HasRequestValidCaptchaEntry() == false)
			{
				ModelState.AddModelError(CaptchaOptions.CaptchaComponent
					.CaptchaInputName, Resources.Messages.Errors.CaptchaImageIsNotCorrect);
			}
		}
		// **************************************************

		if (ModelState.IsValid == false)
		{
			UsersSelectList =
				await
				Infrastructure.SelectLists.GetUsersForContactUsPageAsync
				(databaseContext: DatabaseContext, selectedValue: ViewModel.RecipientUserId);

			return Page();
		}

		// **************************************************
		var localizedUser =
			await
			DatabaseContext.LocalizedUsers

			.Include(current => current.User)

			.Where(current => current.User!.IsActive)
			.Where(current => current.User!.IsDeleted == false)
			.Where(current => current.User!.IsVisibleInContactUsPage)

			.Where(current => current.UserId == ViewModel.RecipientUserId)

			.FirstOrDefaultAsync();

		if (localizedUser is null)
		{
			return RedirectToPage
				(pageName: Constants.CommonRouting.NotFound);
		}
		// **************************************************

		var localizedMailSetting =
			await
			LocalizedMailSettingService.GetInstanceAsync();

		// **************************************************
		var emailBodyContent =
			await
			EmailTemplateService.GetContentForContactingAsync();

		if (emailBodyContent is null)
		{
			return RedirectToPage
				(pageName: Constants.CommonRouting.NotFound);
		}

		var userIP =
			HttpContextService.GetRemoteIpAddress();

		emailBodyContent = emailBodyContent
			.Replace(oldValue: "{{USER_IP}}", newValue: userIP)
			.Replace(oldValue: "{{FULL_NAME}}", newValue: ViewModel.FullName)
			.Replace(oldValue: "{{EMAIL_BODY}}", newValue: ViewModel.EmailBody)
			.Replace(oldValue: "{{EMAIL_ADDRESS}}", newValue: ViewModel.EmailAddress)
			.Replace(oldValue: "{{EMAIL_SUBJECT}}", newValue: ViewModel.EmailSubject)
			.Replace(oldValue: "{{CELL_PHONE_NUMBER}}", newValue: ViewModel.CellPhoneNumber)
			;
		// **************************************************

		// **************************************************
		var recipient =
			new System.Net.Mail.MailAddress
			(address: localizedUser.User!.EmailAddress,
			displayName: localizedUser.TitleInContactUsPage);

		var subject =
			"Email From Contact US Page!";
		// **************************************************

		// **************************************************
		await Dtat.Net.Mail.Utility.SendAsync
			(recipient: recipient, subject: subject,
			body: emailBodyContent, mailSetting: localizedMailSetting);
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.EmailSentSuccessfully);

		AddPageSuccess(message: successMessage);
		// **************************************************

		// **************************************************
		ModelState.Clear();

		ViewModel = new();

		UsersSelectList =
			await
			Infrastructure.SelectLists.GetUsersForContactUsPageAsync
			(databaseContext: DatabaseContext, selectedValue: null);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
