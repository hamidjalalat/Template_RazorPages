namespace ViewModels.Pages.Features.Cms;

public class SearchPostViewModel : object
{
	#region Constructor
	public SearchPostViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid? TypeId { get; set; }
	/// <summary>
	/// دسته‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PostType))]
	public System.Guid? TypeId { get; set; }
	#endregion /public System.Guid? TypeId { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? TypesSelectList { get; set; }

	#region public System.Guid? CategoryId { get; set; }
	/// <summary>
	/// طبقه‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]
	public System.Guid? CategoryId { get; set; }
	#endregion /public System.Guid? CategoryId { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? CategoriesSelectList { get; set; }

	#region public string? Body { get; set; }
	/// <summary>
	/// متن اصلی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Body))]
	public string? Body { get; set; }
	#endregion /public string? Body { get; set; }

	#endregion /Properties
}
