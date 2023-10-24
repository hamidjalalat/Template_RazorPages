using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Services.Features.Identity;

public class UserNotificationService :
	Infrastructure.BaseServiceWithDatabaseContext
{
	#region Constructor
	public UserNotificationService
		(Persistence.DatabaseContext databaseContext,
		Common.HttpContextService httpContextService,
		Common.EmailTemplateService emailTemplateService,
		Common.LocalizedMailSettingService localizedMailSettingService) : base(databaseContext: databaseContext)
	{
		HttpContextService = httpContextService;
		EmailTemplateService = emailTemplateService;
		LocalizedMailSettingService = localizedMailSettingService;
	}
	#endregion /Constructor

	#region Properties

	private Common.HttpContextService HttpContextService { get; init; }
	private Common.EmailTemplateService EmailTemplateService { get; init; }
	private Common.LocalizedMailSettingService LocalizedMailSettingService { get; init; }

	#endregion /Properties

	#region Methods

	#region SendEmailVerificationKeyAsync()
	public async System.Threading.Tasks.Task
		SendEmailVerificationKeyAsync(Domain.Features.Identity.User user)
	{
		var localizedMailSetting =
			await
			LocalizedMailSettingService.GetInstanceAsync();

		var subject =
			"Verify Your Email Address!";

		var siteUrl =
			HttpContextService.GetCurrentHostUrl();

		var userIP =
			HttpContextService.GetRemoteIpAddress();

		var body =
			await
			EmailTemplateService
			.GetContentForRegistrationAsync();

		if (string.IsNullOrWhiteSpace(value: body))
		{
			return;
		}

		body = body
			.Replace(oldValue: "{{USER_IP}}", newValue: userIP)
			.Replace(oldValue: "{{SITE_URL}}", newValue: siteUrl)
			.Replace(oldValue: "{{EMAIL_ADDRESS}}", newValue: user.EmailAddress)
			.Replace(oldValue: "{{VERIFICATION_KEY}}", newValue: user.EmailAddressVerificationKey.ToString())
			;

		var recipient = new System.Net.Mail.MailAddress
			(address: user.EmailAddress, displayName: user.EmailAddress);

		try
		{
			await Dtat.Net.Mail.Utility.SendAsync
				(recipient: recipient, subject: subject,
				body: body, mailSetting: localizedMailSetting);
		}
		catch { }
	}
	#endregion /SendEmailVerificationKeyAsync()

	#region SendEmailForResettingPasswordAsync()
	public async System.Threading.Tasks.Task
		SendEmailForResettingPasswordAsync(Domain.Features.Identity.User user)
	{
		var localizedMailSetting =
			await
			LocalizedMailSettingService.GetInstanceAsync();

		var subject =
			"Reset Password!";

		var siteUrl =
			HttpContextService.GetCurrentHostUrl();

		var userIP =
			HttpContextService.GetRemoteIpAddress();

		var body =
			await
			EmailTemplateService
			.GetContentForResettingPasswordAsync();

		if (string.IsNullOrWhiteSpace(value: body))
		{
			return;
		}

		body = body
			.Replace(oldValue: "{{USER_IP}}", newValue: userIP)
			.Replace(oldValue: "{{SITE_URL}}", newValue: siteUrl)
			.Replace(oldValue: "{{EMAIL_ADDRESS}}", newValue: user.EmailAddress)
			.Replace(oldValue: "{{VERIFICATION_KEY}}", newValue: user.EmailAddressVerificationKey.ToString())
			;

		var recipient = new System.Net.Mail.MailAddress
			(address: user.EmailAddress, displayName: user.EmailAddress);

		try
		{
			await Dtat.Net.Mail.Utility.SendAsync
				(recipient: recipient, subject: subject,
				body: body, mailSetting: localizedMailSetting);
		}
		catch { }
	}
	#endregion /SendEmailForResettingPasswordAsync()

	#region NotifyAllActiveManagersAfterUserRegistrationAsync()
	public async System.Threading.Tasks.Task
		NotifyAllActiveManagersAfterUserRegistrationAsync(Domain.Features.Identity.User newUser)
	{
		var body =
			await
			EmailTemplateService
			.GetContentForNewUserAsync();

		if (string.IsNullOrWhiteSpace(value: body))
		{
			return;
		}

		var localizedMailSetting =
			await
			LocalizedMailSettingService.GetInstanceAsync();

		var users =
			await
			DatabaseContext.Users

			.Where(current => current.IsActive)
			.Where(current => current.IsDeleted == false)

			.Where(current => current.Role != null && current.Role.Code != null &&
				(current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.Programmer ||
				current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.Supervisor ||
				current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.Administrator ||
				current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner))

			.ToListAsync()
			;

		foreach (var user in users)
		{
			var subject =
				"New User Registration!";

			var siteUrl =
				HttpContextService.GetCurrentHostUrl();

			var userIP =
				HttpContextService.GetRemoteIpAddress();

			body = body
				.Replace(oldValue: "{{USER_IP}}", newValue: userIP)
				.Replace(oldValue: "{{SITE_URL}}", newValue: siteUrl)
				.Replace(oldValue: "{{EMAIL_ADDRESS}}", newValue: newUser.EmailAddress)
				.Replace(oldValue: "{{VERIFICATION_KEY}}", newValue: newUser.EmailAddressVerificationKey.ToString())
				;

			var recipient = new System.Net.Mail.MailAddress
				(address: user.EmailAddress, displayName: user.EmailAddress);

			try
			{
				await Dtat.Net.Mail.Utility.SendAsync
					(recipient: recipient,
					subject: subject,
					body: body,
					mailSetting: localizedMailSetting);
			}
			catch { }
		}
	}
	#endregion /NotifyAllActiveManagersAfterUserRegistrationAsync()



	#region NotifyAllActiveManagersAfterAOldPostModifiedAsync()
	public async System.Threading.Tasks.Task
		NotifyAllActiveManagersAfterAOldPostModifiedAsync
		(Domain.Features.Cms.Post modifiedPost, string userEmailAddress)
	{
		var body =
			await
			EmailTemplateService
			.GetContentForModifyingOldPostAsync();

		if (string.IsNullOrWhiteSpace(value: body))
		{
			return;
		}

		var localizedMailSetting =
			await
			LocalizedMailSettingService.GetInstanceAsync();

		var users =
			await
			DatabaseContext.Users

			.Where(current => current.IsActive)
			.Where(current => current.IsDeleted == false)

			.Where(current => current.Role != null && current.Role.Code != null &&
				(current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.Programmer ||
				current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.Supervisor ||
				current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.Administrator ||
				current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner))

			.ToListAsync()
			;

		foreach (var user in users)
		{
			var subject =
				"A Post Modified!";

			var siteUrl =
				HttpContextService.GetCurrentHostUrl();

			var userIP =
				HttpContextService.GetRemoteIpAddress();

			body = body
				.Replace(oldValue: "{{USER_IP}}", newValue: userIP)
				.Replace(oldValue: "{{SITE_URL}}", newValue: siteUrl)
				.Replace(oldValue: "{{EMAIL_ADDRESS}}", newValue: userEmailAddress)

				.Replace(oldValue: "{{POST_ID}}", newValue: modifiedPost.Id.ToString())
				.Replace(oldValue: "{{POST_TITLE}}", newValue: modifiedPost.Title)
				.Replace(oldValue: "{{POST_DESCRIPTION}}", newValue: modifiedPost.Description)
				;

			var recipient = new System.Net.Mail.MailAddress
				(address: user.EmailAddress, displayName: user.EmailAddress);

			try
			{
				await Dtat.Net.Mail.Utility.SendAsync
					(recipient: recipient,
					subject: subject,
					body: body,
					mailSetting: localizedMailSetting);
			}
			catch { }
		}
	}
	#endregion NotifyAllActiveManagersAfterAOldPostModifiedAsync()

	#region NotifyAllActiveManagersAfterNewPostPublishedAsync()
	public async System.Threading.Tasks.Task
		NotifyAllActiveManagersAfterNewPostPublishedAsync
		(Domain.Features.Cms.Post newPost, string userEmailAddress)
	{
		var body =
			await
			EmailTemplateService
			.GetContentForPublishingNewPostAsync();

		if (string.IsNullOrWhiteSpace(value: body))
		{
			return;
		}

		var localizedMailSetting =
			await
			LocalizedMailSettingService.GetInstanceAsync();

		var users =
			await
			DatabaseContext.Users

			.Where(current => current.IsActive)
			.Where(current => current.IsDeleted == false)

			.Where(current =>
				current.Role != null && current.Role.Code != null &&
				(current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.Programmer ||
				current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.Supervisor ||
				current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.Administrator ||
				current.Role.Code == (int)Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner))

			.ToListAsync()
			;

		foreach (var user in users)
		{
			var subject =
				"New Post Published!";

			var siteUrl =
				HttpContextService.GetCurrentHostUrl();

			var userIP =
				HttpContextService.GetRemoteIpAddress();

			body = body
				.Replace(oldValue: "{{USER_IP}}", newValue: userIP)
				.Replace(oldValue: "{{SITE_URL}}", newValue: siteUrl)
				.Replace(oldValue: "{{EMAIL_ADDRESS}}", newValue: userEmailAddress)

				.Replace(oldValue: "{{POST_ID}}", newValue: newPost.Id.ToString())
				.Replace(oldValue: "{{POST_TITLE}}", newValue: newPost.Title)
				.Replace(oldValue: "{{POST_DESCRIPTION}}", newValue: newPost.Description)
				;

			var recipient = new System.Net.Mail.MailAddress
				(address: user.EmailAddress, displayName: user.EmailAddress);

			try
			{
				await Dtat.Net.Mail.Utility.SendAsync
					(recipient: recipient,
					subject: subject,
					body: body,
					mailSetting: localizedMailSetting);
			}
			catch { }
		}
	}
	#endregion NotifyAllActiveManagersAfterNewPostPublishedAsync()

	#endregion /Methods
}
