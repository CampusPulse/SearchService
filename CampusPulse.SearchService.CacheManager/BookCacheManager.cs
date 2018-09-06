using CampusPulse.Core.Domain;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusPulse.CacheManager
{
    public class BookCacheManager<T> : ICacheManager<T> where T : Book
    {
        private readonly IDistributedCache cache;

        public BookCacheManager(IDistributedCache cache)
        {
            this.cache = cache;

        }

        public async Task<string> getAsync(string key)
        {
            return await cache.GetStringAsync(key);
        }

        public ICollection<T> get(string key)
        {
            var data = cache.GetStringAsync("test").Result;

            return JsonConvert.DeserializeObject<ICollection<T>>(data);
        }

        public async Task save(IEnumerable<T>books)
        {
            await cache.SetStringAsync("test", JsonConvert.SerializeObject(books));
        }
    }
}
