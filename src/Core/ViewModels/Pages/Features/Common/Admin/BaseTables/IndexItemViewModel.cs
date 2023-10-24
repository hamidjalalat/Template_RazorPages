namespace ViewModels.Pages.Features.Common.Admin.BaseTables;

public class IndexItemViewModel : object
{
	#region Constructor
	public IndexItemViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }

	public bool IsActive { get; set; }

	public bool IsTestData { get; set; }

	public int Ordering { get; set; }

	public Domain.Features.Common.Enums.BaseTableEnum Code { get; set; }

	public Domain.Features.Common.Enums.BaseTableTypeEnum Type { get; set; }

	public int ItemCount { get; set; }

	public string? Color { get; set; }

	public string? Icon { get; set; }

	public string? Title { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
