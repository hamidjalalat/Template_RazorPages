namespace Infrastructure;

public abstract class BasePageModelWithDatabaseContext : BasePageModel
{
	public BasePageModelWithDatabaseContext
		(Persistence.DatabaseContext databaseContext) : base()
	{
		DatabaseContext = databaseContext;
	}

	protected Persistence.DatabaseContext DatabaseContext { get; init; }

	protected void DisposeDatabaseContext()
	{
		//if (DatabaseContext is not null)
		//{
		//	DatabaseContext.Dispose();
		//	//DatabaseContext = null;
		//}

		DatabaseContext?.Dispose();
	}

	protected async
		System.Threading.Tasks.Task DisposeDatabaseContextAsync()
	{
		if (DatabaseContext is not null)
		{
			await DatabaseContext.DisposeAsync();
			//DatabaseContext = null;
		}
	}
}
