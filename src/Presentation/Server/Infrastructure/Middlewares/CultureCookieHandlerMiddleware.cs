﻿using System.Linq;

namespace Infrastructure.Middlewares;

public class CultureCookieHandlerMiddleware : object
{
	#region Statics
	public readonly static string CookieName = "Culture.Cookie";

	public static string? GetCultureNameByCookie
		(Microsoft.AspNetCore.Http.HttpContext httpContext,
		System.Collections.Generic.IList<string>? supportedCultureNames)
	{
		if (supportedCultureNames is null)
		{
			return null;
		}

		if (supportedCultureNames.Count == 0)
		{
			return null;
		}

		var cultureName =
			httpContext.Request.Cookies[key: CookieName];

		if (string.IsNullOrWhiteSpace(value: cultureName))
		{
			return null;
		}

		cultureName =
			cultureName.ToLower();

		var contains =
			supportedCultureNames
			.Where(current => current.ToLower() == cultureName)
			.Any();

		if (contains == false)
		{
			return null;
		}

		return cultureName;
	}

	public static void SetCulture(string? cultureName)
	{
		if (string.IsNullOrWhiteSpace(value: cultureName) == false)
		{
			cultureName = cultureName.Replace
				(oldValue: " ", newValue: string.Empty).ToLower();

			var cultureInfo =
				new System.Globalization.CultureInfo(name: cultureName);

			System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
			System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
		}
	}

	public static void CreateCookie
		(Microsoft.AspNetCore.Http.HttpContext httpContext, string cultureName)
	{
		// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.cookieoptions?view=aspnetcore-6.0

		var cookieOptions =
			new Microsoft.AspNetCore.Http.CookieOptions
			{
				Path = "/", // Default: [null]

				// Domain: Gets or sets the domain to associate the cookie with.
				// نباید تغییر دهیم، خودش به طور
				// خودکار بر اساس دامنه سایت تنظیم می‌شود

				Secure = false, // Default: [false]

				// Secure: Gets or sets a value that indicates whether to transmit
				// the cookie using Secure Sockets Layer (SSL)--that is, over HTTPS only.

				HttpOnly = false, // Default: [false]

				// HttpOnly: Gets or sets a value that indicates
				// whether a cookie is accessible by client-side script.
				// [true] if a cookie must not be accessible by client-side script; otherwise, [false].

				IsEssential = false, // Default: [false]

				// Indicates if this cookie is essential for the application
				// to function correctly. If true then consent policy checks may be bypassed.

				MaxAge = null, // Default: [null]

				// MaxAge: Gets or sets the max-age for the cookie.

				Expires =
					System.DateTimeOffset.UtcNow.AddYears(1), // Default: [null]

				// Expires: Gets or sets the expiration date and time for the cookie.

				SameSite =
					Microsoft.AspNetCore.Http.SameSiteMode.Unspecified, // Default: [Unspecified]

				// SameSite: Gets or sets the value for the SameSite attribute of the cookie.

				// The SameSiteMode representing the enforcement mode of the cookie:

				// Lax			1	Indicates the client should send the cookie with "same-site"
				//					requests, and with "cross-site" top-level navigations.
				// None			0	Indicates the client should disable same-site restrictions.
				// Strict		2	Indicates the client should only send the cookie with
				//					"same-site" requests.
				// Unspecified	-1	No SameSite field will be set, the client should
				//					follow its default cookie policy.
			};

		httpContext.Response.Cookies.Delete(key: CookieName);

		if (string.IsNullOrWhiteSpace(value: cultureName) == false)
		{
			httpContext.Response.Cookies.Append
				(key: CookieName, value: cultureName, options: cookieOptions);
		}
	}
	#endregion /Statics

	public CultureCookieHandlerMiddleware
		(Microsoft.AspNetCore.Http.RequestDelegate next) : base()
	{
		Next = next;
	}

	private Microsoft.AspNetCore.Http.RequestDelegate Next { get; }

	public async System.Threading.Tasks.Task InvokeAsync
		(Microsoft.AspNetCore.Http.HttpContext httpContext,
		Services.Features.Common.ApplicationSettingService applicationSettingService)
	{
		// TODO
		var request =
			httpContext.Request.Path;

		// **************************************************
		var applicationSetting =
			await
			applicationSettingService.GetInstanceAsync();

		var defaultCultureName =
			applicationSetting.DefaultCulture!.CultureName;

		var supportedCultureNames =
			await
			applicationSettingService.GetSupportedCultureNamesAsync();
		// **************************************************

		// **************************************************
		var currentCultureName =
			GetCultureNameByCookie
			(httpContext: httpContext,
			supportedCultureNames: supportedCultureNames);

		//if (currentCultureName is null)
		//{
		//	currentCultureName = defaultCultureName;
		//}

		currentCultureName ??= defaultCultureName;

		SetCulture(cultureName: currentCultureName);
		// **************************************************

		await Next(context: httpContext);
	}
}
