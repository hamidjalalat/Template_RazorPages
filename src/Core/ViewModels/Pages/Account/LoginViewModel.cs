namespace ViewModels.Pages.Account;

public class LoginViewModel : object
{
	#region Constructor
	public LoginViewModel() : base()
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
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.RequiredGeneric))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.EmailAddress,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLengthGeneric))]

	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Constants.RegularExpression.UsernameOrEmailAddress,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.UsernameOrEmailAddress))]
	public string? UsernameOrEmailAddress { get; set; }
	#endregion /public string? UsernameOrEmailAddress { get; set; }

	#region public string? Password { get; set; }
	/// <summary>
	/// گذرواژه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Password),
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
	public string? Password { get; set; }
	#endregion /public string? Password { get; set; }

	#region public bool RememberMe { get; set; }
	/// <summary>
	/// مرا به خاطر داشته باش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.RememberMe),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool RememberMe { get; set; }
	#endregion /public bool RememberMe { get; set; }

	#region public string? ReturnUrl { get; set; }
	/// <summary>
	/// جایی که کاربر تمایل داشته که وارد آن شود
	/// </summary>
	public string? ReturnUrl { get; set; }
	#endregion /public string? ReturnUrl { get; set; }

	#endregion /Properties
}
