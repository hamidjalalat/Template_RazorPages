namespace Server.Pages.Test;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Programmer)]
public class AllRoutesModel : Infrastructure.BasePageModel
{
	public AllRoutesModel(Infrastructure.Security
		.Routing.IRouteAnalyzer routeAnalyzer) : base()
	{
		RouteAnalyzer = routeAnalyzer;
	}

	private Infrastructure.Security.Routing.IRouteAnalyzer RouteAnalyzer { get; }

	public System.Collections.Generic.IList<Infrastructure.Security.Routing.RouteInformation>? ViewModel { get; set; }

	public void OnGet()
	{
		ViewModel =
			RouteAnalyzer.GetAllRouteInformations();
	}
}
