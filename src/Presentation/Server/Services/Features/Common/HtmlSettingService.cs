using Microsoft.EntityFrameworkCore;

namespace Services.Features.Common;

public class HtmlSettingService :
	Infrastructure.BaseServiceWithDatabaseContext
{
	#region Constructor
	public HtmlSettingService(Persistence
		.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
	}
	#endregion /Constructor

	#region Methods

	#region GetInstanceAsync()
	public async System.Threading.Tasks.Task
		<Domain.Features.Common.HtmlSetting> GetInstanceAsync()
	{
		var result =
			await
			DatabaseContext.HtmlSettings
			.FirstOrDefaultAsync();

		if (result != null)
		{
			return result;
		}

		result =
			new Domain.Features.Common.HtmlSetting();

		await DatabaseContext.AddAsync(entity: result);

		await DatabaseContext.SaveChangesAsync();

		return result;
	}
	#endregion /GetInstanceAsync()

	#endregion /Methods
}
