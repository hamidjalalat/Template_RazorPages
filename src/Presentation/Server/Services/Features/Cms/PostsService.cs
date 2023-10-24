using System.Linq;

namespace Services.Features.Cms;

public class PostsService :
	Infrastructure.BaseServiceWithDatabaseContext
{
	#region Constructor
	public PostsService(Persistence.DatabaseContext
		databaseContext) : base(databaseContext: databaseContext)
	{
	}
	#endregion /Constructor

	#region Methods

	#region GetByCulture()
	public IQueryable<Domain.Features.Cms.Post> GetByCulture()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var result =
			DatabaseContext.Posts

			.Where(current => current.IsActive)
			.Where(current => current.IsDraft == false)
			.Where(current => current.IsDeleted == false)

			.Where(current => current.Type!.IsActive)
			.Where(current => current.Category!.IsActive)

			.Where(current => current.User!.IsDeleted == false)

			// فکر می‌کنم که به دو دستور ذیل نیازی نیست
			//.Where(current => current.User!.IsActive)
			//.Where(current => current.User!.IsEmailAddressVerified)

			.Where(current => current.Culture!.Lcid == currentUICultureLcid)
			;

		return result;
	}
	#endregion /GetByCulture()

	#region OrderBy()
	public IQueryable<Domain.Features.Cms.Post>
		OrderBy(IQueryable<Domain.Features.Cms.Post> request)
	{
		var result =
			request

			.OrderByDescending(current => current.IsFeatured)
			.ThenByDescending(current => current.UpdateDateTime)
			;

		return result;
	}
	#endregion /OrderBy()

	#endregion /Methods
}
