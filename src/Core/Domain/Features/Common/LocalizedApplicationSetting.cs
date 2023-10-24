namespace Domain.Features.Common;

public class LocalizedApplicationSetting :
	Seedwork.LocalizedEntity,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public LocalizedApplicationSetting
		(System.Guid cultureId) : base(cultureId: cultureId)
	{
	}
	#endregion /Constructor

	#region Properties

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
	//[System.ComponentModel.DataAnnotations.Display
	//	(ResourceType = typeof(Resources.DataDictionary),
	//	Name = nameof(Resources.DataDictionary.NavbarBrandText))]
	public string? NavbarBrandText { get; set; }
	#endregion /public string? NavbarBrandText { get; set; }

	#region public string? NavbarBrandImageUrl { get; set; }
	/// <summary>
	/// Navbar Brand Image Url
	/// </summary>
	//[System.ComponentModel.DataAnnotations.Display
	//	(ResourceType = typeof(Resources.DataDictionary),
	//	Name = nameof(Resources.DataDictionary.NavbarBrandText))]
	public string? NavbarBrandImageUrl { get; set; }
	#endregion /public string? NavbarBrandImageUrl { get; set; }

	#endregion /Properties
}
