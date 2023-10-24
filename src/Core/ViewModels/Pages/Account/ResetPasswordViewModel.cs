namespace ViewModels.Pages.Account;

public class ResetPasswordViewModel : object
{
	#region Constructor
	public ResetPasswordViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid Key { get; set; }
	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid Key { get; set; }
	#endregion /public System.Guid Key { get; set; }

	#region public string? NewPassword { get; set; }
	/// <summary>
	/// گذرواژه جدید
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.NewPassword),
		ResourceType = typeof(Resources.DataDictionary))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.Password,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Constants.RegularExpression.Password,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Password))]

	[System.ComponentModel.DataAnnotations.DataType
		(dataType: System.ComponentModel.DataAnnotations.DataType.Password)]
	public string? NewPassword { get; set; }
	#endregion /public string? NewPassword { get; set; }

	#region public string? ConfirmNewPassword { get; set; }
	/// <summary>
	/// تکرار گذرواژه جدید
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.ConfirmNewPassword),
		ResourceType = typeof(Resources.DataDictionary))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.Compare
		(otherProperty: nameof(NewPassword),
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Compare))]

	[System.ComponentModel.DataAnnotations.DataType
		(dataType: System.ComponentModel.DataAnnotations.DataType.Password)]
	public string? ConfirmNewPassword { get; set; }
	#endregion /public string? ConfirmNewPassword { get; set; }

	#endregion /Properties
}
