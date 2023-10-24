namespace ViewModels.Pages.Features.Common.Admin.LocalizedApplicationSettings;

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



	#region public string? Copyright { get; set; }
	/// <summary>
	/// کپی‌رایت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Copyright))]
	public string? Copyright { get; set; }
	#endregion /public string? Copyright { get; set; }

	#region public string? ApplicationVersioin { get; set; }
	/// <summary>
	/// نسخه سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Version))]
	public string? ApplicationVersioin { get; set; }
	#endregion /public string? ApplicationVersioin { get; set; }



	#region public string? NavbarBrandText { get; set; }
	/// <summary>
	/// Navbar Brand Text
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Navbar Brand Text")]
	public string? NavbarBrandText { get; set; }
	#endregion /public string? NavbarBrandText { get; set; }

	#region public string? NavbarBrandImageUrl { get; set; }
	/// <summary>
	/// Navbar Brand Image Url
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Navbar Brand Image URL")]
	public string? NavbarBrandImageUrl { get; set; }
	#endregion /public string? NavbarBrandImageUrl { get; set; }

	#endregion /Properties
}
