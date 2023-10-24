namespace ViewModels.Pages.Features.Identity.Admin.LoginLogs;

public class IndexItemViewModel : object
{
	#region Constructor
	public IndexItemViewModel() : base()
	{
		UserIP = string.Empty;
		EmailAddress = string.Empty;
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }
	public System.Guid UserId { get; set; }


	public string UserIP { get; set; }
	public string? LastName { get; set; }
	public string? Username { get; set; }
	public string? FirstName { get; set; }
	public string? RoleTitle { get; set; }
	public string EmailAddress { get; set; }
	public string? GenderPrefix { get; set; }
	public string? CellPhoneNumber { get; set; }


	public System.DateTimeOffset LoginDateTime { get; set; }
	public System.DateTimeOffset? LogoutDateTime { get; set; }

	public Domain.Features.Identity.Enums.AuthenticationTypeEnum LoginType { get; set; }

	#endregion /Properties
}
