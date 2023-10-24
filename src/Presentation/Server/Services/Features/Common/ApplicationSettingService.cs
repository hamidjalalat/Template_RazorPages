using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Services.Features.Common;

public class ApplicationSettingService :
	Infrastructure.BaseServiceWithDatabaseContext
{
	#region Constructor
	public ApplicationSettingService(Persistence
		.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
	}
	#endregion /Constructor

	#region Methods

	#region GetInstanceAsync()
	public async System.Threading.Tasks.Task
		<Domain.Features.Common.ApplicationSetting> GetInstanceAsync()
	{
		var result =
			await
			DatabaseContext.ApplicationSettings
			.Include(current => current.DefaultCulture)
			.FirstOrDefaultAsync();

		if (result != null)
		{
			return result;
		}

		var persianLcid =
			Domain.Features.Common.Enums.CultureEnum.Persian;

		var persianCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.Lcid == persianLcid)
			.FirstOrDefaultAsync();

		if (persianCulture == null)
		{
			var persianCultureInfo = Domain.Features.Common
				.CultureEnumHelper.GetByLcid(lcid: persianLcid);

			persianCulture =
				new Domain.Features.Common.Culture
				(lcid: Domain.Features.Common.Enums.CultureEnum.Persian,
				cultureName: persianCultureInfo.Name,
				nativeName: persianCultureInfo.NativeName)
				{
					IsActive = true,
					Ordering = 10_000,
					Description = null,
				};

			await DatabaseContext.AddAsync(entity: persianCulture);

			await DatabaseContext.SaveChangesAsync();
		}
		else
		{
			if (persianCulture.IsActive == false)
			{
				persianCulture.IsActive = true;

				await DatabaseContext.SaveChangesAsync();
			}
		}

		result =
			new Domain.Features.Common
			.ApplicationSetting(defaultCultureId: persianCulture.Id);

		await DatabaseContext.AddAsync(entity: result);

		await DatabaseContext.SaveChangesAsync();

		return result;
	}
	#endregion /GetInstanceAsync()

	#region GetSupportedCultureNamesAsync()
	public async System.Threading.Tasks.Task
		<System.Collections.Generic.IList<string>> GetSupportedCultureNamesAsync()
	{
		var result =
			await
			DatabaseContext.Cultures
			.Where(current => current.IsActive)
			.Select(current => current.CultureName)
			.ToListAsync()
			;

		if (result.Count == 0)
		{
			return new string[] { "fa-IR" };
		}

		return result;
	}
	#endregion /GetSupportedCultureNamesAsync()

	#endregion /Methods
}
