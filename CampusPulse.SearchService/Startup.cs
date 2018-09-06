using CampusPulse.CacheManager;
using CampusPulse.Core.Domain;
using CampusPulse.SearchService.Filter;
using CampusPulse.SearchService.Manager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace CampusPulse.Trade.Service
{
    public class Startup
    {
        public static string BasePath { get; internal set; }
        public readonly ILoggerFactory loggerFactory;

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
           
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddMvc(options =>
            {
                options.Filters.Add(new SearchServiceExceptionFilterAtribute(loggerFactory));
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "redis-10772.c54.ap-northeast-1-2.ec2.cloud.redislabs.com:10772, password=6yWfcdoMDGlIv5zkRCxnRjNki2xSNO7A";
                options.InstanceName = "BookCatalog";
            });

            services.AddTransient<ISearchManager, SearchManager>();
            services.AddTransient<ICacheManager<Book>, BookCacheManager<Book>>();
            //services.AddSingleton<Serilog.ILogger, Serilog.Logger>();

            services.Configure<MvcOptions>(options =>
            {
                ////options.Filters.Add(new RequireHttpsAttribute());
            });
           

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));



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
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

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
            
           loggerFactory.AddSerilog();
            // MVC need to be the last one
            //app.UseMvc(RouteHelper.AddRoutesToConfiguration);
            app.UseMvc(routes =>
            {
                routes.MapRoute("service-status", "service-status", defaults: new { controller = "Status", Action = "Get" });
                routes.MapRoute("search-get", "search/get", defaults: new { controller = "Search" , Action = "Get" });
                routes.MapRoute("search-post", "search/set", defaults: new { controller = "Search", Action = "Post" });
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
