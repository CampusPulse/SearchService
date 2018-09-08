using CampusPulse.SearchService.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CampusPulse.Trade.Service
{
    public class Startup
    {
        public static string BasePath { get; internal set; }
        public readonly ILoggerFactory loggerFactory;
        public readonly IConfiguration configuration;

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.loggerFactory = loggerFactory;
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            MvcConfigurationManager.ConfigureService(services);
            CacheConfigurationManager.ConfigureService(services, this.configuration);
            LoggingConfigurationManager.ConfigureService(services);
            DependencyConfigurationManager.AddDependencies(services);
            //services.AddSingleton<Serilog.ILogger, Serilog.Logger>();

            /*services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("example.com");
                options.ExcludedHosts.Add("www.example.com");
            });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddSerilog();

            ConfigureHttpsEndpoints(app, env);

            app.UseMvc(routes =>  RouteHelper.AddRoutesToConfiguration(routes));

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private void ConfigureHttpsEndpoints(IApplicationBuilder app, IHostingEnvironment env)
        {
            int? httpsPort = null;
            var httpsSection = configuration.GetSection("HttpServer:Endpoints:Https");
            if (httpsSection.Exists())
            {
                var httpsEndpoint = new EndpointConfiguration();
                httpsSection.Bind(httpsEndpoint);
                httpsPort = httpsEndpoint.Port;
            }
            var statusCode = env.IsDevelopment() ? StatusCodes.Status302Found : StatusCodes.Status301MovedPermanently;

            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(statusCode, httpsPort));
        }
    }
}
