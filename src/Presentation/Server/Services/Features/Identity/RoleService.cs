using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Services.Features.Identity;

public static class RoleService : object
{
	static RoleService()
	{
	}

	public static async System.Threading.Tasks.Task<Domain.Features.Common.BaseTableItem?>
		GetByIdAsync(Persistence.DatabaseContext databaseContext, System.Guid? id)
	{
		if(id.HasValue == false)
		{
			return null;
		}

		var role =
			await databaseContext.BaseTableItems
			.Where(current => current.Id == id.Value)
			.Where(current => current.BaseTable != null &&
				current.BaseTable.Code == Domain.Features.Common.Enums.BaseTableEnum.Role)
			.FirstOrDefaultAsync();

		if(role is null)
		{
			return null;
		}

		if(role.Code.HasValue == false)
		{
			return null;
		}

		var exists = System.Enum.IsDefined
			(enumType: typeof(Domain.Features.Identity.Enums.RoleEnum), value: role.Code.Value);

		if(exists == false)
		{
			return null;
		}

		return role;
	}
}
