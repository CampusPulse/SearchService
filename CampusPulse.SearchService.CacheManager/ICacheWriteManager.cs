using CampusPulse.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusPulse.CacheManager
{
    public interface ICacheWriteManager<T> 
    {
        Task<string> setAsync(string key);

        ICollection<T> set(string key);

        Task save(IEnumerable<T> books);
    }
}
