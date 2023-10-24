﻿namespace Domain.Features.Common;

public class Culture :
	Seedwork.Entity,
	Dtat.Seedwork.Abstractions.IEntityHasIsActive,
	Dtat.Seedwork.Abstractions.IEntityHasOrdering,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public Culture(Enums.CultureEnum lcid,
		string cultureName, string nativeName) : base()
	{
		Lcid = lcid;
		Ordering = 10_000;

		NativeName = nativeName;
		CultureName = cultureName;

		UpdateDateTime = InsertDateTime;

		// **************************************************
		Pages =
			new System.Collections.Generic.List<Cms.Page>();

		Posts =
			new System.Collections.Generic.List<Cms.Post>();

		Slides =
			new System.Collections.Generic.List<Cms.Slide>();

		MenuItems =
			new System.Collections.Generic.List<Cms.MenuItem>();

		PostTypes =
			new System.Collections.Generic.List<Cms.PostType>();

		PostCategories =
			new System.Collections.Generic.List<Cms.PostCategory>();
		// **************************************************

		// **************************************************
		LocalizedUsers =
			new System.Collections.Generic.List<Identity.LocalizedUser>();
		// **************************************************

		// **************************************************
		LocalizedBaseTables =
			new System.Collections.Generic.List<LocalizedBaseTable>();

		LocalizedMailSettings =
			new System.Collections.Generic.List<LocalizedMailSetting>();

		LocalizedBaseTableItems =
			new System.Collections.Generic.List<LocalizedBaseTableItem>();

		LocalizedHomePageSettings =
			new System.Collections.Generic.List<LocalizedHomePageSetting>();

		LocalizedApplicationSettings =
			new System.Collections.Generic.List<LocalizedApplicationSetting>();
		// **************************************************
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

	#region public Enums.CultureEnum Lcid { get; set; }
	/// <summary>
	/// کد زبان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Lcid))]
	public Enums.CultureEnum Lcid { get; set; }
	#endregion /public Enums.CultureEnum Lcid { get; set; }



	#region public string CultureName { get; set; }
	/// <summary>
	/// نام زبان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CultureName))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string CultureName { get; set; }
	#endregion /public string CultureName { get; set; }

	#region public string NativeName { get; set; }
	/// <summary>
	/// نام بومی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NativeName))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.NativeName,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string NativeName { get; set; }
	#endregion /public string NativeName { get; set; }

	#region public string? Description { get; set; }
	/// <summary>
	/// توضیحات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]
	public string? Description { get; set; }
	#endregion /public string? Description { get; set; }



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

	public string DisplayName
	{
		get
		{
			var result =
				NativeName;

			if (IsActive == false)
			{
				result +=
					$" ({Resources.DataDictionary.Inactive})";
			}

			return result;
		}
	}

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

	public virtual System.Collections.Generic.IList<Cms.Page> Pages { get; private set; }
	public virtual System.Collections.Generic.IList<Cms.Post> Posts { get; private set; }
	public virtual System.Collections.Generic.IList<Cms.Slide> Slides { get; private set; }
	public virtual System.Collections.Generic.IList<Cms.MenuItem> MenuItems { get; private set; }
	public virtual System.Collections.Generic.IList<Cms.PostType> PostTypes { get; private set; }
	public virtual System.Collections.Generic.IList<Cms.PostCategory> PostCategories { get; private set; }

	public virtual System.Collections.Generic.IList<Identity.LocalizedUser> LocalizedUsers { get; private set; }

	public virtual System.Collections.Generic.IList<LocalizedBaseTable> LocalizedBaseTables { get; private set; }
	public virtual System.Collections.Generic.IList<LocalizedMailSetting> LocalizedMailSettings { get; private set; }
	public virtual System.Collections.Generic.IList<LocalizedBaseTableItem> LocalizedBaseTableItems { get; private set; }
	public virtual System.Collections.Generic.IList<LocalizedHomePageSetting> LocalizedHomePageSettings { get; private set; }
	public virtual System.Collections.Generic.IList<LocalizedApplicationSetting> LocalizedApplicationSettings { get; private set; }

	#endregion /Collections
}
