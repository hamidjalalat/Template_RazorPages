namespace ViewModels.Pages.Features.Common.Admin.BaseTableItems;

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

	public string? KeyName { get; set; }

	public int? Code { get; set; }

	public string? Color { get; set; }

	public string? Icon { get; set; }

	public string? Title { get; set; }

	public int ItemCount { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
