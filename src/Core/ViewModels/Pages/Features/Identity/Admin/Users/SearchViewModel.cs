namespace ViewModels.Pages.Features.Identity.Admin.Users;

public class SearchViewModel : object
{
	#region Constructor
	public SearchViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid? RoleId { get; set; }
	/// <summary>
	/// نقش کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Role))]
	public System.Guid? RoleId { get; set; }
	#endregion /public System.Guid? RoleId { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? RolesSelectList { get; set; }

	#region public System.Guid? GenderId { get; set; }
	/// <summary>
	/// طبقه‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]
	public System.Guid? GenderId { get; set; }
	#endregion /public System.Guid? GenderId { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? GendersSelectList { get; set; }

	#region public bool? IsActive { get; set; }
	/// <summary>
	/// فعال
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool? IsActive { get; set; }
	#endregion /public bool? IsActive { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? IsActiveSelectList { get; set; }

	#region public bool? IsDeleted { get; set; }
	/// <summary>
	/// حذف شده به صورت مجازی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsDeleted))]
	public bool? IsDeleted { get; set; }
	#endregion /public bool? IsDeleted { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? IsDeletedSelectList { get; set; }

	#region public bool? IsFeatured { get; set; }
	/// <summary>
	/// ویژه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsFeatured))]
	public bool? IsFeatured { get; set; }
	#endregion /public bool? IsFeatured { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? IsFeaturedSelectList { get; set; }

	#region public bool? IsVerified { get; set; }
	/// <summary>
	/// تایید شده
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsVerified))]
	public bool? IsVerified { get; set; }
	#endregion /public bool? IsVerified { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? IsVerifiedSelectList { get; set; }

	#region public bool? IsEmailAddressVerified { get; set; }
	/// <summary>
	/// نشانی پست الکترونیکی تایید شده است
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsEmailAddressVerified))]
	public bool? IsEmailAddressVerified { get; set; }
	#endregion /public bool? IsEmailAddressVerified { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? IsEmailAddressVerifiedSelectList { get; set; }

	#region public bool? IsNationalCodeVerified { get; set; }
	/// <summary>
	/// آیا کد ملی تایید شده است یا خیر؟
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsNationalCodeVerified))]
	public bool? IsNationalCodeVerified { get; set; }
	#endregion /public bool? IsNationalCodeVerified { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? IsNationalCodeVerifiedSelectList { get; set; }

	#region public bool? IsVisibleInContactUsPage { get; set; }
	/// <summary>
	/// در صفحه تماس با ما نمایش داده شود
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsVisibleInContactUsPage))]
	public bool? IsVisibleInContactUsPage { get; set; }
	#endregion /public bool? IsVerified { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? IsVisibleInContactUsPageSelectList { get; set; }

	#region public bool? IsCellPhoneNumberVerified { get; set; }
	/// <summary>
	/// شماره تلفن همراه تایید شده است
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsCellPhoneNumberVerified))]
	public bool? IsCellPhoneNumberVerified { get; set; }
	#endregion /public bool? IsVerified { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? IsCellPhoneNumberVerifiedSelectList { get; set; }

	#region public string? Username { get; set; }
	/// <summary>
	/// شناسه‌کاربری
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Username))]
	public string? Username { get; set; }
	#endregion /public string? Username { get; set; }

	#region public string? EmailAddress { get; set; }
	/// <summary>
	/// نشانی پست الکترونیکی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.EmailAddress))]
	public string? EmailAddress { get; set; }
	#endregion /public string? EmailAddress { get; set; }

	#region public string? NationalCode { get; set; }
	/// <summary>
	/// کد ملی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NationalCode))]
	public string? NationalCode { get; set; }
	#endregion /public string? NationalCode { get; set; }

	#region public string? CellPhoneNumber { get; set; }
	/// <summary>
	/// شماره تلفن همراه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CellPhoneNumber))]
	public string? CellPhoneNumber { get; set; }
	#endregion /public string? CellPhoneNumber { get; set; }

	#region public string? AdminDescription { get; set; }
	/// <summary>
	/// توضیحات مدیریتی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AdminDescription))]
	public string? AdminDescription { get; set; }
	#endregion /public string? AdminDescription { get; set; }

	#endregion /Properties
}
