namespace ViewModels.Pages.Features.Hrm.User.UserSites;

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

	public int Ordering { get; set; }

	public string? SiteUrl { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
