
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace IIG.Core.Providers.Interfaces;


public interface IRedisGenericRepository<T> : IRedisGenericRepository<string, T> where T : class
{
}
public interface IRedisGenericRepository<TKey, T> where T : class
{
    Task<(string, T)> GetExact(TKey key);
    Task<T> Get(TKey key);
    Task<T> GetOrCreateWithDistributedLockAsync(
    string redisKey,
    Func<Task<T>> factory,
    int maxRetries = 3,
    int retryCount = 0,
    TimeSpan? lockExpiry = null,
    int? seconds = null);
    Task Remove(TKey key);
    Task RemoveCacheWithStartKey(TKey key);

    Task Add(TKey key, T item);
    Task Add(TKey key, T item, int second);
    Task Add(TKey key, T item, DistributedCacheEntryOptions options);
    void ChangeKeyPrefix(string keyPrefix);
    Task<int> GetTTL(TKey key);
}

public interface IRedisGenericCollectionRepository<TKey, T> : IRedisGenericRepository<string, IEnumerable<T>> where T : class
{
    Task<IEnumerable<T>> GetListItem(TKey key, Func<T, bool> predicate);
    Task<T> GetFirstOrDefault(TKey key, Func<T, bool> predicate);
    Task AddItem(TKey key, T item);
    Task AddItem(TKey key, T item, int second);
    Task<bool> UpdateItem(TKey key, Func<T, bool> predicate, Action<T> updateAction);

    Task<bool> DeleteItem(TKey key, Func<T, bool> predicate);
    Task<bool> DeleteRange(TKey key, Func<T, bool> predicate);
}

public interface IRedisGenericCollectionRepository<T> : IRedisGenericRepository<string, IEnumerable<T>> where T : class
{
    Task<IEnumerable<T>> GetListItem(string key, Func<T, bool> predicate);
    Task<T> GetFirstOrDefault(string key, Func<T, bool> predicate);
    Task AddItem(string key, T item);
    Task AddItem(string key, T item, int second);
    Task<bool> UpdateItem(string key, Func<T, bool> predicate, Action<T> updateAction);

    Task<bool> DeleteItem(string key, Func<T, bool> predicate);
    Task<bool> DeleteRange(string key, Func<T, bool> predicate);
}

public interface IRedisGenericFactory
{
    IRedisGenericRepository<T> Create<T>(string prefix = "") where T : class;
    IRedisGenericCollectionRepository<T> CreateCollection<T>(string prefix = "") where T : class;
}


