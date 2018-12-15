using TenderSearch.Contracts.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;

namespace TenderSearch.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapMvcAttributeRoutes(); //Enables Attribute Routing

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default_children",
            //    url: "{controller}/{parentId}/{action}",
            //  //  defaults: new { action = "IndexWithParent" },
            //    namespaces: new[] { "TenderSearch.Web.Controllers" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = ActionNames.Index, id = UrlParameter.Optional },
                namespaces: new[] { "TenderSearch.Web.Controllers" }
            );
        }
    }
}
