using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace Infrastructure.Security;

public class UserManagerService : BaseServiceWithDatabaseContext
{
	#region Constructor
	public UserManagerService
		(Persistence.DatabaseContext databaseContext,
		Services.Features.Common.HttpContextService httpContextService,
		Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor) : base(databaseContext: databaseContext)
	{
		HttpContextService = httpContextService;
		HttpContextAccessor = httpContextAccessor;
	}
	#endregion /Constructor

	#region Properties

	private Services.Features.Common.HttpContextService HttpContextService { get; init; }
	private Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; init; }

	#endregion /Properties

	#region Login()
	public async System.Threading.Tasks.Task<bool>
		LoginAsync(Domain.Features.Identity.User user, bool rememberMe,
		bool log, Domain.Features.Identity.Enums.AuthenticationTypeEnum loginType)
	{
		if (user.Role is null)
		{
			return false;
		}

		if (HttpContextAccessor.HttpContext is null)
		{
			return false;
		}

		// **************************************************
		var userIP =
			HttpContextService.GetRemoteIpAddress();

		if (userIP is null)
		{
			return false;
		}
		// **************************************************

		// **************************************************
		Domain.Features.Identity.LoginLog? loginLog = null;

		if (log)
		{
			user.LastLoginDateTime = Dtat.DateTime.Now;

			loginLog =
				new Domain.Features.Identity.LoginLog
				(userId: user.Id, userIP: userIP, loginType: loginType);

			await DatabaseContext.AddAsync(entity: loginLog);

			await DatabaseContext.SaveChangesAsync();
		}
		// **************************************************

		// **************************************************
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var currentCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.Lcid == currentUICultureLcid)
			.FirstOrDefaultAsync();

		if (currentCulture is null)
		{
			return false;
		}
		// **************************************************

		// **************************************************
		var localizedUser =
			DatabaseContext.LocalizedUsers
			.Where(current => current.UserId == user.Id)
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefault();
		// **************************************************

		// **************************************************
		var claims =
			new System.Collections.Generic
			.List<System.Security.Claims.Claim>();

		System.Security.Claims.Claim claim;
		// **************************************************

		// **************************************************
		if (localizedUser is not null)
		{
			if (string.IsNullOrWhiteSpace(value: localizedUser.LastName) == false)
			{
				claim = new System.Security.Claims.Claim(type: Constants
					.ClaimKeyName.LastName, value: localizedUser.LastName);

				claims.Add(item: claim);
			}

			if (string.IsNullOrWhiteSpace(value: localizedUser.FirstName) == false)
			{
				claim = new System.Security.Claims.Claim(type: Constants
					.ClaimKeyName.FirstName, value: localizedUser.FirstName);

				claims.Add(item: claim);
			}
		}
		// **************************************************

		// **************************************************
		claim = new System.Security.Claims.Claim(type:
			System.Security.Claims.ClaimTypes.Role, value: user.Role.KeyName);

		claims.Add(item: claim);

		if(user.Role.Code.HasValue)
		{
			claim = new System.Security.Claims.Claim(type: Constants
				.ClaimKeyName.RoleCode, value: user.Role.Code.Value.ToString());

			claims.Add(item: claim);
		}
		// **************************************************

		// **************************************************
		// نباید از دستور ذیل استفاده نماییم
		//claim = new System.Security.Claims.Claim(type:
		//	System.Security.Claims.ClaimTypes.Name, value: foundedUser.Username);

		claim = new System.Security.Claims.Claim(type:
			System.Security.Claims.ClaimTypes.Name, value: user.EmailAddress);

		claims.Add(item: claim);
		// **************************************************

		// **************************************************
		// نیازی نیست که از دستور ذیل استفاده نماییم
		//claim = new System.Security.Claims.Claim(type:
		//	System.Security.Claims.ClaimTypes.Email, value: user.EmailAddress);

		claim = new System.Security.Claims.Claim(type:
			Constants.ClaimKeyName.EmailAddress, value: user.EmailAddress);

		claims.Add(item: claim);
		// **************************************************

		// **************************************************
		if (string.IsNullOrWhiteSpace(value: user.Username) == false)
		{
			claim = new System.Security.Claims.Claim(type:
				Constants.ClaimKeyName.Username, value: user.Username);

			claims.Add(item: claim);
		}
		// **************************************************

		// **************************************************
		if (string.IsNullOrWhiteSpace(value: user.CellPhoneNumber) == false)
		{
			// نیازی نیست که از دستور ذیل استفاده نماییم
			//claim = new System.Security.Claims.Claim(type:
			//	System.Security.Claims.ClaimTypes.MobilePhone, value: user.CellPhoneNumber);

			claim = new System.Security.Claims.Claim(type:
				Constants.ClaimKeyName.CellPhoneNumber, value: user.CellPhoneNumber);

			claims.Add(item: claim);
		}
		// **************************************************

		// **************************************************
		// نیازی نیست که از دستورات ذیل استفاده نماییم
		//claim = new System.Security.Claims.Claim(type:
		//	System.Security.Claims.ClaimTypes.NameIdentifier, value: user.Id.ToString());

		claim = new System.Security.Claims.Claim(type:
			Constants.ClaimKeyName.UserId, value: user.Id.ToString());

		claims.Add(item: claim);
		// **************************************************

		// **************************************************
		claim = new System.Security.Claims.Claim(type:
			Constants.ClaimKeyName.UserIP, value: userIP);

		claims.Add(item: claim);
		// **************************************************

		// **************************************************
		if (loginLog is not null)
		{
			claim = new System.Security.Claims.Claim(type:
				Constants.ClaimKeyName.SessionId, value: loginLog.Id.ToString());

			claims.Add(item: claim);
		}
		// **************************************************

		// **************************************************
		var claimsIdentity =
			new System.Security.Claims.ClaimsIdentity
			(claims: claims, authenticationType: Constants.Scheme.Default);
		// **************************************************

		// **************************************************
		var claimsPrincipal =
			new System.Security.Claims
			.ClaimsPrincipal(identity: claimsIdentity);
		// **************************************************

		// **************************************************
		var authenticationProperties = new Microsoft
			.AspNetCore.Authentication.AuthenticationProperties
		{
			IsPersistent = rememberMe,
		};
		// **************************************************

		// **************************************************
		// using Microsoft.AspNetCore.Authentication;
		await HttpContextAccessor.HttpContext.SignInAsync
			(scheme: Infrastructure.Security.Constants.Scheme.Default,
			principal: claimsPrincipal, properties: authenticationProperties);
		// **************************************************

		return true;
	}
	#endregion /Login()
}
