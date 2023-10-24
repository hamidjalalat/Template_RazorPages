namespace Domain.Features.Common;

public class BaseTableItem : Seedwork.ExtendedEntity
{
	#region Constructor
	public BaseTableItem
		(System.Guid baseTableId,
		string keyName, int? code = null) : base()
	{
		Code = code;
		KeyName = keyName;
		BaseTableId = baseTableId;

		Users_Role =
			new System.Collections.Generic.List<Identity.User>();

		Users_Gender =
			new System.Collections.Generic.List<Identity.User>();

		Users_Religion =
			new System.Collections.Generic.List<Identity.User>();

		Users_MaritalStatus =
			new System.Collections.Generic.List<Identity.User>();

		Users_LastEducationDegree =
			new System.Collections.Generic.List<Identity.User>();

		Users_MilitaryServiceStatus =
			new System.Collections.Generic.List<Identity.User>();

		LocalizedBaseTableItems =
			new System.Collections.Generic.List<LocalizedBaseTableItem>();
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

	#region public string KeyName { get; set; }
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
	public string KeyName { get; set; }
	#endregion /public string KeyName { get; set; }

	#region public int? Code { get; set; }
	/// <summary>
	/// کد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Code))]
	public int? Code { get; set; }
	#endregion /public int? Code { get; set; }

	#endregion /Properties

	#region Collections

	public virtual System.Collections.Generic.IList<Identity.User> Users_Role { get; private set; }
	public virtual System.Collections.Generic.IList<Identity.User> Users_Gender { get; private set; }
	public virtual System.Collections.Generic.IList<Identity.User> Users_Religion { get; private set; }
	public virtual System.Collections.Generic.IList<Identity.User> Users_MaritalStatus { get; private set; }
	public virtual System.Collections.Generic.IList<Identity.User> Users_LastEducationDegree { get; private set; }
	public virtual System.Collections.Generic.IList<Identity.User> Users_MilitaryServiceStatus { get; private set; }
	public virtual System.Collections.Generic.IList<LocalizedBaseTableItem> LocalizedBaseTableItems { get; private set; }

	#endregion /Collections
}
