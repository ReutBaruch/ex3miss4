using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /**routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "display", action = "Index2", id = UrlParameter.Optional }
            );*/
            routes.MapRoute("display", "display/{ip}/{port}/{time}",
            defaults: new { controller = "Flight", action = "display", ip = UrlParameter.Optional, port = UrlParameter.Optional, time = 0 });

            routes.MapRoute(
                name: "Port",
                url: "{action}/{ip}/{port}/",
                defaults: new { controller = "Flight", action = "Index", ip = UrlParameter.Optional, port = UrlParameter.Optional }
            );

            routes.MapRoute("save", "save/{ip}/{port}/{time}/{seconds}/{files}",
            defaults: new
            {
                controller = "Flight",
                action = "save",
                /*
                ip = UrlParameter.Optional,
                port = UrlParameter.Optional,
                time = UrlParameter.Optional,
                seconds = UrlParameter.Optional,
                files = UrlParameter.Optional
                */
            });

        }
    }
}
