using Dtat;
using System.Linq;

namespace Server.Pages;

public class IndexModel :
	Infrastructure.BasePageModel
{
	#region Constructor
	public IndexModel(Services.Features.Common
		.ApplicationSettingService applicationSettingService) : base()
	{
		ApplicationSettingService = applicationSettingService;
	}
	#endregion /Constructor

	#region Properties
	private Services.Features.Common.ApplicationSettingService ApplicationSettingService { get; init; }
	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(string? culture = null)
	{
		// **************************************************
		var applicationSetting =
			await
			ApplicationSettingService.GetInstanceAsync();

		var defaultCultureName =
			applicationSetting.DefaultCulture!.CultureName;

		var supportedCultureNames =
			await
			ApplicationSettingService.GetSupportedCultureNamesAsync();

		var cultureNameInCookie =
			Infrastructure.Middlewares.CultureCookieHandlerMiddleware
			.GetCultureNameByCookie(httpContext: HttpContext, supportedCultureNames: supportedCultureNames);
		// **************************************************

		// **************************************************
		// فرض می‌کنیم که برنامه‌نویس، گزینه‌های فوق را به درستی
		// تنظیم کرده است، یعنی لااقل یک زبان فعال داریم و زبان
		//  پیش‌فرض با یکی از زبان‌های فعالی که پشتیبانی می‌شود برابر است
		// **************************************************

		// **************************************************
		culture = culture.Fix();

		if (culture is null)
		{
			if (string.IsNullOrWhiteSpace(value: cultureNameInCookie))
			{
				Infrastructure.Middlewares
					.CultureCookieHandlerMiddleware.CreateCookie
					(httpContext: HttpContext, cultureName: defaultCultureName);

				Infrastructure.Middlewares
					.CultureCookieHandlerMiddleware.SetCulture(cultureName: defaultCultureName);

				var url =
					$"{Constants.CommonRouting.RootIndex}/{defaultCultureName}";

				return Redirect(url: url);
			}
			else
			{
				cultureNameInCookie =
					cultureNameInCookie.ToLower();

				var contains =
					supportedCultureNames
					.Where(current => current.ToLower() == cultureNameInCookie)
					.Any();

				if (contains == false)
				{
					Infrastructure.Middlewares
						.CultureCookieHandlerMiddleware.CreateCookie
						(httpContext: HttpContext, cultureName: defaultCultureName);

					Infrastructure.Middlewares
						.CultureCookieHandlerMiddleware.SetCulture(cultureName: defaultCultureName);

					var url =
						$"{Constants.CommonRouting.RootIndex}/{defaultCultureName}";

					return Redirect(url: url);
				}
				else
				{
					Infrastructure.Middlewares
						.CultureCookieHandlerMiddleware.CreateCookie
						(httpContext: HttpContext, cultureName: cultureNameInCookie);

					Infrastructure.Middlewares
						.CultureCookieHandlerMiddleware.SetCulture(cultureName: cultureNameInCookie);

					var url =
						$"{Constants.CommonRouting.RootIndex}/{cultureNameInCookie}";

					return Redirect(url: url);
				}
			}
		}
		else
		{
			culture =
				culture.Replace
				(oldValue: " ", newValue: string.Empty)
				.ToLower();

			var contains =
				supportedCultureNames
				.Where(current => current.ToLower() == culture)
				.Any();

			if (contains == false)
			{
				Infrastructure.Middlewares
					.CultureCookieHandlerMiddleware.CreateCookie
					(httpContext: HttpContext, cultureName: defaultCultureName);

				Infrastructure.Middlewares
					.CultureCookieHandlerMiddleware.SetCulture(cultureName: defaultCultureName);

				var url =
					$"{Constants.CommonRouting.RootIndex}/{defaultCultureName}";

				return Redirect(url: url);
			}
			else
			{
				Infrastructure.Middlewares
					.CultureCookieHandlerMiddleware.CreateCookie
					(httpContext: HttpContext, cultureName: culture);

				Infrastructure.Middlewares
					.CultureCookieHandlerMiddleware.SetCulture(cultureName: culture);

				return Page();
			}
		}
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
