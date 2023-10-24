namespace Domain.Seedwork;

public abstract class LocalizedEntity : Entity,
	Dtat.Seedwork.Abstractions.IEntityHasCultureId<System.Guid>
{
	#region Constructor
	public LocalizedEntity(System.Guid cultureId) : base()
	{
		CultureId = cultureId;
		UpdateDateTime = InsertDateTime;
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid CultureId { get; private set; }
	/// <summary>
	/// زبان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Culture))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid CultureId { get; private set; }
	#endregion /public System.Guid CultureId { get; private set; }

	#region public virtual Features.Common.Culture? Culture { get; private set; }
	/// <summary>
	/// زبان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Culture))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual Features.Common.Culture? Culture { get; private set; }
	#endregion /public virtual Features.Common.Culture? Culture { get; private set; }

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

	#region SetUpdateDateTime()
	public void SetUpdateDateTime()
	{
		UpdateDateTime =
			Dtat.DateTime.Now;
	}
	#endregion /SetUpdateDateTime()

	#endregion /Methods
}
