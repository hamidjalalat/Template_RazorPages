using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares;

public class ActivationKeysHandlerMiddleware : object
{
	public ActivationKeysHandlerMiddleware
		(Microsoft.AspNetCore.Http.RequestDelegate next) : base()
	{
		Next = next;
	}

	private Microsoft.AspNetCore.Http.RequestDelegate Next { get; }

	/// <summary>
	/// باید اولین پارامتر باشد httpContext
	/// </summary>
	public async System.Threading.Tasks.Task InvokeAsync
		(Microsoft.AspNetCore.Http.HttpContext httpContext,
		Settings.ApplicationSettings applicationSettings,
		Services.Features.Common.HttpContextService httpContextService)
	{
		// **************************************************
		string? domain = null;
		object? validActivationKey = null;

		var errorMessage =
			"No Activation Key! - Call Mr. Dariush Tasdighi - (+98)9121087461";
		// **************************************************

		// **************************************************
		//domain = "dtat.ir";
		//validActivationKey =
		//	GetValidActivationKeyByDomain(domain: domain);
		//// Key = "cE3fxHf/pu69DTfoyX3LCw=="

		//domain = "www.dtat.ir";
		//validActivationKey =
		//	GetValidActivationKeyByDomain(domain: domain);
		//// Key: "SjN/c2wLmcfxhy/Adx8mVg=="

		//domain = "rekotec.se";
		//validActivationKey =
		//	GetValidActivationKeyByDomain(domain: domain);
		//// Key: "cCqyB9n2zpqtUSCPt/1xKw=="

		//domain = "www.rekotec.se";
		//validActivationKey =
		//	GetValidActivationKeyByDomain(domain: domain);
		//// Key: "i9aMzbNlb7l3IYgjOSclbA=="

		//domain = "aysantaremi.ir";
		//validActivationKey =
		//	GetValidActivationKeyByDomain(domain: domain);
		//// Key: "R5MKzhHoMydxreXIhxRhzg=="

		//domain = "www.aysantaremi.ir";
		//validActivationKey =
		//	GetValidActivationKeyByDomain(domain: domain);
		//// Key: "N6EsqCrkjd8+3D4pUOqvdQ=="

		//domain = "iranianexperts.ir";
		//validActivationKey =
		//	GetValidActivationKeyByDomain(domain: domain);
		//// Key: "G7YXYFUngEC/2hUsxk42uA=="

		//domain = "www.iranianexperts.ir";
		//validActivationKey =
		//	GetValidActivationKeyByDomain(domain: domain);
		//// Key: "McUzpExll/AKJEQGqMyBrQ=="
		// **************************************************

		// **************************************************
		domain =
			httpContextService.GetCurrentHostName();

		if (string.IsNullOrWhiteSpace(value: domain))
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "1. " + domain + " - " + errorMessage);

			return;
		}
		// **************************************************

		// **************************************************
		if (domain == "127.0.0.1")
		{
			await Next(context: httpContext);

			return;
		}

		if (domain == "localhost")
		{
			await Next(context: httpContext);

			return;
		}

		if (domain.StartsWith(value: "localhost:"))
		{
			await Next(context: httpContext);

			return;
		}
		// **************************************************

		// **************************************************
		var activationKeys =
			applicationSettings.ActivationKeys;

		if (activationKeys is null)
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "2. " + domain + " - " + errorMessage);

			return;
		}

		if (activationKeys.Length == 0)
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "3. " + domain + " - " + errorMessage);

			return;
		}
		// **************************************************

		// **************************************************
		validActivationKey =
			GetValidActivationKeyByDomain(domain: domain);

		if (validActivationKey is null)
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "4. " + domain + " - " + errorMessage);

			return;
		}
		// **************************************************

		// **************************************************
		var validActivationKeyString =
			validActivationKey.ToString();

		if (string.IsNullOrWhiteSpace(value: validActivationKeyString))
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "5. " + domain + " - " + errorMessage);

			return;
		}
		// **************************************************

		// **************************************************
		var contains =
			activationKeys
			.Where(current => current.ToLower() == validActivationKeyString.ToLower())
			.Any();

		if (contains == false)
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "6. " + domain + " - " + errorMessage);

			return;
		}
		// **************************************************

		await Next(context: httpContext);
	}

	private static object GetValidActivationKeyByDomain(string domain)
	{
		var result = Dtat.Security
			.Hashing.GetSha256(value: domain);

		//var part1 = result.Substring
		//	(startIndex: 0, length: 10);

		var part1 = result[..10];

		//var part2 = result
		//	.Substring(startIndex: 10);

		var part2 = result[10..];

		result = part2 + part1;

		result = Dtat.Security
			.Hashing.GetMD5(value: result);

		return result;
	}
}
