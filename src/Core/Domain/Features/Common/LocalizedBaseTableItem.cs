namespace Domain.Features.Common;

public class LocalizedBaseTableItem : Seedwork.LocalizedEntity
{
	#region Constructor
	public LocalizedBaseTableItem(System.Guid cultureId,
		System.Guid baseTableItemId, string title) : base(cultureId: cultureId)
	{
		Title = title;
		BaseTableItemId = baseTableItemId;
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid BaseTableItemId { get; set; }
	/// <summary>
	/// آیتم اطلاعات پایه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.BaseTableItem))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid BaseTableItemId { get; set; }
	#endregion /public System.Guid BaseTableItemId { get; set; }

	#region public virtual BaseTableItem? BaseTableItem { get; private set; }
	/// <summary>
	/// آیتم اطلاعات پایه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.BaseTableItem))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual BaseTableItem? BaseTableItem { get; private set; }
	#endregion /public virtual BaseTableItem? BaseTableItem { get; private set; }

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
