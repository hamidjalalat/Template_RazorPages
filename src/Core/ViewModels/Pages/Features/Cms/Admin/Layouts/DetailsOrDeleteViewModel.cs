﻿namespace ViewModels.Pages.Features.Cms.Admin.Layouts;

public class DetailsOrDeleteViewModel : UpdateViewModel
{
	#region Constructor
	public DetailsOrDeleteViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public Domain.Features.Cms.Enums.LayoutTypeEnum Type { get; set; }
	/// <summary>
	/// نوع
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Type))]
	public Domain.Features.Cms.Enums.LayoutTypeEnum Type { get; set; }
	#endregion /public Domain.Features.Cms.Enums.LayoutTypeEnum Type { get; set; }

	#region public System.DateTimeOffset InsertDateTime { get; set; }
	/// <summary>
	/// زمان ایجاد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.InsertDateTime))]
	public System.DateTimeOffset InsertDateTime { get; set; }
	#endregion /public System.DateTimeOffset InsertDateTime { get; set; }

	#region public System.DateTimeOffset UpdateDateTime { get; set; }
	/// <summary>
	/// زمان ویرایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]
	public System.DateTimeOffset UpdateDateTime { get; set; }
	#endregion /public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion Properties
}
