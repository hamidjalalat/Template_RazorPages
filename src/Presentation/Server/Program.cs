// **************************************************
using DNTCaptcha.Core;
using Infrastructure.Middlewares;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Security.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
// **************************************************

// **************************************************
var webApplicationOptions =
	new Microsoft.AspNetCore.Builder.WebApplicationOptions
	{
		EnvironmentName =
			System.Diagnostics.Debugger.IsAttached ?
			Microsoft.Extensions.Hosting.Environments.Development
			:
			Microsoft.Extensions.Hosting.Environments.Production,
	};

var builder =
	Microsoft.AspNetCore.Builder
	.WebApplication.CreateBuilder(options: webApplicationOptions);
// **************************************************

// **************************************************
//using Microsoft.Extensions.Configuration;
var applicationSettings =
	builder.Configuration.GetSection
	(Infrastructure.Settings.ApplicationSettings.KeyName)
	.Get<Infrastructure.Settings.ApplicationSettings>();
// **************************************************

// **************************************************
// using Microsoft.Extensions.DependencyInjection;
builder.Services
	.AddHttpContextAccessor();
// **************************************************

// **************************************************
builder.Services
	.AddControllers();

// using DNTCaptcha.Core;
builder.Services
	.AddDNTCaptcha(options =>
	{
		// .UseDistributedSerializationProvider()

		// options.UseSessionStorageProvider()
		// -> It doesn't rely on the server or client's times. Also, it's the safest one.

		// options.UseMemoryCacheStorageProvider()
		// -> It relies on the server's times. It's safer than the CookieStorageProvider.

		// .UseDistributedCacheStorageProvider()
		// -> It's ideal for scalability using
		// `services.AddStackExchangeRedisCache()` for instance.

		options.UseCookieStorageProvider
			(sameSite: Microsoft.AspNetCore.Http.SameSiteMode.Strict)
		// -> It relies on the server and client's times. It's ideal.
		// for scalability because it doesn't save anything in the server's memory.

		// This is optional!
		// Don't set this line (remove it) to use the installed system's fonts
		// (FontName = "Tahoma") or if you want to use a custom font, make sure
		// that font is present in the wwwroot/fonts folder and use a good
		// and complete font!
		//.UseCustomFont(fullPath: System.IO.Path.Combine
		//	(env.WebRootPath, "fonts", "IRANSans(FaNum)_Bold.ttf"))

		.AbsoluteExpiration(minutes: 10)
		.ShowThousandsSeparators(show: false)
		.WithEncryptionKey(key: applicationSettings!.CaptchaImageEncryptionKey!)
		.WithNoise(baseFrequencyX: 0.15f, baseFrequencyY: 0.15f, numOctaves: 10, seed: 1.0f)

		// This is optional!
		// Change it if you don't like the default names.
		.InputNames(component: new DNTCaptcha.Core.DNTCaptchaComponent
		{
			CaptchaInputName = "DNTCaptchaInputText",
			CaptchaHiddenInputName = "DNTCaptchaText",
			CaptchaHiddenTokenName = "DNTCaptchaToken",
		})

		// This is optional!
		// Change it if you don't like its default name.
		.Identifier(className: "dntCaptcha")
		;
	});
// **************************************************
// **************************************************
// **************************************************

// **************************************************
// حل مشکل نمایش متن فارسی در صفحات
// **************************************************
//builder.Services.AddSingleton
//	(implementationInstance: System.Text.Encodings.Web.HtmlEncoder.Create
//	(allowedRanges: new[]
//	{
//		System.Text.Unicode.UnicodeRanges.Arabic,
//		System.Text.Unicode.UnicodeRanges.BasicLatin,
//	}));
// **************************************************

// **************************************************
builder.Services
	.AddRouteAnalyzer();
// **************************************************

// **************************************************
builder.Services
	.AddRouting(options =>
	{
		options.LowercaseUrls = true;

		// نکته مهم
		// مشکل دارد Captcha Image دستور ذیل با
		//options.LowercaseQueryStrings = true;
	});
// **************************************************

// **************************************************
// using Microsoft.Extensions.DependencyInjection;
builder.Services
	  .AddRazorPages(o =>
	  {
		  o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
	  });
//.AddRazorRuntimeCompilation()
;
// **************************************************

// **************************************************
// using Microsoft.Extensions.DependencyInjection;
builder.Services.Configure<Infrastructure.Settings.ApplicationSettings>
	(builder.Configuration.GetSection(key: Infrastructure.Settings.ApplicationSettings.KeyName))
	// using Microsoft.Extensions.DependencyInjection;
	.AddSingleton
	(implementationFactory: serviceType =>
	{
		var result =
			// using Microsoft.Extensions.DependencyInjection;
			serviceType.GetRequiredService
			<Microsoft.Extensions.Options.IOptions
			<Infrastructure.Settings.ApplicationSettings>>().Value;

		return result;
	});
// **************************************************

// **************************************************
// **************************************************
// **************************************************
builder.Services
	.AddAuthentication(defaultScheme:
		Infrastructure.Security.Constants.Scheme.Default)

	.AddCookie(authenticationScheme:
		Infrastructure.Security.Constants.Scheme.Default)

	.AddGoogle
		(authenticationScheme: Microsoft.AspNetCore
		.Authentication.Google.GoogleDefaults.AuthenticationScheme,
		configureOptions: options =>
		{
			options.ClientId =
				applicationSettings!.GoogleAuthenticationSetting.ClientId!;

			options.ClientSecret =
				applicationSettings!.GoogleAuthenticationSetting.ClientSecret!;

			// using Microsoft.AspNetCore.Authentication;
			options.ClaimActions.MapJsonKey
				(claimType: "urn:google:picture", jsonKey: "picture", valueType: "url");
		})
		;
// **************************************************
// **************************************************
// **************************************************

// **************************************************
switch (applicationSettings!.DatabaseSetting.DatabaseProviderType)
{
	case Infrastructure.Settings.Enums.DatabaseProviderType.SQLite:
	{
		// using Microsoft.Extensions.DependencyInjection;
		builder.Services
			.AddDbContext<Persistence.DatabaseContext>(optionsAction: options =>
			{
				// using Microsoft.EntityFrameworkCore;
				options.UseLazyLoadingProxies();

				// using Microsoft.EntityFrameworkCore;
				//options.UseSqlite(connectionString:
				//	applicationSettings.DatabaseSettings.SQLiteConnectionString);

				// using Microsoft.EntityFrameworkCore;
				options.UseSqlite(connectionString: applicationSettings
					.DatabaseSetting.SQLiteConnectionString, sqliteOptionsAction: current =>
					{
						current.MigrationsAssembly
							(assemblyName: "Persistence.SQLite");
					});
			});

		break;
	}

	case Infrastructure.Settings.Enums.DatabaseProviderType.SqlServer:
	{
		// using Microsoft.Extensions.DependencyInjection;
		builder.Services
			.AddDbContext<Persistence.DatabaseContext>(optionsAction: options =>
			{
				// using Microsoft.EntityFrameworkCore;
				options.UseLazyLoadingProxies();

				// using Microsoft.EntityFrameworkCore;
				//options.UseSqlServer(connectionString:
				//	applicationSettings.DatabaseSettings.SqlServerConnectionString);

				// using Microsoft.EntityFrameworkCore;
				options.UseSqlServer(connectionString: applicationSettings
					.DatabaseSetting.SqlServerConnectionString, sqlServerOptionsAction: current =>
				{
					current.MigrationsAssembly
						(assemblyName: "Persistence.SqlServer");
				});
			});

		break;
	}
}
// **************************************************

// **************************************************
// **************************************************
// **************************************************
builder.Services
	.AddSingleton<Services.ColorService>();
// **************************************************

// **************************************************
builder.Services
	.AddScoped<Services.Features.Cms.PostsService>();
// **************************************************

// **************************************************
builder.Services
	.AddScoped<Infrastructure.Security.UserManagerService>();

builder.Services
	.AddScoped<Infrastructure.Security.AuthenticatedUserService>();
// **************************************************

// **************************************************
builder.Services
	.AddScoped<Services.Features.Identity.UserNotificationService>();
// **************************************************

// **************************************************
builder.Services
	.AddScoped<Services.Features.Common.HtmlSettingService>();

builder.Services
	.AddScoped<Services.Features.Common.HttpContextService>();

builder.Services
	.AddScoped<Services.Features.Common.EmailTemplateService>();

builder.Services
	.AddScoped<Services.Features.Common.ApplicationSettingService>();

builder.Services
	.AddScoped<Services.Features.Common.LocalizedMailSettingService>();

builder.Services
	.AddScoped<Services.Features.Common.UnexpectedErrorLoggerService>();

builder.Services
	.AddScoped<Services.Features.Common.LocalizedHomePageSettingService>();

builder.Services
	.AddScoped<Services.Features.Common.LocalizedApplicationSettingService>();
// **************************************************
// **************************************************
// **************************************************

// **************************************************
var app =
	builder.Build();
// **************************************************

// using Microsoft.Extensions.Hosting;
if (app.Environment.IsDevelopment())
{
	// **************************************************
	// using Microsoft.AspNetCore.Builder;
	app.UseDeveloperExceptionPage();
	// **************************************************
}
else
{
	// **************************************************
	// using Infrastructure.Middlewares;
	app.UseGlobalException();
	// **************************************************

	// **************************************************
	// using Microsoft.AspNetCore.Builder;
	//app.UseExceptionHandler
	//	(errorHandlingPath: "/Errors/Error");
	// **************************************************

	if (applicationSettings!.SiteHasSsl)
	{
		// The default HSTS value is 30 days.
		// You may want to change this for production scenarios,
		// see https://aka.ms/aspnetcore-hsts
		//
		// using Microsoft.AspNetCore.Builder;
		app.UseHsts();
	}
}

// **************************************************
if (applicationSettings!.SiteHasSsl)
{
	// using Microsoft.AspNetCore.Builder;
	app.UseHttpsRedirection();
}
// **************************************************

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.UseStaticFiles();
// **************************************************

// **************************************************
// using Infrastructure.Middlewares;
app.UseActivationKeys();
// **************************************************

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.UseRouting();
// **************************************************

// **************************************************
// For Captcha Image!
// using Infrastructure.Middlewares;
app.UseCultureCookie();
// **************************************************

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.UseAuthentication();
// **************************************************

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.UseAuthorization();
// **************************************************

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.MapRazorPages();
// **************************************************

// **************************************************
// For Captcha Image!
// using Microsoft.AspNetCore.Builder;
app.MapControllers();
// **************************************************

// **************************************************
app.Run();
// **************************************************
