namespace Domain.Features.Common;

public class LocalizedBaseTable : Seedwork.LocalizedEntity
{
	#region Constructor
	public LocalizedBaseTable(System.Guid cultureId,
		System.Guid baseTableId, string title) : base(cultureId: cultureId)
	{
		Title = title;
		BaseTableId = baseTableId;
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid BaseTableId { get; set; }
	/// <summary>
	/// نوع اطلاعات پایه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.BaseTable))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid BaseTableId { get; set; }
	#endregion /public System.Guid BaseTableId { get; set; }

	#region public virtual BaseTable? BaseTable { get; private set; }
	/// <summary>
	/// نوع اطلاعات پایه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.BaseTable))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual BaseTable? BaseTable { get; private set; }
	#endregion /public virtual BaseTable? BaseTable { get; private set; }

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
		(length: Constants.MaxLength.Title,
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
	public string? Description { get; set; }
	#endregion /public string? Description { get; set; }

	#endregion /Properties
}
