namespace Domain.Features.Common;

public class HtmlSetting :
	Seedwork.Entity,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public HtmlSetting() : base()
	{
		UpdateDateTime = InsertDateTime;

		DisplayDateTimeFormat =
			"yyyy/MM/dd [HH:mm:ss]";

		ToastDelayStep = 1000;
		ToastInitialDelay = 4000;
		ToastCssClasses = "top-25 end-0 p-3 opacity-50";

		TableHeaderCssClasses = "table-primary";
		TableFooterCssClasses = "table-secondary";

		TableCssClasses =
			"table table-bordered table-sm table-striped table-hover";

		CardsContainerCssClasses =
			"row row-cols-1 row-cols-sm-1 row-cols-md-2 row-cols-lg-3 row-cols-xxl-4 g-4";

		PicturePreviewExtensions =
			".ico,.png,.jpg,.jpeg,.bmp,.gif,.svg";

		PermittedFileExtensionsForUploading =
			".ico,.png,.jpg,.jpeg,.bmp,.gif,.svg,.mp3,.mp4,.pdf,.zip,.rar,.doc,.docx,.txt,.bak,.pdf";
	}
	#endregion /Constructor

	#region Properties

	#region public string DisplayDateTimeFormat { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Display Date Time Format")]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public string DisplayDateTimeFormat { get; set; }
	#endregion /public string DisplayDateTimeFormat { get; set; }



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



	#region public System.DateTimeOffset UpdateDateTime { get; private set; }
	/// <summary>
	/// زمان ویرایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTimeOffset UpdateDateTime { get; private set; }
	#endregion /public System.DateTimeOffset UpdateDateTime { get; private set; }

	#endregion /Properties

	#region Readonly Properties

	#region public string[]? PicturePreviewExtensionsArray { get; }
	/// <summary>
	/// Picture Preview Extensions Array
	/// </summary>
	public string[]? PicturePreviewExtensionsArray
	{
		get
		{
			if(string.IsNullOrWhiteSpace
				(value: PicturePreviewExtensions))
			{
				return null;
			}

			var result =
				PicturePreviewExtensions.Split(separator: ',');

			return result;
		}
	}
	#endregion /public string[]? PicturePreviewExtensionsArray { get; }

	#region public string[]? PermittedFileExtensionsForUploadingArray { get; }
	/// <summary>
	/// Permitted File Extensions For Uploading Array
	/// </summary>
	public string[]? PermittedFileExtensionsForUploadingArray
	{
		get
		{
			if (string.IsNullOrWhiteSpace
				(value: PermittedFileExtensionsForUploading))
			{
				return null;
			}

			var result =
				PermittedFileExtensionsForUploading.Split(separator: ',');

			return result;
		}
	}
	#endregion /public string[]? PermittedFileExtensionsForUploadingArray { get; }

	#endregion /Readonly Properties

	#region Methods

	#region SetUpdateDateTime()
	public void SetUpdateDateTime()
	{
		UpdateDateTime = Dtat.DateTime.Now;
	}
	#endregion /SetUpdateDateTime()

	#endregion /Methods
}
