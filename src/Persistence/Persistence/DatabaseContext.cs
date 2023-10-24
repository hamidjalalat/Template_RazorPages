using System.Linq;
using Persistence.Extensions;

namespace Persistence;

public class DatabaseContext :
	Microsoft.EntityFrameworkCore.DbContext
{
	#region Constructor
	public DatabaseContext(Microsoft.EntityFrameworkCore
		.DbContextOptions<DatabaseContext> options) : base(options: options)
	{
		// تا قبل از اولین نسخه اصلی
		Database.EnsureCreated();

		// نوشتن دستورات ذیل کامل غلط است
		// لااقل در اولین باری که بانک‌اطلاعاتی
		// می‌خواهد ایجاد شود، کار نمی‌کند
		//// using Microsoft.EntityFrameworkCore;
		//if (Database.GetAppliedMigrations().Any())
		//{
		//	// using Microsoft.EntityFrameworkCore;
		//	Database.Migrate();
		//}

		// using Microsoft.EntityFrameworkCore;
		//Database.Migrate();
	}
	#endregion /Constructor

	#region Properties

	#region CMS Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.Page> Pages { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.Post> Posts { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.Slide> Slides { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.Layout> Layouts { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.PostType> PostTypes { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.MenuItem> MenuItems { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.PostComment> PostComments { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.PostCategory> PostCategories { get; set; }

	#endregion /CMS Feature

	#region HRM Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Hrm.UserSite> UserSites { get; set; }

	#endregion /HRM Feature

	#region Common Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.Culture> Cultures { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.BaseTable> BaseTables { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.HtmlSetting> HtmlSettings { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.BaseTableItem> BaseTableItems { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.ApplicationSetting> ApplicationSettings { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.LocalizedBaseTable> LocalizedBaseTables { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.LocalizedMailSetting> LocalizedMailSettings { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.LocalizedBaseTableItem> LocalizedBaseTableItems { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.LocalizedHomePageSetting> LocalizedHomePageSettings { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.LocalizedApplicationSetting> LocalizedApplicationSettings { get; set; }

	#endregion /Common Feature

	#region Identity Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.User> Users { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.LoginLog> LoginLogs { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.LocalizedUser> LocalizedUsers { get; set; }

	#endregion /Identity Feature

	#endregion /Properties

	#region Methods

	#region OnModelCreating()
	protected override void OnModelCreating
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly
			(assembly: typeof(DatabaseContext).Assembly);

		if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
		{
			// SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
			// here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
			// To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
			// use the DateTimeOffsetToBinaryConverter
			// Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
			// This only supports millisecond precision, but should be sufficient for most use cases.
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				var properties =
					entityType.ClrType.GetProperties()
					.Where(current =>
						current.PropertyType == typeof(System.DateTimeOffset) ||
						current.PropertyType == typeof(System.DateTimeOffset?));

				foreach (var property in properties)
				{
					modelBuilder
						.Entity(name: entityType.Name)
						.Property(propertyName: property.Name)
						.HasConversion(converter:
							new Microsoft.EntityFrameworkCore
							.Storage.ValueConversion.DateTimeOffsetToBinaryConverter());
				}
			}
		}

		modelBuilder.Seed();
	}
	#endregion /OnModelCreating()

	#endregion /Methods
}
