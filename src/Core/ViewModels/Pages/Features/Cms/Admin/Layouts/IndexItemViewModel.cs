namespace ViewModels.Pages.Features.Cms.Admin.Layouts;

public class IndexItemViewModel : object
{
	#region Constructor
	public IndexItemViewModel() : base()
	{
		Title = string.Empty;
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }

	public bool IsActive { get; set; }

	public Domain.Features.Cms.Enums.LayoutTypeEnum Type { get; set; }

	public int Ordering { get; set; }

	public string Title { get; set; }

	public Domain.Features.Cms.Enums.ThemeEnum? Theme { get; set; }

	public int PageCount { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
