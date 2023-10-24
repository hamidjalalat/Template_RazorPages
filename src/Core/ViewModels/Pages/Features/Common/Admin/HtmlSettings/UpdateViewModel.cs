namespace ViewModels.Pages.Features.Common.Admin.HtmlSettings;

public class UpdateViewModel : object
{
	#region Constructor
	public UpdateViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid Id { get; set; }
	/// <summary>
	/// شناسه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public System.Guid Id { get; set; }
	#endregion /public System.Guid Id { get; set; }



	#region public string? DisplayDateTimeFormat { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Display Date Time Format")]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public string? DisplayDateTimeFormat { get; set; }
	//public string DisplayDateTimeFormat { get; set; }
	#endregion /public string? DisplayDateTimeFormat { get; set; }



	#region public int ToastDelayStep { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Toast Delay Step")]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public int ToastDelayStep { get; set; }
	#endregion /public int ToastDelayStep { get; set; }

	#region public int ToastInitialDelay { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Toast Initial Delay")]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public int ToastInitialDelay { get; set; }
	#endregion /public int ToastInitialDelay { get; set; }

	#region public string? ToastCssClasses { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Toast CSS Classes")]
	public string? ToastCssClasses { get; set; }
	#endregion /public string? ToastCssClasses { get; set; }



	#region public string? TableCssClasses { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Table CSS Classes")]
	public string? TableCssClasses { get; set; }
	#endregion /public string? TableCssClasses { get; set; }

	#region public string? TableFooterCssClasses { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Table CSS Classes")]
	public string? TableFooterCssClasses { get; set; }
	#endregion /public string? TableFooterCssClasses { get; set; }

	#region public string? TableHeaderCssClasses { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Table Header CSS Classes")]
	public string? TableHeaderCssClasses { get; set; }
	#endregion /public string? TableHeaderCssClasses { get; set; }



	#region public string? CardsContainerCssClasses { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Cards Container CSS Classes")]
	public string? CardsContainerCssClasses { get; set; }
	#endregion /public string? CardsContainerCssClasses { get; set; }



	#region public string? PicturePreviewExtensions { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Picture Preview Extensions")]
	public string? PicturePreviewExtensions { get; set; }
	#endregion /public string? PicturePreviewExtensions { get; set; }

	#region public string? PermittedFileExtensionsForUploading { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Permitted File Extensions For Uploading")]
	public string? PermittedFileExtensionsForUploading { get; set; }
	#endregion /public string? PermittedFileExtensionsForUploading { get; set; }

	#endregion /Properties
}
