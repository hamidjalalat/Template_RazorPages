﻿namespace Domain.Features.Cms;

public class PostCategory :
	Seedwork.LocalizedEntity,
	Dtat.Seedwork.Abstractions.IEntityHasIsActive,
	Dtat.Seedwork.Abstractions.IEntityHasOrdering,
	Dtat.Seedwork.Abstractions.IEntityHasIsTestData,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public PostCategory
		(System.Guid cultureId, string name, string title) : base(cultureId: cultureId)
	{
		Name = name;
		Title = title;

		Ordering = 10_000;

		Posts =
			new System.Collections.Generic.List<Post>();
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



	#region public int Hits { get; set; }
	/// <summary>
	/// تعداد دفعات بازدید
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

	#region public int MaxPostCountInMainPage { get; set; }
	/// <summary>
	/// حداکثر تعداد مطالب در صفحه اصلی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MaxPostCountInMainPage))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public int MaxPostCountInMainPage { get; set; }
	#endregion /public int MaxPostCountInMainPage { get; set; }



	#region public bool DisplayInHomePage { get; set; }
	/// <summary>
	/// نمایش در صفحه اول / خانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DisplayInHomePage))]
	public bool DisplayInHomePage { get; set; }
	#endregion /public bool DisplayInHomePage { get; set; }

	#region public int MaxPostCountInHomePage { get; set; }
	/// <summary>
	/// حداکثر تعداد مطالب در صفحه اول / خانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MaxPostCountInHomePage))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public int MaxPostCountInHomePage { get; set; }
	#endregion /public int MaxPostCountInHomePage { get; set; }



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



	#region public string Name { get; set; }
	/// <summary>
	/// نام
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Constants.RegularExpression.AToZDigitsUnderline,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Name))]
	public string Name { get; set; }
	#endregion /public string Name { get; set; }

	#region public string? Body { get; set; }
	/// <summary>
	/// متن اصلی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Body))]
	public string? Body { get; set; }
	#endregion /public string? Body { get; set; }

	#region public string Title { get; set; }
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
		(length: Constants.MaxLength.MetaTitle,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string Title { get; set; }
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

	#endregion /Properties

	#region Read Only Properties

	public string DisplayName
	{
		get
		{
			var result =
				$"{Title} ({Name})";

			if (IsActive == false)
			{
				result +=
					$" ({Resources.DataDictionary.Inactive})";
			}

			return result;
		}
	}

	#endregion /Read Only Properties

	#region Collections

	public virtual System.Collections.Generic.IList<Post> Posts { get; private set; }

	#endregion /Collections
}
