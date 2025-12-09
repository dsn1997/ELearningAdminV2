using Microsoft.Extensions.Caching.Distributed;

namespace IIG.Core.Providers.Caching
{
    public interface IDistributedCache<TItem> : IDistributedCache<TItem, string> where TItem : class
    {
    }

    public interface IDistributedCache<TItem, TKey> where TItem : class
    {
        Task<string> GetJson(TKey key);

        Task<TItem> Get(TKey key);
        Task Remove(TKey key);

        Task Add(TKey key, TItem item);
        Task Add(TKey key, TItem item, DistributedCacheEntryOptions options);

        Task<TItem> GetOrAdd(TKey key, Func<Task<TItem>> factory);
        Task<TItem> GetOrAdd(TKey key, Func<Task<TItem>> factory, DistributedCacheEntryOptions options);
    }
}
