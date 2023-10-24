namespace ViewModels.Pages.Features.Cms.Admin.Posts;

public class SearchViewModel : object
{
	#region Constructor
	public SearchViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid? TypeId { get; set; }
	/// <summary>
	/// دسته‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PostType))]
	public System.Guid? TypeId { get; set; }
	#endregion /public System.Guid? TypeId { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? TypesSelectList { get; set; }

	#region public System.Guid? CategoryId { get; set; }
	/// <summary>
	/// طبقه‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]
	public System.Guid? CategoryId { get; set; }
	#endregion /public System.Guid? CategoryId { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? CategoriesSelectList { get; set; }

	#region public bool? IsDraft { get; set; }
	/// <summary>
	/// پیش‌نویس
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsDraft))]
	public bool? IsDraft { get; set; }
	#endregion /public bool? IsDraft { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? IsDraftSelectList { get; set; }

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

	#region public string? Body { get; set; }
	/// <summary>
	/// متن اصلی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Body))]
	public string? Body { get; set; }
	#endregion /public string? Body { get; set; }

	#region public string? Title { get; set; }
	/// <summary>
	/// عنوان
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

	#region public string? Description { get; set; }
	/// <summary>
	/// توضیحات
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

	#region public string? Introduction { get; set; }
	/// <summary>
	/// مقدمه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Introduction))]
	public string? Introduction { get; set; }
	#endregion /public string? Introduction { get; set; }

	#endregion /Properties
}
