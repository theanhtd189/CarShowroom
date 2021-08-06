using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShopK19PR01
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //// MODIFY HERE 
            routes.MapRoute(
                name: "Homepage",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Trangchu", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShopK19PR01.Controllers" }
            );

        }
    }
}
