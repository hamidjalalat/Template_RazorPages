namespace ViewModels.Pages.Features.Cms.User.Posts;

public class DetailsOrDeleteViewModel : UpdateViewModel
{
	#region Constructor
	public DetailsOrDeleteViewModel() : base()
	{
		TypeName = string.Empty;
		CategoryName = string.Empty;
	}
	#endregion /Constructor

	#region Properties

	#region public bool IsActive { get; set; }
	/// <summary>
	/// وضعیت صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }
	#endregion /public bool IsActive { get; set; }

	#region public bool IsFeatured { get; set; }
	/// <summary>
	/// ویژه بودن مطلب
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsFeatured))]
	public bool IsFeatured { get; set; }
	#endregion /public bool IsFeatured { get; set; }



	#region public int Hits { get; set; }
	/// <summary>
	/// تعداد دفعات بازدید
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Hits))]
	public int Hits { get; set; }
	#endregion /public int Hits { get; set; }

	#region public int Score { get; set; }
	/// <summary>
	/// تعداد دفعات بازدید
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Score))]
	public int Score { get; set; }
	#endregion /public int Score { get; set; }

	#region public int CommentCount { get; set; }
	/// <summary>
	/// تعداد نظرات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CommentCount))]
	public int CommentCount { get; set; }
	#endregion /public int CommentCount { get; set; }



	#region public string TypeName { get; set; }
	/// <summary>
	/// دسته‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Type))]
	public string TypeName { get; set; }
	#endregion /public string TypeName { get; set; }

	#region public string CategoryName { get; set; }
	/// <summary>
	/// طبقه‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]
	public string CategoryName { get; set; }
	#endregion /public string CategoryName { get; set; }

	#region public string? AdminDescription { get; set; }
	/// <summary>
	/// توضیحات مدیریتی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AdminDescription))]
	public string? AdminDescription { get; set; }
	#endregion /public string? AdminDescription { get; set; }



	#region public System.DateTimeOffset InsertDateTime { get; set; }
	/// <summary>
	/// زمان ایجاد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.InsertDateTime))]
	public System.DateTimeOffset InsertDateTime { get; set; }
	#endregion /public System.DateTimeOffset InsertDateTime { get; set; }

	#region public System.DateTimeOffset UpdateDateTime { get; set; }
	/// <summary>
	/// زمان ویرایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]
	public System.DateTimeOffset UpdateDateTime { get; set; }
	#endregion /public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
