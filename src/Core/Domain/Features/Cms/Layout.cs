namespace Domain.Features.Cms;

public class Layout :
	Seedwork.Entity,
	Dtat.Seedwork.Abstractions.IEntityHasIsActive,
	Dtat.Seedwork.Abstractions.IEntityHasOrdering,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public Layout(string title,
		Enums.LayoutTypeEnum type) : base()
	{
		Type = type;
		Title = title;

		Ordering = 10_000;

		UpdateDateTime = InsertDateTime;

		Pages = new System
			.Collections.Generic.List<Page>();
	}
	#endregion /Constructor

	#region Properties

	#region public bool IsActive { get; set; }
	/// <summary>
	/// وضعیت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }
	#endregion /public bool IsActive { get; set; }



	#region public int Ordering { get; set; }
	/// <summary>
	/// چیدمان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Ordering))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 1, maximum: 100_000,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int Ordering { get; set; }
	#endregion /public int Ordering { get; set; }



	#region public Enums.ThemeEnum? Theme { get; set; }
	/// <summary>
	/// تم
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Theme))]
	public Enums.ThemeEnum? Theme { get; set; }
	#endregion /public Enums.ThemeEnum? Theme { get; set; }

	#region public Enums.LayoutTypeEnum Type { get; private set; }
	/// <summary>
	/// نوع
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Type))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public Enums.LayoutTypeEnum Type { get; private set; }
	#endregion /public Enums.LayoutTypeEnum Type { get; private set; }



	#region public string Title { get; set; }
	/// <summary>
	/// عنوان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string Title { get; set; }
	#endregion /public string Title { get; set; }

	#region public string? TopBody { get; set; }
	/// <summary>
	/// Top Body
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Top Body")]
	public string? TopBody { get; set; }
	#endregion /public string? TopBody { get; set; }

	#region public string? StartBody { get; set; }
	/// <summary>
	/// Start Body
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Start Body")]
	public string? StartBody { get; set; }
	#endregion /public string? StartBody { get; set; }

	#region public string? EndBody { get; set; }
	/// <summary>
	/// End Body
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "End Body")]
	public string? EndBody { get; set; }
	#endregion /public string? EndBody { get; set; }

	#region public string? BottomBody { get; set; }
	/// <summary>
	/// Bottom Body
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Bottom Body")]
	public string? BottomBody { get; set; }
	#endregion /public string? BottomBody { get; set; }



	#region public string? StartCssClass { get; set; }
	/// <summary>
	/// Start CSS Class
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Start CSS Class")]
	public string? StartCssClass { get; set; }
	#endregion /public string? StartCssClass { get; set; }

	#region public string? MainCssClass { get; set; }
	/// <summary>
	/// Main CSS Class
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Main CSS Class")]
	public string? MainCssClass { get; set; }
	#endregion /public string? MainCssClass { get; set; }

	#region public string? EndCssClass { get; set; }
	/// <summary>
	/// End CSS Class
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "End CSS Class")]
	public string? EndCssClass { get; set; }
	#endregion /public string? EndCssClass { get; set; }

	#region public string? ContainerCssClass { get; set; }
	/// <summary>
	/// Container CSS Class
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Container CSS Class")]
	public string? ContainerCssClass { get; set; }
	#endregion /public string? ContainerCssClass { get; set; }



	#region public string? CustomScripts { get; set; }
	/// <summary>
	/// Custom Scripts
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Custom Scripts")]
	public string? CustomScripts { get; set; }
	#endregion /public string? CustomScripts { get; set; }

	#region public string? CustomStyleSheets { get; set; }
	/// <summary>
	/// Custom Style Sheets
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Custom Style Sheets")]
	public string? CustomStyleSheets { get; set; }
	#endregion /public string? CustomStyleSheets { get; set; }



	#region public bool DisplayDefaultMenu1 { get; set; }
	/// <summary>
	/// Display Menu 1
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Display Default Menu 1")]
	public bool DisplayDefaultMenu1 { get; set; }
	#endregion /public bool DisplayDefaultMenu1 { get; set; }

	#region public bool DisplayDefaultMenu2 { get; set; }
	/// <summary>
	/// Display Default Menu 2
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Display Default Menu 2")]
	public bool DisplayDefaultMenu2 { get; set; }
	#endregion /public bool DisplayDefaultMenu2 { get; set; }

	#region public bool DisplayDefaultMenu3 { get; set; }
	/// <summary>
	/// Display Default Menu 3
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Display Default Menu 3")]
	public bool DisplayDefaultMenu3 { get; set; }
	#endregion /public bool DisplayDefaultMenu3 { get; set; }

	#region public bool DisplayDefaultFooter { get; set; }
	/// <summary>
	/// Display Default Footer
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Display Default Footer")]
	public bool DisplayDefaultFooter { get; set; }
	#endregion /public bool DisplayDefaultFooter { get; set; }



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

	#region Read Only Properties

	#region public string ThemeName { get; }
	public string? ThemeName
	{
		get
		{
			var result =
				Theme?.ToString().ToLower();

			return result;
		}
	}
	#endregion /public string ThemeName { get; }

	#region public string LayoutName { get; }
	public string LayoutName
	{
		get
		{
			var result = $"_{Type}";

			return result;
		}
	}
	#endregion /public string LayoutName { get; }

	#region public string DisplayName { get; }
	public string DisplayName
	{
		get
		{
			var result = $"{Title}";

			if (IsActive == false)
			{
				result += $" ({Resources.DataDictionary.Inactive})";
			}

			return result;
		}
	}
	#endregion /public string DisplayName { get; }

	#endregion /Read Only Properties

	#region Methods

	#region SetUpdateDateTime()
	public void SetUpdateDateTime()
	{
		UpdateDateTime = Dtat.DateTime.Now;
	}
	#endregion /SetUpdateDateTime()

	#endregion /Methods

	#region Collections

	public virtual System.Collections.Generic.IList<Page> Pages { get; private set; }

	#endregion /Collections
}
