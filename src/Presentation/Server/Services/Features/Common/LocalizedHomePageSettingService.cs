using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Services.Features.Common;

public class LocalizedHomePageSettingService :
	Infrastructure.BaseServiceWithDatabaseContext
{
	public LocalizedHomePageSettingService(Persistence
		.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
	}

	public async System.Threading.Tasks.Task
		<Domain.Features.Common.LocalizedHomePageSetting> GetInstanceAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var result =
			await
			DatabaseContext.LocalizedHomePageSettings
			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)
			.FirstOrDefaultAsync();

		if (result != null)
		{
			result.Hits++;

			await DatabaseContext.SaveChangesAsync();

			return result;
		}

		var currentCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.Lcid == currentUICultureLcid)
			.FirstOrDefaultAsync();

		if (currentCulture == null)
		{
			throw new System.Exception
				($"{nameof(currentCulture)} is null!");
		}

		result = new Domain.Features.Common
			.LocalizedHomePageSetting(cultureId: currentCulture.Id);

		await DatabaseContext.AddAsync(entity: result);

		await DatabaseContext.SaveChangesAsync();

		return result;
	}
}
