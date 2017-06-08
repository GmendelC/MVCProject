using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProductRoute",
                url: "product/{id}",
                defaults: new { controller = "Product", action = "Index" },
                constraints: new { id = "[0-9]+" }
            );

            //This route means we can access photos like this: /photo/title/my%20photo%20title
            routes.MapRoute(
                name: "ProductTitleRoute",
                url: "product/title/{title}",
                defaults: new { controller = "Product", action = "ByTitle" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
