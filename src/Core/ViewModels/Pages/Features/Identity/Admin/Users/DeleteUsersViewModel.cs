namespace ViewModels.Pages.Features.Identity.Admin.Users;

public class DeleteUsersViewModel : object
{
	#region Constructor
	public DeleteUsersViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public int Value { get; set; }
	/// <summary>
	/// مقدار
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Value))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 1, maximum: 60,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int Value { get; set; }
	#endregion /public int Value { get; set; }

	#region public Domain.Features.Common.Enums.TimeUnitEnum TimeUnit { get; set; }
	/// <summary>
	/// واحد زمانی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.TimeUnit))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public Domain.Features.Common.Enums.TimeUnitEnum TimeUnit { get; set; }
	#endregion /public Domain.Features.Common.Enums.TimeUnitEnum TimeUnit { get; set; }

	#endregion /Properties
}
