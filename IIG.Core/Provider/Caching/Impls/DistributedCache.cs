using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace IIG.Core.Providers.Caching.Impls
{
    public class DistributedCache<TItem> : DistributedCache<TItem, string>, IDistributedCache<TItem> where TItem : class
    {
        public DistributedCache(
            IDistributedCache cache)
            : base(cache)
        {
        }
    }

    public class DistributedCache<TItem, TKey> : IDistributedCache<TItem, TKey> where TItem : class
    {
        private readonly IDistributedCache _cache;

        public DistributedCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetJson(TKey key)
        {
            var json = await _cache.GetStringAsync(key.ToString());
            return json;
        }
        public async Task<TItem> Get(TKey key)
        {
            var json = await _cache.GetStringAsync(key.ToString());

            return string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<TItem>(json);
        }
        public async Task Remove(TKey key)
        {
            await _cache.RemoveAsync(key.ToString());
        }

        public async Task Add(TKey key, TItem value)
        {
            await Add(key, value, new DistributedCacheEntryOptions());
        }
        public async Task Add(TKey key, TItem value, DistributedCacheEntryOptions options)
        {
            await _cache.SetStringAsync(key.ToString(), JsonSerializer.Serialize(value), options);
        }


        public async Task<TItem> GetOrAdd(TKey tKey, Func<Task<TItem>> factory)
        {
            return await GetOrAdd(tKey, factory, new DistributedCacheEntryOptions());
        }
        public async Task<TItem> GetOrAdd(TKey tKey, Func<Task<TItem>> factory, DistributedCacheEntryOptions options)
        {
            var item = await Get(tKey);

            if (item != null)
            {
                return item;
            }

            item = await factory();
            await Add(tKey, item, options);
            return item;
        }
    }
}
