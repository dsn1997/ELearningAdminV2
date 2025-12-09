namespace IIG.Core.Providers.Caching
{
    public interface IDistributedCacheProvider
    {

        IDistributedCache<TItem> Cache<TItem>() where TItem : class;
        IDistributedCache<TItem, TKey> Cache<TItem, TKey>() where TItem : class;
        Task<int> GetTTL(string key);

        public Task RemoveCacheWithStartKey(string key);
    }
}
