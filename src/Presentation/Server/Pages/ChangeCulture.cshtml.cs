using Dtat;
using System.Linq;

namespace Server.Pages;

public class ChangeCultureModel : Infrastructure.BasePageModel
{
	#region Constructor
	public ChangeCultureModel(Services.Features.Common.HttpContextService httpContextService,
		Services.Features.Common.ApplicationSettingService applicationSettingService) : base()
	{
		HttpContextService = httpContextService;
		ApplicationSettingService = applicationSettingService;
	}
	#endregion /Constructor

	#region Properties

	private Services.Features.Common.HttpContextService HttpContextService { get; init; }
	private Services.Features.Common.ApplicationSettingService ApplicationSettingService { get; init; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(string? cultureName)
	{
		// **************************************************
		var httpReferer =
			HttpContextService.GetHttpReferer();

		if (string.IsNullOrWhiteSpace(value: httpReferer))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.RootIndex);
		}
		// **************************************************

		// **************************************************
		var applicationSetting =
			await
			ApplicationSettingService.GetInstanceAsync();

		var defaultCultureName =
			applicationSetting.DefaultCulture!.CultureName.ToLower();

		var supportedCultureNames =
			await
			ApplicationSettingService.GetSupportedCultureNamesAsync();
		// **************************************************

		// **************************************************
		cultureName = cultureName.Fix();

		//if (cultureName is null)
		//{
		//	cultureName =
		//		defaultCultureName;
		//}

		cultureName ??= defaultCultureName;

		cultureName =
			cultureName.ToLower();
		// **************************************************

		// **************************************************
		if (supportedCultureNames is null)
		{
			cultureName =
				defaultCultureName;
		}
		else
		{
			var contains =
				supportedCultureNames
				.Where(current => current.ToLower() == cultureName)
				.Any();

			if (contains == false)
			{
				cultureName =
					defaultCultureName;
			}
		}
		// **************************************************

		// **************************************************
		Infrastructure.Middlewares
			.CultureCookieHandlerMiddleware
			.SetCulture(cultureName: cultureName);
		// **************************************************

		// **************************************************
		Infrastructure.Middlewares.CultureCookieHandlerMiddleware
			.CreateCookie(httpContext: HttpContext, cultureName: cultureName);
		// **************************************************

		// **************************************************
		var cultureNameRoutingPart =
			$"/{cultureName}".ToLower();

		if (httpReferer.ToLower().Contains(value: "/fa-ir"))
		{
			httpReferer =
				httpReferer.ToLower()
				.Replace(oldValue: "/fa-ir", newValue: cultureNameRoutingPart)
				;
		}
		else
		{
			if (httpReferer.ToLower().Contains(value: "/en-us"))
			{
				httpReferer =
					httpReferer.ToLower()
					.Replace(oldValue: "/en-us", newValue: cultureNameRoutingPart)
					;
			}
		}
		// **************************************************

		return Redirect(url: httpReferer);
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
