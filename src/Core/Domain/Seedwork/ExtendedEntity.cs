namespace Domain.Seedwork;

public abstract class ExtendedEntity : Entity,
	Dtat.Seedwork.Abstractions.IEntityHasIsActive,
	Dtat.Seedwork.Abstractions.IEntityHasIsSynced,
	Dtat.Seedwork.Abstractions.IEntityHasOrdering,
	Dtat.Seedwork.Abstractions.IEntityHasIsTestData,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime,
	Dtat.Seedwork.Abstractions.IEntityIdIsSetable<System.Guid>
{
	#region Constructor
	public ExtendedEntity() : base()
	{
		Ordering = 10_000;
		UpdateDateTime = InsertDateTime;
	}
	#endregion /Constructor

	#region Properties

	#region public virtual bool IsActive { get; set; }
	/// <summary>
	/// وضعیت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public virtual bool IsActive { get; set; }
	#endregion /public virtual bool IsActive { get; set; }

	#region public virtual bool IsSynced { get; set; }
	/// <summary>
	/// همگام‌سازی شده
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsSynced))]
	public virtual bool IsSynced { get; set; }
	#endregion /public virtual bool IsSynced { get; set; }

	#region public virtual bool IsTestData { get; set; }
	/// <summary>
	/// داده تستی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsTestData))]
	public virtual bool IsTestData { get; set; }
	#endregion /public virtual bool IsTestData { get; set; }



	#region public virtual int Ordering { get; set; }
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
	public virtual int Ordering { get; set; }
	#endregion /public virtual int Ordering { get; set; }



	#region public virtual string? Icon { get; set; }
	/// <summary>
	/// آیکن
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
	(ResourceType = typeof(Resources.DataDictionary),
	Name = nameof(Resources.DataDictionary.Icon))]
	public virtual string? Icon { get; set; }
	#endregion /public virtual string? Icon { get; set; }

	#region public virtual string? Color { get; set; }
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
	public virtual string? Color { get; set; }
	#endregion /public virtual string? Color { get; set; }

	#region public virtual string? ImageUrl { get; set; }
	/// <summary>
	/// نشانی تصویر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ImageUrl))]
	public virtual string? ImageUrl { get; set; }
	#endregion /public virtual string? ImageUrl { get; set; }

	#region public virtual string? CoverImageUrl { get; set; }
	/// <summary>
	/// نشانی تصویر کاور
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CoverImageUrl))]
	public virtual string? CoverImageUrl { get; set; }
	#endregion /public virtual string? CoverImageUrl { get; set; }



	#region public virtual System.DateTimeOffset UpdateDateTime { get; private set; }
	/// <summary>
	/// زمان ویرایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public virtual System.DateTimeOffset UpdateDateTime { get; private set; }
	#endregion /public virtual System.DateTimeOffset UpdateDateTime { get; private set; }

	#endregion /Properties

	#region Methods

	#region SetId()
	public void SetId(System.Guid id)
	{
		Id = id;
	}
	#endregion /SetId()

	#region SetUpdateDateTime()
	public void SetUpdateDateTime()
	{
		UpdateDateTime =
			Dtat.DateTime.Now;
	}
	#endregion /SetUpdateDateTime()

	#endregion /Methods
}
