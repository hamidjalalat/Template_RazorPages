namespace ViewModels.Pages.Features.Common.Admin.BaseTableItems;

public class CreateViewModel : object
{
	#region Constructor
	public CreateViewModel() : base()
	{
		Ordering = 10_000;
	}
	#endregion /Constructor

	#region Properties

	#region public bool IsActive { get; set; }
	/// <summary>
	/// وضعیت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }
	#endregion /public bool IsActive { get; set; }

	#region public bool IsTestData { get; set; }
	/// <summary>
	/// داده تستی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsTestData))]
	public bool IsTestData { get; set; }
	#endregion /public bool IsTestData { get; set; }

	#region public int Ordering { get; set; }
	/// <summary>
	/// چیدمان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Ordering))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 1, maximum: 100_000,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int Ordering { get; set; }
	#endregion /public int Ordering { get; set; }

	#region public string? KeyName { get; set; }
	/// <summary>
	/// نام کلیدی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.KeyName))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.KeyName,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Constants.RegularExpression.AToZDigitsUnderline,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.KeyName))]
	public string? KeyName { get; set; }
	#endregion /public string? KeyName { get; set; }

	#region public string? Title { get; set; }
	/// <summary>
	/// عنوان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Title { get; set; }
	#endregion /public string? Title { get; set; }

	#region public int? Code { get; set; }
	/// <summary>
	/// کد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Code))]

	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Constants.RegularExpression.JustDigits,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.JustDigits))]
	public int? Code { get; set; }
	#endregion /public int? Code { get; set; }

	#region public string? Color { get; set; }
	/// <summary>
	/// رنگ
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
	(ResourceType = typeof(Resources.DataDictionary),
	Name = nameof(Resources.DataDictionary.Color))]

	[System.ComponentModel.DataAnnotations.MaxLength
	(length: Constants.MaxLength.Color,
	ErrorMessageResourceType = typeof(Resources.Messages.Validations),
	ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	// TODO
	//[System.ComponentModel.DataAnnotations.RegularExpression
	//(pattern: Constants.RegularExpression.Color,
	//ErrorMessageResourceType = typeof(Resources.Messages.Validations),
	//ErrorMessageResourceName = nameof(Resources.Messages.Validations.Color))]
	public string? Color { get; set; }
	#endregion /public string? Color { get; set; }

	#region public string? Icon { get; set; }
	/// <summary>
	/// آیکن
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
	(ResourceType = typeof(Resources.DataDictionary),
	Name = nameof(Resources.DataDictionary.Icon))]
	public string? Icon { get; set; }
	#endregion /public string? Icon { get; set; }

	#region public string? ImageUrl { get; set; }
	/// <summary>
	/// نشانی تصویر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ImageUrl))]
	public string? ImageUrl { get; set; }
	#endregion /public string? ImageUrl { get; set; }

	#region public string? CoverImageUrl { get; set; }
	/// <summary>
	/// نشانی تصویر کاور
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CoverImageUrl))]
	public string? CoverImageUrl { get; set; }
	#endregion /public string? CoverImageUrl { get; set; }

	#region public string? Description { get; set; }
	/// <summary>
	/// توضیحات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]
	public string? Description { get; set; }
	#endregion /public string? Description { get; set; }

	#endregion /Properties
}
