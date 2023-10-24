https://learn.microsoft.com/en-us/dotnet/core/extensions/package-and-deploy-resources?redirectedfrom=MSDN
https://learn.microsoft.com/en-us/dotnet/core/extensions/work-with-resx-files-programmatically?redirectedfrom=MSDN

Role					[0..1]..[0..N]					User

System.Guid Id											System.Guid Id

با نگاه بانک اطلاعاتی

														System.Guid RoleId

با نگاه شیء‌گرایی

														System.Guid RoleId
IList<User> Users										Role Role


IList<User> Users & Role Role -> Navigation Property

														System.Guid? RoleId

virtual IList<User> Users { get; private set; }			virtual Role? Role { get; set; }

- RoleConfiguration:

	builder
		.HasMany(current => current.Users)
		.WithOne(other => other.Role)
		.IsRequired(required: false)
		.HasForeignKey(other => other.RoleId)
		.OnDelete(deleteBehavior:
			Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
		;
-------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------
PageCategory					[1]..[0..N]				Page

System.Guid Id											System.Guid Id

														[Required]
														System.Guid PageCategoryId

														[Required]
virtual IList<Page> Pages { get; private set; }			virtual PageCategory? PageCategory { get; set; }

	builder
		.HasMany(current => current.Pages)
		.WithOne(other => other.PageCategory)
		.IsRequired(required: true)
		.HasForeignKey(other => other.PageCategoryId)
		.OnDelete(deleteBehavior:
			Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
		;
-------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------
User					[1]..[0..N]							Page

System.Guid Id												System.Guid Id

															[Required]
															System.Guid CreatorUserId

															[Required]
virtual IList<Page> CreatedPages { get; private set; }		virtual User? CreatorUser { get; set; }

	builder
		.HasMany(current => current.CreatedPages)
		.WithOne(other => other.CreatorUser)
		.IsRequired(required: true)
		.HasForeignKey(other => other.CreatorUserId)
		.OnDelete(deleteBehavior:
			Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
		;

															System.Guid VerifierUserId

virtual IList<Page> VerifiedPages { get; private set; }		virtual User? VerifierUser { get; set; }

	builder
		.HasMany(current => current.VerifiedPages)
		.WithOne(other => other.VerifierUser)
		.IsRequired(required: false)
		.HasForeignKey(other => other.VerifierUserId)
		.OnDelete(deleteBehavior:
			Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
		;
-------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------
Gmail API:

https://mycodebit.com/send-emails-in-asp-net-core-5-using-gmail-api/
-------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------
Captcha Image:

https://github.com/VahidN/DNTCaptcha.Core
https://edi.wang/post/2018/10/13/generate-captcha-code-aspnet-core
https://codeburst.io/implement-recaptcha-in-asp-net-core-and-razor-pages-eed8ae720933
https://learn.microsoft.com/en-us/aspnet/web-pages/overview/security/using-a-catpcha-to-prevent-automated-programs-bots-from-using-your-aspnet-web-site
-------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------
Tag Helper:

https://bigfont.ca/taghelper-structure/
https://www.yogihosting.com/aspnet-core-custom-tag-helpers/
https://wesleycabus.be/writing-a-custom-taghelper-in-aspnet-5
https://riptutorial.com/asp-net-core/example/11152/custom-tag-helper
https://stackoverflow.com/questions/51904629/how-to-create-custom-tag-helper-containing-other-tag-helpers
-------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------
Migration:
https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/providers?tabs=dotnet-core-cli
https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli

(1)

(1.1)
In 'appsettings.json' and 'appsettings.Development.json' files:

		"DatabaseSettings": {
			"Provider": "MSSqlServer",
			"SQLiteConnectionString": "Data Source=Database\\MySQLite.db",
			"SqlServerConnectionString": "Server=.;Database=DT_CMS;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true;"
		}

(1.2)
Tools
	-> NuGet Package Manager
		-> Package Manager Console

			In 'Default Project' select: 'Persistence' project

Add-Migration InitialCreate -Context DatabaseContext -OutputDir Migrations\SqlServerMigrations

(2)

(2.1)
In 'appsettings.json' and 'appsettings.Development.json' files:

		"DatabaseSettings": {
			"Provider": "SQLite",
			"SQLiteConnectionString": "Data Source=Database\\MySQLite.db",
			"SqlServerConnectionString": "Server=.;Database=DT_CMS;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true;"
		}

(2.2)
Tools
	-> NuGet Package Manager
		-> Package Manager Console

			In 'Default Project' select: 'Persistence' project

Add-Migration InitialCreate -Context DatabaseContext -OutputDir Migrations\SqliteMigrations

(3)

(3.1)
In 'appsettings.json' and 'appsettings.Development.json' files:

		"DatabaseSettings": {
			"Provider": "MSSqlServer",
			"SQLiteConnectionString": "Data Source=Database\\MySQLite.db",
			"SqlServerConnectionString": "Server=.;Database=DT_CMS;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true;"
		}

(3.2)
Run and Test the Proejct

(4)

(4.1)
In 'appsettings.json' and 'appsettings.Development.json' files:

		"DatabaseSettings": {
			"Provider": "SQLite",
			"SQLiteConnectionString": "Data Source=Database\\MySQLite.db",
			"SqlServerConnectionString": "Server=.;Database=DT_CMS;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true;"
		}

(4.2)
Run and Test the Proejct

(5)

(5.1)
Making some changes in Domain Models (Entities)

(6)

Repeat steps (1) to (4) with another name of migrations!!!
-------------------------------------------------------------------------------------------
