using CampusPulse.CacheManager;
using CampusPulse.Core.Domain;
using CampusPulse.SearchService.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace CampusPulse.SearchService
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
