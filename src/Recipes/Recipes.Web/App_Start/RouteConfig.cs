using System.Web.Mvc;
using System.Web.Routing;

namespace Recipes.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Maps all routes to Home controller and Index action which serves a Single Page ReactJS application
            routes.MapRoute(
                name: "Default",
                url: "{*.}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
