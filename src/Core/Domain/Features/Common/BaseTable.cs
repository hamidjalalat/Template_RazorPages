namespace Domain.Features.Common;

public class BaseTable : Seedwork.ExtendedEntity
{
	#region Constructor
	public BaseTable(Enums.BaseTableEnum
		code, Enums.BaseTableTypeEnum type) : base()
	{
		Code = code;
		Type = type;

		BaseTableItems =
			new System.Collections.Generic.List<BaseTableItem>();

		LocalizedBaseTables =
			new System.Collections.Generic.List<LocalizedBaseTable>();
	}
	#endregion /Constructor

	#region Properties

	#region public Enums.BaseTableEnum Code { get; set; }
	/// <summary>
	/// کد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Code))]
	public Enums.BaseTableEnum Code { get; set; }
	#endregion /public Enums.BaseTableEnum Code { get; set; }

	#region public Enums.BaseTableTypeEnum Type { get; set; }
	/// <summary>
	/// نوع اطلاعات پایه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.BaseTableType))]
	public Enums.BaseTableTypeEnum Type { get; set; }
	#endregion /public Enums.BaseTableTypeEnum Type { get; set; }

	#endregion /Properties

	#region Collections

	public virtual System.Collections.Generic.IList<BaseTableItem> BaseTableItems { get; private set; }
	public virtual System.Collections.Generic.IList<LocalizedBaseTable> LocalizedBaseTables { get; private set; }

	#endregion /Collections
}
