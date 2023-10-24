namespace ViewModels.Shared;

public class PaginationViewModel : object
{
	#region Constructor
	public PaginationViewModel() : base()
	{
		PageSizes =
			new int[] { 5, 10, 20, 50, 100, 200, 500 };
	}
	#endregion /Constructor

	#region Properties

	#region public int PageSize { get; set; }
	/// <summary>
	/// تعداد اطلاعات در هر صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PageSize))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 1, maximum: 1_000,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int PageSize { get; set; }
	#endregion /public int PageSize { get; set; }

	#region public int PageIndex { get; set; }
	/// <summary>
	/// شماره صفحه جاری
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PageIndex))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 1, maximum: 500,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int PageIndex { get; set; }
	#endregion /public int PageIndex { get; set; }

	#region public int RecordCount { get; set; }
	/// <summary>
	/// تعداد اطلاعات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RecordCount))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 0, maximum: 10_000_000,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int RecordCount { get; set; }
	#endregion /public int RecordCount { get; set; }

	#region public int[] PageSizes { get; set; }
	/// <summary>
	/// سایزهای صفحات
	/// </summary>
	public int[] PageSizes { get; set; }
	#endregion /public int[] PageSizes { get; set; }

	#endregion /Properties

	#region Read Only Properties

	#region public int Skip
	public int Skip
	{
		get
		{
			if (PageIndex < 1)
			{
				PageIndex = 1;
			}

			var result =
				(PageIndex - 1) * PageSize;

			return result;
		}
	}
	#endregion /public int Skip

	#region public int Take
	public int Take
	{
		get
		{
			return PageSize;
		}
	}
	#endregion /public int Take

	#region public int PageCount
	public int PageCount
	{
		get
		{
			var result = System.Convert.ToInt32
				(System.Math.Ceiling(System.Convert.ToDouble(RecordCount) / PageSize));

			return result;
		}
	}
	#endregion /public int PageCount

	#region public int StartIndex
	public int StartIndex
	{
		get
		{
			var result = Skip + 1;

			return result;
		}
	}
	#endregion /public int StartIndex

	#endregion /Read Only Properties
}
