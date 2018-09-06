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

        public void SaveBook()
        {
            List<Book> books = new List<Book>();
            Book book = new Book()
            {
                Acedamics = "A1",
                Author = "auth1",
                Description = "desc",
                Year = 1990
            };
            Book book1 = new Book()
            {
                Acedamics = "A1",
                Author = "auth1",
                Description = "desc",
                Year = 1990
            };
            books.Add(book);
            books.Add(book1);
            cacheManager.save(books as IReadOnlyCollection<Book>);
        }
    }
}
