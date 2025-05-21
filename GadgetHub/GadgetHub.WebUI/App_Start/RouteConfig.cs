using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GadgetHub.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "", new
            {
                controller = "Gadget",
                action = "List",
                category = (string)null,
                page = 1
            });

            routes.MapRoute(null, "Page{page}", new
            {
                controller = "Gadget",
                action = "List",
                category = (string)null,
            },
            new { page = @"\d+" });

            routes.MapRoute(null, "{category}", new
            {
                controller = "Gadget",
                action = "List",
                page = 1
            });


            // page 2 - List the specified page (in this case page 2)
            // showing items from all categories

            routes.MapRoute(null, "{category}/Page{page}", new
            {
                controller = "Gadget",
                action = "List",
            },
            new { page = @"\d+" });
            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
