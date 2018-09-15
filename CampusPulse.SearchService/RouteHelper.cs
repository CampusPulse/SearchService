
namespace CampusPulse.SearchService
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using System;

    public static class RouteHelper
    {
        public static void AddRoutesToConfiguration(IRouteBuilder routeBuilder)
        {
            if (routeBuilder == null)
            {
                throw new ArgumentNullException(nameof(routeBuilder));
            }
            routeBuilder.MapRoute("service-status", "service-status", defaults: new { controller = "Status", Action = "Get" });
            routeBuilder.MapRoute("search-get", "search/get", defaults: new { controller = "Search", Action = "Get" });
            routeBuilder.MapRoute("search-post", "search/set", defaults: new { controller = "Search", Action = "Post" });
        }
    }
}
