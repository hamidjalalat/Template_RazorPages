namespace Domain.Features.Common;

public class ApplicationSetting :
	Seedwork.Entity,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public ApplicationSetting(System.Guid defaultCultureId) : base()
	{
		DigitCountInCaptchaImage = 4;
		FavoriteUserProfileLevel = 0;

		Theme =
			Cms.Enums.ThemeEnum.Ligth;

		LoginOption =
			Enums.LoginOptionEnum.Both;

		UpdateDateTime = InsertDateTime;
		DefaultCultureId = defaultCultureId;
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid DefaultCultureId { get; set; }
	/// <summary>
	/// زبان
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

	#region public virtual Features.Common.Culture? DefaultCulture { get; private set; }
	/// <summary>
	/// زبان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DefaultCulture))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual Features.Common.Culture? DefaultCulture { get; private set; }
	#endregion /public virtual Features.Common.Culture? DefaultCulture { get; private set; }



	#region public string? MasterPassword { get; set; }
	/// <summary>
	/// گذرواژه اصلی / شاه کلید
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MasterPassword))]
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



	#region public Cms.Enums.ThemeEnum Theme { get; set; }
	/// <summary>
	/// تم پیش‌فرض سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Theme))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public Cms.Enums.ThemeEnum Theme { get; set; }
	#endregion /public Cms.Enums.ThemeEnum Theme { get; set; }

	#region public Enums.LoginOptionEnum LoginOption { get; set; }
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
	public Enums.LoginOptionEnum LoginOption { get; set; }
	#endregion /public Enums.LoginOptionEnum LoginOption { get; set; }



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
	/// ثبت‌نام فعال است
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



	#region public System.DateTimeOffset UpdateDateTime { get; private set; }
	/// <summary>
	/// زمان ویرایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTimeOffset UpdateDateTime { get; private set; }
	#endregion /public System.DateTimeOffset UpdateDateTime { get; private set; }

	#endregion /Properties

	#region Read Only Properties

	#region public string ThemeName { get; }
	public string ThemeName
	{
		get
		{
			var result =
				Theme.ToString().ToLower();

			return result;
		}
	}
	#endregion /public string ThemeName { get; }

	#endregion /Read Only Properties

	#region Methods

	#region SetUpdateDateTime()
	public void SetUpdateDateTime()
	{
		UpdateDateTime = Dtat.DateTime.Now;
	}
	#endregion /SetUpdateDateTime()

	#endregion /Methods
}
