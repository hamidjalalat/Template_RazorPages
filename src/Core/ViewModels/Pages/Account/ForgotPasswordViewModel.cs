namespace ViewModels.Pages.Account;

public class ForgotPasswordViewModel : object
{
	#region Constructor
	public ForgotPasswordViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public string? UsernameOrEmailAddress { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.UsernameOrEmailAddress),
		ResourceType = typeof(Resources.DataDictionary))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.EmailAddress,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Constants.RegularExpression.UsernameOrEmailAddress,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Username))]
	public string? UsernameOrEmailAddress { get; set; }
	#endregion /public string? UsernameOrEmailAddress { get; set; }

	#endregion /Properties
}
