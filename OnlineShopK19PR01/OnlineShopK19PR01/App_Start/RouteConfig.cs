using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShopK19PR01
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "About",
                url: "{gioi-thieu}",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShopK19PR01.Controllers" }
            );
            routes.MapRoute(
                name: "Product Category",
                url: "{san-pham}/{metatitle}-{cateId}",
                defaults: new { controller = "Product", action = "CategoryDetail", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShopK19PR01.Controllers" }
            );

            routes.MapRoute(
                name: "Product",
                url: "san-pham/{id}",
                defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShopK19PR01.Controllers" }
            );

            routes.MapRoute(
               name: "Add Cart",
               url: "them-gio-hang/{id}/{quantity}",
               defaults: new { controller = "Cart", action = "AddCart", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShopK19PR01.Controllers" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new[] {"OnlineShopK19PR01.Controllers"}
            );
        }
    }
}
