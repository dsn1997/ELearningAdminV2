using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Collections;
using System.Diagnostics;
using System.Net;

namespace IIG.Core.Providers.Caching.Impls
{
    public class DistributedCacheProvider : IDistributedCacheProvider
    {
        private Hashtable _caches;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
        private readonly IConnectionMultiplexer _redisDb;
        public DistributedCacheProvider(IDistributedCache cache,
            IConfiguration configuration,
            IConnectionMultiplexer redisDb
            )
        {
            _cache = cache;
            _configuration = configuration;
            _redisDb = redisDb;
        }

        public IDistributedCache<TItem> Cache<TItem>() where TItem : class
        {
            var cacheName = $"{typeof(TItem).FullName}";
            var cacheType = typeof(DistributedCache<>);
            return GetCache<IDistributedCache<TItem>>(cacheType, cacheName, typeof(TItem));
        }

        public IDistributedCache<TItem, TKey> Cache<TItem, TKey>() where TItem : class
        {
            var cacheName = $"{typeof(TItem).Name}.{typeof(TKey).Name}";
            var cacheType = typeof(DistributedCache<,>);
            return GetCache<IDistributedCache<TItem, TKey>>(cacheType, cacheName, typeof(TItem), typeof(TKey));
        }

        private TCache GetCache<TCache>(Type cacheType, string cacheName, params Type[] typeArguments)
        {
            _caches ??= new Hashtable();

            if (!_caches.ContainsKey(cacheName))
            {
                var cacheInstance = Activator.CreateInstance(cacheType.MakeGenericType(typeArguments), _cache);
                _caches.Add(cacheName, cacheInstance);
            }

            return (TCache)_caches[cacheName];

        }
        public async Task<int> GetTTL(string key)
        {
            var result = await _redisDb.GetDatabase().KeyTimeToLiveAsync(key, CommandFlags.None);
            return (int)(result?.TotalSeconds ?? 0);
        }

        public async Task RemoveCacheWithStartKey(string key)
        {
            EndPoint endPoint = _redisDb.GetEndPoints().First();

            RedisKey[] keys = _redisDb.GetServer(endPoint).Keys(pattern: "*").ToArray();
            var keyNeedToRemove = keys.Where(p => p.ToString().Contains(key));
            foreach (var itemKey in keyNeedToRemove)
            {
                await _cache.RemoveAsync(itemKey);
            }
        }
    }
}
