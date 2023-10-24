namespace ViewModels.Pages.Features.Cms;

public class PageViewModel : object
{
	#region Constructor
	public PageViewModel() : base()
	{
		Title = string.Empty;
		LayoutName = string.Empty;
	}
	#endregion /Constructor

	#region Properties

	public string Title { get; set; }
	public string? Body { get; set; }
	public string? Description { get; set; }

	public string? LayoutName { get; set; }
	public System.Guid LayoutId { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
