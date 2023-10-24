using Dtat;

namespace Infrastructure.Settings;

public class DatabaseSetting : object
{
	public DatabaseSetting() : base()
	{
	}

	public string? Provider { get; set; }

	public string? SQLiteConnectionString { get; set; }

	public string? SqlServerConnectionString { get; set; }

	public Enums.DatabaseProviderType DatabaseProviderType
	{
		get
		{
			var result =
				Enums.DatabaseProviderType.Unknown;

			Provider =
				Provider.Fix();

			if (Provider is null)
			{
				return result;
			}

			Provider = Provider.Replace
				(oldValue: " ", newValue: string.Empty);

			switch (Provider.ToLower())
			{
				case "sqlite":
				{
					result = Enums
						.DatabaseProviderType.SQLite;

					break;
				}

				case "sqlserver":
				case "mssqlserver":
				{
					result = Enums
						.DatabaseProviderType.SqlServer;

					break;
				}
			}

			return result;
		}
	}
}
