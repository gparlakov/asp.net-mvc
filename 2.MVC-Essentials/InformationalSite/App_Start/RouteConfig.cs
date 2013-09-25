using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InformationalSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //
            // MotoSpecialRoute/Moto/123 - 3 digits
            routes.MapRoute(
                name: "AdminSpecialRoute",
                url: "{special}/{number}",
                defaults: new
                {
                    controller = "Secret",
                    action = "Number"
                },
                constraints: new 
                {
                    special= "MotoSpecialRoute",
                    number = @"[\w]{3}"
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new 
                { 
                    controller = "Home", 
                    action = "Index", 
                    id = UrlParameter.Optional 
                }
            );
        }
    }
}
