namespace Persistence.Extensions;

internal static class ModelBuilderExtensions : object
{
	#region Static Constructor
	static ModelBuilderExtensions()
	{
		// **************************************************
		// *** Cultures *************************************
		// **************************************************
		var persianCultureInfo =
			Domain.Features.Common.CultureEnumHelper.GetByLcid
			(lcid: Domain.Features.Common.Enums.CultureEnum.Persian);

		CulturePersian =
			new Domain.Features.Common.Culture
			(lcid: Domain.Features.Common.Enums.CultureEnum.Persian,
			cultureName: persianCultureInfo.Name,
			nativeName: persianCultureInfo.NativeName)
			{
				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};
		// **************************************************

		// **************************************************
		var englishCultureInfo =
			Domain.Features.Common.CultureEnumHelper.GetByLcid
			(lcid: Domain.Features.Common.Enums.CultureEnum.English);

		CultureEnglish =
			new Domain.Features.Common.Culture
			(lcid: Domain.Features.Common.Enums.CultureEnum.English,
			cultureName: englishCultureInfo.Name,
			nativeName: englishCultureInfo.NativeName)
			{
				IsActive = false,
				Ordering = 10_100,
				Description = null,
			};
		// **************************************************
		// *** /Cultures ************************************
		// **************************************************

		// **************************************************
		// *** Layout ***************************************
		// **************************************************
		LayoutDefault =
			new Domain.Features.Cms.Layout(title: "قالب پیش‌فرض",
			type: Domain.Features.Cms.Enums.LayoutTypeEnum.Default)
			{
				IsActive = true,
			};
		// **************************************************
		// *** /Layout **************************************
		// **************************************************
	}
	#endregion /Static Constructor

	#region Properties

	private static Domain.Features.Cms.Layout LayoutDefault { get; set; }
	private static Domain.Features.Common.Culture CulturePersian { get; set; }
	private static Domain.Features.Common.Culture CultureEnglish { get; set; }

	#endregion /Properties

	#region Methods

	#region Seed()
	public static void Seed
		(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		// تابع ذیل باید اول نوشته شود
		SeedCultures(modelBuilder: modelBuilder);

		SeedBaseTables(modelBuilder: modelBuilder);

		SeedUsers(modelBuilder: modelBuilder);

		SeedLayouts(modelBuilder: modelBuilder);

		SeedPages(modelBuilder: modelBuilder);

		SeedMenuItems(modelBuilder: modelBuilder);

		SeedPostTypes(modelBuilder: modelBuilder);

		SeedApplicationSettings(modelBuilder: modelBuilder);
	}
	#endregion /Seed()

	#region SeedCultures()
	private static void SeedCultures
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Domain.Features
			.Common.Culture>().HasData(data: CulturePersian);

		modelBuilder.Entity<Domain.Features
			.Common.Culture>().HasData(data: CultureEnglish);
	}
	#endregion /SeedCultures()

	#region SeedBaseTables()
	private static void SeedBaseTables
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		// **************************************************
		SeedBaseTables_Roles
			(modelBuilder: modelBuilder);

		SeedBaseTables_Genders
			(modelBuilder: modelBuilder);

		SeedBaseTables_Marriages
			(modelBuilder: modelBuilder);
		// **************************************************

		Domain.Features.Common.BaseTable entity;
		Domain.Features.Common.LocalizedBaseTable localizedEntity;

		// **************************************************
		// **************************************************
		// **************************************************
		entity =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.Language,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.NotEnum)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features.Common.BaseTable>().HasData(data: entity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CulturePersian.Id,
			baseTableId: entity.Id, title: "زبان");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CultureEnglish.Id,
			baseTableId: entity.Id, title: "Language");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		entity =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.Religion,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.NotEnum)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features.Common.BaseTable>().HasData(data: entity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CulturePersian.Id,
			baseTableId: entity.Id, title: "مذهب");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CultureEnglish.Id,
			baseTableId: entity.Id, title: "Religion");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		entity =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.LanguageLevel,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.NotEnum)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features.Common.BaseTable>().HasData(data: entity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CulturePersian.Id,
			baseTableId: entity.Id, title: "سطح زبان");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CultureEnglish.Id,
			baseTableId: entity.Id, title: "Language Level");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		entity =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.SocialNetwork,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.NotEnum)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features.Common.BaseTable>().HasData(data: entity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CulturePersian.Id,
			baseTableId: entity.Id, title: "شبکه اجتماعی");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CultureEnglish.Id,
			baseTableId: entity.Id, title: "Social Network");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		entity =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.EducationDegree,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.NotEnum)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features.Common.BaseTable>().HasData(data: entity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CulturePersian.Id,
			baseTableId: entity.Id, title: "مدرک تحصیلی");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CultureEnglish.Id,
			baseTableId: entity.Id, title: "Education Degree");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		entity =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.MilitaryServiceStatus,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.NotEnum)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features.Common.BaseTable>().HasData(data: entity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CulturePersian.Id,
			baseTableId: entity.Id, title: "وضعیت نظام‌وظیفه");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************

		// **************************************************
		localizedEntity =
			new(cultureId: CultureEnglish.Id,
			baseTableId: entity.Id, title: "Military Service Status");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedEntity);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /SeedBaseTables()

	#region SeedBaseTables_Roles()
	private static void SeedBaseTables_Roles
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		Domain.Features.Common.BaseTable baseTable;
		Domain.Features.Common.LocalizedBaseTable localizedBaseTable;

		// **************************************************
		// **************************************************
		// **************************************************
		baseTable =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.Role,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.Enum)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features.Common.BaseTable>().HasData(data: baseTable);
		// **************************************************

		// **************************************************
		localizedBaseTable =
			new(cultureId: CulturePersian.Id,
			baseTableId: baseTable.Id, title: "نقش کاربر");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedBaseTable);
		// **************************************************

		// **************************************************
		localizedBaseTable =
			new(cultureId: CultureEnglish.Id,
			baseTableId: baseTable.Id, title: "Role");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedBaseTable);
		// **************************************************
		// **************************************************
		// **************************************************

		Domain.Features.Common.BaseTableItem baseTableItem;
		Domain.Features.Common.LocalizedBaseTableItem localizedBaseTableItem;

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName:
			nameof(Domain.Features.Identity.Enums.RoleEnum.SimpleUser))
			{
				IsActive = true,
				Ordering = 1000,
				Code = (int)Domain.Features.Identity.Enums.RoleEnum.SimpleUser,
			};

		baseTableItem.SetId(id:
			Constants.BaseTableItem.Role.SimpleUser);

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CulturePersian.Id,
			baseTableItemId: baseTableItem.Id, title: "کاربر معمولی");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CultureEnglish.Id,
			baseTableItemId: baseTableItem.Id, title: "Simple User");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName:
			nameof(Domain.Features.Identity.Enums.RoleEnum.SpecialUser))
			{
				IsActive = true,
				Ordering = 2000,
				Code = (int)Domain.Features.Identity.Enums.RoleEnum.SpecialUser,
			};

		baseTableItem.SetId(id:
			Constants.BaseTableItem.Role.SpecialUser);

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CulturePersian.Id,
			baseTableItemId: baseTableItem.Id, title: "کاربر ویژه");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CultureEnglish.Id,
			baseTableItemId: baseTableItem.Id, title: "Special User");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName:
			nameof(Domain.Features.Identity.Enums.RoleEnum.Supervisor))
			{
				IsActive = true,
				Ordering = 3000,
				Code = (int)Domain.Features.Identity.Enums.RoleEnum.Supervisor,
			};

		baseTableItem.SetId(id:
			Constants.BaseTableItem.Role.Supervisor);

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CulturePersian.Id,
			baseTableItemId: baseTableItem.Id, title: "سرپرست سامانه");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CultureEnglish.Id,
			baseTableItemId: baseTableItem.Id, title: "Supervisor");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName:
			nameof(Domain.Features.Identity.Enums.RoleEnum.Administrator))
			{
				IsActive = true,
				Ordering = 4000,
				Code = (int)Domain.Features.Identity.Enums.RoleEnum.Administrator,
			};

		baseTableItem.SetId(id:
			Constants.BaseTableItem.Role.Administrator);

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CulturePersian.Id,
			baseTableItemId: baseTableItem.Id, title: "مدیر سامانه");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CultureEnglish.Id,
			baseTableItemId: baseTableItem.Id, title: "Administrator");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName:
			nameof(Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner))
			{
				IsActive = true,
				Ordering = 5000,
				Code = (int)Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner,
			};

		baseTableItem.SetId(id:
			Constants.BaseTableItem.Role.ApplicationOwner);

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CulturePersian.Id,
			baseTableItemId: baseTableItem.Id, title: "مالک سامانه");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CultureEnglish.Id,
			baseTableItemId: baseTableItem.Id, title: "Application Owner");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName:
			nameof(Domain.Features.Identity.Enums.RoleEnum.Programmer))
			{
				IsActive = true,
				Ordering = 6000,
				Code = (int)Domain.Features.Identity.Enums.RoleEnum.Programmer,
			};

		baseTableItem.SetId(id:
			Constants.BaseTableItem.Role.Programmer);

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CulturePersian.Id,
			baseTableItemId: baseTableItem.Id, title: "برنامه‌نویس سامانه");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CultureEnglish.Id,
			baseTableItemId: baseTableItem.Id, title: "Programmer");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /SeedBaseTables_Roles()

	#region SeedBaseTables_Genders()
	private static void SeedBaseTables_Genders
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		Domain.Features.Common.BaseTable baseTable;
		Domain.Features.Common.LocalizedBaseTable localizedBaseTable;

		// **************************************************
		// **************************************************
		// **************************************************
		baseTable =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.Gender,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.NotEnum)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features.Common.BaseTable>().HasData(data: baseTable);
		// **************************************************

		// **************************************************
		localizedBaseTable =
			new(cultureId: CulturePersian.Id,
			baseTableId: baseTable.Id, title: "جنسیت");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedBaseTable);
		// **************************************************

		// **************************************************
		localizedBaseTable =
			new(cultureId: CultureEnglish.Id,
			baseTableId: baseTable.Id, title: "Gender");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedBaseTable);
		// **************************************************
		// **************************************************
		// **************************************************

		Domain.Features.Common.BaseTableItem baseTableItem;
		Domain.Features.Common.LocalizedBaseTableItem localizedBaseTableItem;

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName: "Female")
			{
				IsActive = true,
				Ordering = 1000,
			};

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CulturePersian.Id,
			baseTableItemId: baseTableItem.Id, title: "خانم");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CultureEnglish.Id,
			baseTableItemId: baseTableItem.Id, title: "Female");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName: "Male")
			{
				IsActive = true,
				Ordering = 2000,
			};

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CulturePersian.Id,
			baseTableItemId: baseTableItem.Id, title: "آقا");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CultureEnglish.Id,
			baseTableItemId: baseTableItem.Id, title: "Male");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /SeedBaseTables_Genders()

	#region SeedBaseTables_Marriages
	private static void SeedBaseTables_Marriages
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		Domain.Features.Common.BaseTable baseTable;
		Domain.Features.Common.LocalizedBaseTable localizedBaseTable;

		// **************************************************
		// **************************************************
		// **************************************************
		baseTable =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.MaritalStatus,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.NotEnum)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features.Common.BaseTable>().HasData(data: baseTable);
		// **************************************************

		// **************************************************
		localizedBaseTable =
			new(cultureId: CulturePersian.Id,
			baseTableId: baseTable.Id, title: "وضعیت تاهل");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedBaseTable);
		// **************************************************

		// **************************************************
		localizedBaseTable =
			new(cultureId: CultureEnglish.Id,
			baseTableId: baseTable.Id, title: "Marital Status");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTable>().HasData(data: localizedBaseTable);
		// **************************************************
		// **************************************************
		// **************************************************

		Domain.Features.Common.BaseTableItem baseTableItem;
		Domain.Features.Common.LocalizedBaseTableItem localizedBaseTableItem;

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName: "Single")
			{
				IsActive = true,
				Ordering = 1000,
			};

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CulturePersian.Id,
			baseTableItemId: baseTableItem.Id, title: "مجرد");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CultureEnglish.Id,
			baseTableItemId: baseTableItem.Id, title: "Single");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName: "Married")
			{
				IsActive = true,
				Ordering = 2000,
			};

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CulturePersian.Id,
			baseTableItemId: baseTableItem.Id, title: "متاهل");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************

		// **************************************************
		localizedBaseTableItem =
			new(cultureId: CultureEnglish.Id,
			baseTableItemId: baseTableItem.Id, title: "Married");

		modelBuilder.Entity<Domain.Features.Common
			.LocalizedBaseTableItem>().HasData(data: localizedBaseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /SeedBaseTables_Marriages

	#region SeedUsers()
	private static void SeedUsers
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		Domain.Features.Identity.LocalizedUser localizedUser;

		// **************************************************
		// **************************************************
		// **************************************************
		var userProgrammer =
			new Domain.Features.Identity.User
			(emailAddress: "dariushT@gmail.com", registerIP: "::1",
			registerType: Domain.Features.Identity.Enums.AuthenticationTypeEnum.Internal)
			{
				Ordering = 1000,

				IsActive = true,
				IsVerified = true,
				IsUndeletable = true,
				IsProfilePublic = true,
				IsEmailAddressVerified = true,
				IsVisibleInContactUsPage = true,
				IsCellPhoneNumberVerified = true,

				Username = "dariush",
				CellPhoneNumber = "00989121087461",

				ImageUrl = "/images/dariush.png",
				CoverImageUrl = "/images/dariush_cover.png",

				RoleId =
					Constants.BaseTableItem.Role.Programmer,
			};

		userProgrammer
			.SetPassword(password: "1234512345");

		modelBuilder.Entity<Domain.Features
			.Identity.User>().HasData(data: userProgrammer);
		// **************************************************

		// **************************************************
		localizedUser =
			new Domain.Features.Identity
			.LocalizedUser(cultureId: CulturePersian.Id,
			userId: userProgrammer.Id, firstName: "داریوش", lastName: "تصدیقی");

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedUser>().HasData(data: localizedUser);
		// **************************************************

		// **************************************************
		localizedUser =
			new Domain.Features.Identity
			.LocalizedUser(cultureId: CultureEnglish.Id,
			userId: userProgrammer.Id, firstName: "Dariush", lastName: "Tasdighi")
			{
				TitleInContactUsPage = "Site Programmer",
			};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedUser>().HasData(data: localizedUser);
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /SeedUsers()

	#region SeedLayouts()
	private static void SeedLayouts
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		// **************************************************
		modelBuilder.Entity<Domain.Features
			.Cms.Layout>().HasData(data: LayoutDefault);
		// **************************************************

		// **************************************************
		var layoutEmpty =
			new Domain.Features.Cms.Layout(title: "قالب خالی",
			type: Domain.Features.Cms.Enums.LayoutTypeEnum.Empty)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Layout>().HasData(data: layoutEmpty);
		// **************************************************
	}
	#endregion /SeedLayouts()

	#region SeedPages()
	private static void SeedPages
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		Domain.Features.Cms.Page page;

		// **************************************************
		page =
			new Domain.Features.Cms.Page
			(cultureId: CulturePersian.Id,
			layoutId: LayoutDefault.Id, name: "about", title: "درباره ما")
			{
				IsActive = true,

				Body = "<h1>درباره ما</h1>",
			};

		modelBuilder.Entity<Domain
			.Features.Cms.Page>().HasData(data: page);
		// **************************************************

		// **************************************************
		page =
			new Domain.Features.Cms.Page
			(cultureId: CultureEnglish.Id,
			layoutId: LayoutDefault.Id, name: "about", title: "About us")
			{
				IsActive = true,

				Body = "<h1>About us</h1>",
			};

		modelBuilder.Entity<Domain
			.Features.Cms.Page>().HasData(data: page);
		// **************************************************

		// **************************************************
		page =
			new Domain.Features.Cms.Page
			(cultureId: CulturePersian.Id,
			layoutId: LayoutDefault.Id, name: "help", title: "راهنما")
			{
				IsActive = true,

				Body = "<h1>راهنمای سامانه</h1>",
			};

		modelBuilder.Entity<Domain
			.Features.Cms.Page>().HasData(data: page);
		// **************************************************

		// **************************************************
		page =
			new Domain.Features.Cms.Page
			(cultureId: CultureEnglish.Id,
			layoutId: LayoutDefault.Id, name: "help", title: "Help")
			{
				IsActive = true,

				Body = "<h1>Help</h1>",
			};

		modelBuilder.Entity<Domain
			.Features.Cms.Page>().HasData(data: page);
		// **************************************************

		// **************************************************
		page = new Domain.Features.Cms.Page(cultureId: CulturePersian.Id,
			layoutId: LayoutDefault.Id, name: "privacy_policy", title: "سیاست‌های حفظ حریم خصوصی")
		{
			IsActive = true,

			Body = "<h1>سیاست‌های حفظ حریم خصوصی</h1>",
		};

		modelBuilder.Entity<Domain
			.Features.Cms.Page>().HasData(data: page);
		// **************************************************

		// **************************************************
		page = new Domain.Features.Cms.Page(cultureId: CultureEnglish.Id,
			layoutId: LayoutDefault.Id, name: "privacy_policy", title: "Privacy Policy")
		{
			IsActive = true,

			Body = "<h1>Privacy Policy</h1>",
		};

		modelBuilder.Entity<Domain
			.Features.Cms.Page>().HasData(data: page);
		// **************************************************

		// **************************************************
		page = new Domain.Features.Cms.Page
			(cultureId: CulturePersian.Id, layoutId: LayoutDefault.Id,
			name: "terms_of_service", title: "شرایط استفاده از خدمات سامانه")
		{
			IsActive = true,

			Body = "<h1>شرایط استفاده از خدمات سامانه</h1>",
		};

		modelBuilder.Entity<Domain
			.Features.Cms.Page>().HasData(data: page);
		// **************************************************

		// **************************************************
		page = new Domain.Features.Cms.Page(cultureId: CultureEnglish.Id,
			layoutId: LayoutDefault.Id, name: "terms_of_service", title: "Terms of Service")
		{
			IsActive = true,

			Body = "<h1>Terms of Service</h1>",
		};

		modelBuilder.Entity<Domain
			.Features.Cms.Page>().HasData(data: page);
		// **************************************************
	}
	#endregion /SeedPages()

	#region SeedMenuItems()
	private static void SeedMenuItems
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		Domain.Features.Cms.MenuItem menuItem;

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CulturePersian.Id, title: "درباره ما")
		{
			Ordering = 9_000,
			IsVisible = false,
			IsDisabled = false,
			NavigationUrl = "/page/fa-ir/about",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CultureEnglish.Id, title: "About")
		{
			Ordering = 9_000,
			IsVisible = false,
			IsDisabled = false,
			NavigationUrl = "/page/en-us/about",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CulturePersian.Id, title: "راهنما")
		{
			Ordering = 9_100,
			IsVisible = false,
			IsDisabled = true,
			NavigationUrl = "/page/fa-ir/help",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CultureEnglish.Id, title: "Help")
		{
			Ordering = 9_100,
			IsVisible = false,
			IsDisabled = true,
			NavigationUrl = "/page/en-us/help",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CulturePersian.Id, title: "کاربران")
		{
			Ordering = 9_200,
			IsVisible = false,
			IsDisabled = false,
			NavigationUrl = "/users/fa-ir/",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CultureEnglish.Id, title: "users")
		{
			Ordering = 9_200,
			IsVisible = false,
			IsDisabled = false,
			NavigationUrl = "/users/en-us/",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CulturePersian.Id, title: "دسته‌بندی‌ها")
		{
			Ordering = 9_300,
			IsVisible = false,
			IsDisabled = false,
			NavigationUrl = "/types/fa-ir/",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CultureEnglish.Id, title: "Types")
		{
			Ordering = 9_300,
			IsVisible = false,
			IsDisabled = false,
			NavigationUrl = "/types/en-us/",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CulturePersian.Id, title: "طبقه‌بندی‌ها")
		{
			Ordering = 9_400,
			IsVisible = false,
			IsDisabled = false,
			NavigationUrl = "/categories/fa-ir/",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CultureEnglish.Id, title: "Categories")
		{
			Ordering = 9_400,
			IsVisible = false,
			IsDisabled = false,
			NavigationUrl = "/categories/en-us/",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CulturePersian.Id, title: "مطالب")
		{
			Ordering = 9_500,
			IsVisible = false,
			IsDisabled = false,
			NavigationUrl = "/posts/fa-ir/",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************

		// **************************************************
		menuItem = new Domain.Features.Cms.MenuItem
			(cultureId: CultureEnglish.Id, title: "Posts")
		{
			Ordering = 9_500,
			IsVisible = false,
			IsDisabled = false,
			NavigationUrl = "/posts/en-us/",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: menuItem);
		// **************************************************
	}
	#endregion /SeedMenuItems()

	#region SeedPostTypes()
	private static void SeedPostTypes
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		Domain.Features.Cms.PostType postType;

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CulturePersian.Id, name: "News", title: "خبر")
		{
			IsActive = true,

			//ImageUrl = "/images/types/news.fa-ir.png",
			//CoverImageUrl = "/images/types/news_cover.fa-ir.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CultureEnglish.Id, name: "News", title: "News")
		{
			IsActive = true,

			//ImageUrl = "/images/types/news.en-us.png",
			//CoverImageUrl = "/images/types/news_cover.en-us.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CulturePersian.Id, name: "Article", title: "مقاله")
		{
			IsActive = true,

			//ImageUrl = "/images/types/article.fa-ir.png",
			//CoverImageUrl = "/images/types/article_cover.fa-ir.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CultureEnglish.Id, name: "Article", title: "Article")
		{
			IsActive = true,

			//ImageUrl = "/images/types/article.en-us.png",
			//CoverImageUrl = "/images/types/article_cover.en-us.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CulturePersian.Id, name: "Product", title: "محصول")
		{
			IsActive = true,

			//ImageUrl = "/images/types/product.fa-ir.png",
			//CoverImageUrl = "/images/types/product_cover.fa-ir.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CultureEnglish.Id, name: "Product", title: "Product")
		{
			IsActive = true,

			//ImageUrl = "/images/types/product.en-us.png",
			//CoverImageUrl = "/images/types/product_cover.en-us.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CulturePersian.Id, name: "Advertisement", title: "تبلیغ")
		{
			IsActive = true,

			//ImageUrl = "/images/types/advertisement.fa-ir.png",
			//CoverImageUrl = "/images/types/advertisement_cover.fa-ir.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CultureEnglish.Id, name: "Advertisement", title: "Advertisement")
		{
			IsActive = true,

			//ImageUrl = "/images/types/advertisement.en-us.png",
			//CoverImageUrl = "/images/types/advertisement_cover.en-us.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CulturePersian.Id, name: "Course", title: "دوره آموزشی")
		{
			IsActive = true,

			//ImageUrl = "/images/types/course.fa-ir.png",
			//CoverImageUrl = "/images/types/course_cover.fa-ir.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CultureEnglish.Id, name: "Course", title: "Course")
		{
			IsActive = true,

			//ImageUrl = "/images/types/course.en-us.png",
			//CoverImageUrl = "/images/types/course_cover.en-us.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CulturePersian.Id, name: "Recruitment", title: "آگهی استخدام")
		{
			IsActive = true,

			//ImageUrl = "/images/types/recruitment.fa-ir.png",
			//CoverImageUrl = "/images/types/recruitment_cover.fa-ir.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CultureEnglish.Id, name: "Recruitment", title: "Recruitment")
		{
			IsActive = true,

			//ImageUrl = "/images/types/recruitment.en-us.png",
			//CoverImageUrl = "/images/types/recruitment_cover.en-us.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CulturePersian.Id, name: "Movie", title: "فیلم")
		{
			IsActive = true,

			//ImageUrl = "/images/types/recruitment.fa-ir.png",
			//CoverImageUrl = "/images/types/recruitment_cover.fa-ir.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CultureEnglish.Id, name: "Movie", title: "Movie")
		{
			IsActive = true,

			//ImageUrl = "/images/types/recruitment.en-us.png",
			//CoverImageUrl = "/images/types/recruitment_cover.en-us.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CulturePersian.Id, name: "TV_Series", title: "سریال")
		{
			IsActive = true,

			//ImageUrl = "/images/types/recruitment.fa-ir.png",
			//CoverImageUrl = "/images/types/recruitment_cover.fa-ir.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************

		// **************************************************
		postType = new Domain.Features.Cms.PostType
			(cultureId: CultureEnglish.Id, name: "TV_Series", title: "TV Series")
		{
			IsActive = true,

			//ImageUrl = "/images/types/recruitment.en-us.png",
			//CoverImageUrl = "/images/types/recruitment_cover.en-us.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: postType);
		// **************************************************
		// *** /Post Types **********************************
		// **************************************************
	}
	#endregion /SeedPostTypes()

	#region SeedApplicationSettings()
	private static void SeedApplicationSettings
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		var applicationSetting = new Domain.Features
			.Common.ApplicationSetting(defaultCultureId: CulturePersian.Id);

		modelBuilder.Entity<Domain.Features
			.Common.ApplicationSetting>().HasData(data: applicationSetting);
	}
	#endregion /SeedApplicationSettings()

	#endregion /Methods
}
