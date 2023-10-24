namespace Domain.Features.Common;

public class LocalizedHomePageSetting :
	Seedwork.LocalizedEntity,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public LocalizedHomePageSetting
		(System.Guid cultureId) : base(cultureId: cultureId)
	{
	}
	#endregion /Constructor

	#region Properties

	#region public int Hits { get; set; }
	/// <summary>
	/// تعداد دفعات بازدید در زبان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Hits))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public int Hits { get; set; }
	#endregion /public int Hits { get; set; }



	#region public string? TopBody { get; set; }
	/// <summary>
	/// Top Body
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Top Body")]
	public string? TopBody { get; set; }
	#endregion /public string? TopBody { get; set; }

	#region public string? BottomBody { get; set; }
	/// <summary>
	/// Bottom Body
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Bottom Body")]
	public string? BottomBody { get; set; }
	#endregion /public string? BottomBody { get; set; }



	#region public string? Title { get; set; }
	/// <summary>
	/// عنوان سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.MetaTitle,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Title { get; set; }
	#endregion /public string? Title { get; set; }

	#region public string? Author { get; set; }
	/// <summary>
	/// نام و نام خانوادگی مالک سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Author))]
	public string? Author { get; set; }
	#endregion /public string? Author { get; set; }

	#region public string? ImageUrl { get; set; }
	/// <summary>
	/// نشانی تصویر سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ImageUrl))]
	public string? ImageUrl { get; set; }
	#endregion /public string? ImageUrl { get; set; }

	#region public string? Keywords { get; set; }
	/// <summary>
	/// کلید واژه‌های سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Keywords))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.MetaKeywords,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Keywords { get; set; }
	#endregion /public string? Keywords { get; set; }

	#region public string? Description { get; set; }
	/// <summary>
	/// توضیحات سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.MetaDescription,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Description { get; set; }
	#endregion /public string? Description { get; set; }

	#endregion /Properties
}
