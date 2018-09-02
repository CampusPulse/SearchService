using CampusPulse.CacheManager;
using CampusPulse.Core.Domain;
using CampusPulse.SearchService.Domain.Model;
using System.Collections.Generic;

namespace CampusPulse.SearchService.Manager
{
    public class SearchManager :ISearchManager
    {
        private readonly ICacheManager<Book> cacheManager;
        public SearchManager(ICacheManager<Book> cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public ICollection<Book> GetBooks(BookFilter bookFilter)
        {
            return cacheManager.get(bookFilter.Acedamics);
        }
    }
}
