namespace Infrastructure.Middlewares;

public class GlobalExceptionHandlerMiddleware : object
{
	public GlobalExceptionHandlerMiddleware
		(Microsoft.AspNetCore.Http.RequestDelegate next) : base()
	{
		Next = next;
	}

	#region Properties

	private Microsoft.AspNetCore.Http.RequestDelegate Next { get; init; }

	#endregion /Properties

	#region Methods

	#region InvokeAsync()
	public async System.Threading.Tasks.Task InvokeAsync
		(Microsoft.AspNetCore.Http.HttpContext httpContext,
		Services.Features.Common.UnexpectedErrorLoggerService unexpectedErrorLoggerService)
	{
		try
		{
			await Next(context: httpContext);

			switch (httpContext.Response.StatusCode)
			{
				case (int)System.Net.HttpStatusCode.NotFound:
				{
					if (httpContext.Response.HasStarted == false)
					{
						httpContext.Request.Path = "/Errors/Error404";

						// TODO
						await Next(context: httpContext);
					}

					break;
				}
			}
		}
		catch (System.Exception ex)
		{
			try
			{
				await unexpectedErrorLoggerService.LogAsync(exception: ex);
			}
			catch { }

			httpContext.Response.Redirect(location:
				"/Errors/Error500", permanent: false);
		}
	}
	#endregion /InvokeAsync()

	#endregion /Methods
}
