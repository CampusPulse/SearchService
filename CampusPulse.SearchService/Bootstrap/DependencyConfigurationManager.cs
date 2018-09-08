using CampusPulse.CacheManager;
using CampusPulse.Core.Domain;
using CampusPulse.SearchService.Manager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusPulse.SearchService.Bootstrap
{
    public class DependencyConfigurationManager
    {
        public static void AddDependencies(IServiceCollection services)
        {
            services.AddTransient<ISearchManager, SearchManager>();
            services.AddTransient<ICacheManager<Book>, BookCacheManager<Book>>();
        }
    }
}
