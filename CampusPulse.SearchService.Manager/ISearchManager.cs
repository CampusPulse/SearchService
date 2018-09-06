using CampusPulse.Core.Domain;
using CampusPulse.SearchService.Domain.Model;
using System.Collections.Generic;

namespace CampusPulse.SearchService.Manager
{
    public interface ISearchManager
    {
        ICollection<Book> GetBooks(BookFilter bookFilter);

       void SaveBook();

    }
}
