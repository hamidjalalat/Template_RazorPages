using Dtat;

namespace ViewModels.Pages.Features.Identity.Admin.Users;

public class DetailsOrDeleteViewModel : UpdateViewModel
{
	#region Constructor
	public DetailsOrDeleteViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public string? RoleTitle { get; set; }
	/// <summary>
	/// نقش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Role))]
	public string? RoleTitle { get; set; }
	#endregion /public string? RoleTitle { get; set; }

	#region public string? GenderTitle { get; set; }
	/// <summary>
	/// جنسیت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Gender))]
	public string? GenderTitle { get; set; }
	#endregion /public string? GenderTitle { get; set; }



	#region public bool IsTestData { get; set; }
	/// <summary>
	/// داده تستی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsTestData))]
	public bool IsTestData { get; set; }
	#endregion /public bool IsTestData { get; set; }

	#region public bool IsUndeletable { get; set; }
	/// <summary>
	/// غیر قابل حذف
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsUndeletable))]
	public bool IsUndeletable { get; set; }
	#endregion /public bool IsUndeletable { get; set; }



	#region public string? ImageUrl { get; set; }
	/// <summary>
	/// نشانی تصویر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ImageUrl))]
	public string? ImageUrl { get; set; }
	#endregion /public string? ImageUrl { get; set; }

	#region public string RegisterIP { get; set; }
	/// <summary>
	/// آی‌پی کاربر در زمان ثبت‌نام
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RegisterIP))]
	public string? RegisterIP { get; set; }
	//public string RegisterIP { get; set; }
	#endregion /public string RegisterIP { get; set; }

	#region public string? Description { get; set; }
	/// <summary>
	/// توضیحات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]
	public string? Description { get; set; }
	#endregion /public string? Description { get; set; }

	#region public string? CoverImageUrl { get; set; }
	/// <summary>
	/// نشانی تصویر کاور
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CoverImageUrl))]
	public string? CoverImageUrl { get; set; }
	#endregion /public string? CoverImageUrl { get; set; }



	#region public int PostCount { get; set; }
	/// <summary>
	/// تعداد مطالب
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PostCount))]
	public int PostCount { get; set; }
	#endregion /public int PostCount { get; set; }

	#region public int CommentCount { get; set; }
	/// <summary>
	/// تعداد کامنت‌ها
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CommentCount))]
	public int CommentCount { get; set; }
	#endregion /public int CommentCount { get; set; }

	#region public int MaxPostCountInProfilePage { get; set; }
	/// <summary>
	/// حداکثر تعداد مطالب در صفحه پروفایل
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MaxPostCountInProfilePage))]
	public int MaxPostCountInProfilePage { get; set; }
	#endregion /public int MaxPostCountInProfilePage { get; set; }



	#region Domain.Features.Identity.Enums.AuthenticationTypeEnum RegisterType { get; set; }
	/// <summary>
	/// نوع ثبت‌نام در سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RegisterType))]
	public Domain.Features.Identity.Enums.AuthenticationTypeEnum RegisterType { get; set; }
	#endregion /Domain.Features.Identity.Enums.AuthenticationTypeEnum RegisterType { get; set; }



	#region public System.DateTimeOffset InsertDateTime { get; set; }
	/// <summary>
	/// زمان ثبت‌نام
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RegisterTime))]
	public System.DateTimeOffset InsertDateTime { get; set; }
	#endregion /public System.DateTimeOffset InsertDateTime { get; set; }

	#region public System.DateTimeOffset UpdateDateTime { get; set; }
	/// <summary>
	/// زمان ویرایش پروفایل
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateProfileTime))]
	public System.DateTimeOffset UpdateDateTime { get; set; }
	#endregion /public System.DateTimeOffset UpdateDateTime { get; set; }

	#region public System.DateTimeOffset? DeleteDateTime { get; set; }
	/// <summary>
	/// زمان حذف مجازی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DeleteDateTime))]
	public System.DateTimeOffset? DeleteDateTime { get; set; }
	#endregion /public System.DateTimeOffset? DeleteDateTime { get; set; }

	#region public System.DateTimeOffset? LastLoginDateTime { get; set; }
	/// <summary>
	/// آخرین زمان ورود به سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.LastLoginDateTime))]
	public System.DateTimeOffset? LastLoginDateTime { get; set; }
	#endregion /public System.DateTimeOffset? LastLoginDateTime { get; set; }

	#region public System.DateTimeOffset? LastChangePasswordDateTime { get; set; }
	/// <summary>
	/// آخرین زمان تغییر گذرواژه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.LastChangePasswordDateTime))]
	public System.DateTimeOffset? LastChangePasswordDateTime { get; set; }
	#endregion /public System.DateTimeOffset? LastChangePasswordDateTime { get; set; }

	#endregion /Properties

	#region Read Only Properties

	#region Domain.Features.Identity.Enums.AuthenticationTypeEnum RegisterType { get; set; }
	/// <summary>
	/// نوع ثبت‌نام در سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RegisterType))]
	public string? RegisterTypeTitle
	{
		get
		{
			var result =
				RegisterType.GetDisplayName();

			return result;
		}
	}
	#endregion /Domain.Features.Identity.Enums.AuthenticationTypeEnum RegisterType { get; set; }

	#endregion /Read Only Properties
}
