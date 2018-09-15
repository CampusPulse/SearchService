using CampusPulse.Core.Service.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CampusPulse.SearchService
{
    public class Startup : ServiceStartup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
            : base(env, loggerFactory, configuration) { }

        protected override void ConfigureRoutes(IRouteBuilder routes)
        {
            RouteHelper.AddRoutesToConfiguration(routes);
        }

        protected override void configureDependency(IServiceCollection services)
        {
            DependencyConfigurationManager.AddDependencies(services);
        }
    }
}
