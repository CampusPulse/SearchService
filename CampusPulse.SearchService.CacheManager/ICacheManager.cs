using CampusPulse.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusPulse.CacheManager
{
    public interface ICacheManager<T> 
    {
        Task<string> getAsync(string key);

        ICollection<T> get(string key);

        Task save(IEnumerable<T> books);
    }
}
