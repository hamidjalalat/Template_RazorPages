namespace ViewModels.Pages.Features.Cms.User.Posts;

public class CreateViewModel : object
{
	#region Constructor
	public CreateViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public bool IsDraft { get; set; }
	/// <summary>
	/// وضعیت صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsDraft))]
	public bool IsDraft { get; set; }
	#endregion /public bool IsDraft { get; set; }

	#region public bool IsCommentingEnabled { get; set; }
	/// <summary>
	/// فعال بودن اظهار نظر در صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsCommentingEnabled))]
	public bool IsCommentingEnabled { get; set; }
	#endregion /public bool IsCommentingEnabled { get; set; }

	#region public bool DisplayCommentsAfterVerification { get; set; }
	/// <summary>
	/// نمایش کامنت‌ها صرفا بعد از تایید نویسنده
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DisplayCommentsAfterVerification))]
	public bool DisplayCommentsAfterVerification { get; set; }
	#endregion /public bool DisplayCommentsAfterVerification { get; set; }



	#region public System.Guid TypeId { get; set; }
	/// <summary>
	/// دسته‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PostType))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid TypeId { get; set; }
	#endregion /public System.Guid TypeId { get; set; }

	#region public System.Guid CategoryId { get; set; }
	/// <summary>
	/// طبقه‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid CategoryId { get; set; }
	#endregion /public System.Guid CategoryId { get; set; }



	#region public string Title { get; set; }
	/// <summary>
	/// عنوان صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.MetaTitle,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Title { get; set; }
	//public string Title { get; set; }
	#endregion /public string Title { get; set; }

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

	#region public string? Author { get; set; }
	/// <summary>
	/// نام و نام خانوادگی کاربر
	/// این فیلد برای جلوگیری از اجرای کوئوری‌های سنگین در نظر گرفته شده است
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Author))]
	public string? Author { get; set; }
	#endregion /public string? Author { get; set; }

	#region public string? MovieUrl { get; set; }
	/// <summary>
	/// نشانی فیلم
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MovieUrl))]
	public string? MovieUrl { get; set; }
	#endregion /public string? MovieUrl { get; set; }

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

	#region public string? Introduction { get; set; }
	/// <summary>
	/// مقدمه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Introduction))]
	public string? Introduction { get; set; }
	#endregion /public string? Introduction { get; set; }

	#region public string? Body { get; set; }
	/// <summary>
	/// متن اصلی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Body))]
	public string? Body { get; set; }
	#endregion /public string? Body { get; set; }

	#endregion /Properties
}
