using System.Web.Mvc;
using System.Web.Routing;

namespace DeckOfPlayingCards.Web
{
    /// <summary>
    /// Configure all different routes using this class. By default, all requests handle by default route.
    /// It matches incoming requests that would not otherwise match a file on the file system and maps the
    /// requests to a controller action. It constructs outgoing URLs that correspond to controller actions.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registering the routes to the application and ignoring some routes.
        /// </summary>
        /// <param name="routes">Collection of the routes from the route table.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Enable attribute routing for MVC application.
            routes.MapMvcAttributeRoutes();
        }
    }
}
