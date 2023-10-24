namespace Constants;

public static class CommonRouting : object
{
	static CommonRouting()
	{
	}

	/// <summary>
	/// Error 404
	/// </summary>
	public const string NotFound = "/Errors/Error404";

	/// <summary>
	/// Error 403
	/// </summary>
	public const string Forbidden = "/Errors/Error403";

	/// <summary>
	/// Error 400
	/// </summary>
	public const string BadRequest = "/Errors/Error400";

	/// <summary>
	/// Error 500
	/// </summary>
	public const string InternalServerError = "/Errors/Error500";



	/// <summary>
	/// Root Index
	/// </summary>
	public const string RootIndex = "/Index";

	/// <summary>
	/// Current Index
	/// </summary>
	public const string CurrentIndex = "Index";

	/// <summary>
	/// Dashboard
	/// </summary>
	public const string Dashboard = "/Dashboard";

	/// <summary>
	/// Login
	/// </summary>
	public const string Login = "/Account/Login";

	/// <summary>
	/// Logout
	/// </summary>
	public const string Logout = "/Account/Logout";

	/// <summary>
	/// Google Login
	/// </summary>
	public const string GoogleLogin = "/Google/Login";

	/// <summary>
	/// Register
	/// </summary>
	public const string Register = "/Account/Register";

	/// <summary>
	/// Send Again Email Address Verification Key
	/// </summary>
	public const string
		SendAgainEmailAddressVerificationKey =
		"/Account/SendAgainEmailAddressVerificationKey";
}
