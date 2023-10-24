namespace ViewModels.Pages.Features.Common.Admin.ApplicationSettings;

public class UpdateViewModel : object
{
	#region Constructor
	public UpdateViewModel() : base()
	{
		DigitCountInCaptchaImage = 4;
		FavoriteUserProfileLevel = 0;

		Theme = Domain.Features
			.Cms.Enums.ThemeEnum.Ligth;

		LoginOption = Domain.Features
			.Common.Enums.LoginOptionEnum.Both;
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid Id { get; set; }
	/// <summary>
	/// شناسه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public System.Guid Id { get; set; }
	#endregion /public System.Guid Id { get; set; }

	#region public System.Guid DefaultCultureId { get; set; }
	/// <summary>
	/// زبان پیش‌فرض
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DefaultCulture))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid DefaultCultureId { get; set; }
	#endregion /public System.Guid DefaultCultureId { get; set; }



	#region public string? MasterPassword { get; set; }
	/// <summary>
	/// گذرواژه اصلی / شاه کلید
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MasterPassword))]

	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Constants.RegularExpression.Password,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Password))]
	public string? MasterPassword { get; set; }
	#endregion /public string? MasterPassword { get; set; }

	#region public string? GoogleAnalyticsCode { get; set; }
	/// <summary>
	/// Google Analytics Code
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Google Analytics Code")]
	public string? GoogleAnalyticsCode { get; set; }
	#endregion /public string? GoogleAnalyticsCode { get; set; }

	#region public string? GoogleTagManagerCode { get; set; }
	/// <summary>
	/// Google Tag Manager Code
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Google Tag Manager Code")]
	public string? GoogleTagManagerCode { get; set; }
	#endregion /public string? GoogleTagManagerCode { get; set; }



	#region public int FavoriteUserProfileLevel { get; set; }
	/// <summary>
	/// سطح مورد نظر برای پروفایل کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FavoriteUserProfileLevel))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public int FavoriteUserProfileLevel { get; set; }
	#endregion /public int FavoriteUserProfileLevel { get; set; }



	#region public Domain.Features.Cms.Enums.ThemeEnum Theme { get; set; }
	/// <summary>
	/// تم سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Theme))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public Domain.Features.Cms.Enums.ThemeEnum Theme { get; set; }
	#endregion /public Domain.Features.Cms.Enums.ThemeEnum Theme { get; set; }

	#region public Domain.Features.Common.Enums.LoginOptionEnum LoginOption { get; set; }
	/// <summary>
	/// تنوع ورود به سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.LoginOption))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public Domain.Features.Common.Enums.LoginOptionEnum LoginOption { get; set; }
	#endregion /public Domain.Features.Common.Enums.LoginOptionEnum LoginOption { get; set; }



	#region public bool IsLoginVisible { get; set; }
	/// <summary>
	/// نمایش ورود به سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsLoginVisible))]
	public bool IsLoginVisible { get; set; }
	#endregion /public bool IsLoginVisible { get; set; }

	#region public bool IsContactUsEnabled { get; set; }
	/// <summary>
	/// امکان تماس با ما
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsContactUsEnabled))]
	public bool IsContactUsEnabled { get; set; }
	#endregion /public bool IsContactUsEnabled { get; set; }

	#region public bool IsRegistrationEnabled { get; set; }
	/// <summary>
	/// امکان ثبت‌نام
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsRegistrationEnabled))]
	public bool IsRegistrationEnabled { get; set; }
	#endregion /public bool IsRegistrationEnabled { get; set; }

	#region public bool IsSearchInNavbarEnabled { get; set; }
	/// <summary>
	/// امکان جستجوی مطالب در منوی سایت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsSearchInNavbarEnabled))]
	public bool IsSearchInNavbarEnabled { get; set; }
	#endregion /public bool IsSearchInNavbarEnabled { get; set; }

	#region public bool ActivateUserAfterRegistration { get; set; }
	/// <summary>
	/// فعال‌سازی خودکار کاربر بعد از ثبت‌نام
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ActivateUserAfterRegistration))]
	public bool ActivateUserAfterRegistration { get; set; }
	#endregion /public bool ActivateUserAfterRegistration { get; set; }

	#region public bool IsGoogleAuthenticationEnabled { get; set; }
	/// <summary>
	/// امکان ورود به سامانه از طریق گوگل
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsGoogleAuthenticationEnabled))]
	public bool IsGoogleAuthenticationEnabled { get; set; }
	#endregion /public bool IsGoogleAuthenticationEnabled { get; set; }



	#region public bool IsCaptchaImageEnabled { get; set; }
	/// <summary>
	/// فعال‌سازی کد امنیتی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsCaptchaImageEnabled))]
	public bool IsCaptchaImageEnabled { get; set; }
	#endregion /public bool IsCaptchaImageEnabled { get; set; }

	#region public int DigitCountInCaptchaImage { get; set; }
	/// <summary>
	/// تعداد ارقام در کد امنیتی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DigitCountInCaptchaImage))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 4, maximum: 8,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int DigitCountInCaptchaImage { get; set; }
	#endregion /public int DigitCountInCaptchaImage { get; set; }

	#endregion /Properties
}
