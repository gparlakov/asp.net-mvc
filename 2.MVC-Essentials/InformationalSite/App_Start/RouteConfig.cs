using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace InformationalSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AdminStart",
                url: "{pre}n{controller}/{action}/{id}",
                defaults: new 
                { 
                    id = UrlParameter.Optional
                },
                constraints: new 
                {
                    pre = "admi" /// the {pre} and {controller} must be separated by a string literal so -> {admi}n{controller}
                }
            );


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
