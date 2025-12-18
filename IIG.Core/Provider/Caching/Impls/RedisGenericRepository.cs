using IIG.Core.Providers.Caching;
using IIG.Core.Providers.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net;

namespace IIG.Core.Providers.Impls;

public class RedisGenericRepository<T> : IRedisGenericRepository<T> where T : class
{
    private readonly IDistributedCacheProvider _distributedCacheProvider;
    private readonly IConnectionMultiplexer _redisDb;

    private string _keyPrefix;
    public RedisGenericRepository(
        IDistributedCacheProvider distributedCacheProvider, 
        IConnectionMultiplexer redisDb)
    {
        _distributedCacheProvider = distributedCacheProvider;
        _redisDb = redisDb;
        _keyPrefix = GetKeyPrefix();
    }
    public async Task<(string, T)> GetExact(string key) 
    {
        var jsonData = await _distributedCacheProvider.Cache<T>().GetJson(FormatKey(key));
        T data = null;
        if(!string.IsNullOrEmpty(jsonData))
        {
            data = JsonConvert.DeserializeObject<T>(jsonData);
        }
        return (jsonData, data);
    }
    public virtual Task<T> Get(string key) => _distributedCacheProvider.Cache<T>().Get(FormatKey(key));
    public virtual async Task<T> GetOrCreateWithDistributedLockAsync(
    string redisKey,
    Func<Task<T>> factory, // hàm lấy data từ DB hoặc nơi khác
    int maxRetries = 3,
    int retryCount = 0,
    TimeSpan? lockExpiry = null,
    int? seconds = null)
    {
        lockExpiry ??= TimeSpan.FromSeconds(5);
        var lockValue = Guid.NewGuid().ToString();

        // 1. try get cache
        (var jsonText, var cachedData) = await GetExact(redisKey);
        if (jsonText != null)
        {
            return cachedData;
        }

        // 2. if cache miss
        var lockKey = $"{redisKey}_Lock";
        if (await _redisDb.GetDatabase().LockTakeAsync(lockKey, lockValue, lockExpiry.Value))
        {
            try
            {
                var data = await factory(); // gọi DB hoặc nguồn gốc
                if (seconds.HasValue)
                    await Add(redisKey, data, seconds.Value);
                else
                    await Add(redisKey, data);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await _redisDb.GetDatabase().LockReleaseAsync(lockKey, lockValue);
            }
        }
        else
        {
            // 3. try to  get cache again 
            if (retryCount < maxRetries)
            {
                await Task.Delay(1000);
                return await GetOrCreateWithDistributedLockAsync(
                    redisKey, factory,
                    maxRetries, retryCount + 1, lockExpiry);
            }
            else
            {
                // 4. Fallback
                return await factory();
            }
        }
    }
    public virtual Task Remove(string key) => _distributedCacheProvider.Cache<T>().Remove(FormatKey(key));
    public virtual Task RemoveCacheWithStartKey(string key) => _distributedCacheProvider.RemoveCacheWithStartKey(FormatKey(key));
    public virtual Task Add(string key, T item) => _distributedCacheProvider.Cache<T>().Add(FormatKey(key), item);
    public virtual Task Add(string key, T item,int second) => _distributedCacheProvider.Cache<T>().Add(FormatKey(key), item, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(second)});
    public virtual Task Add(string key, T item, DistributedCacheEntryOptions options) => _distributedCacheProvider.Cache<T>().Add(FormatKey(key), item, options);
    public virtual Task<int> GetTTL(string key) => _distributedCacheProvider.GetTTL(FormatKey(key));
    public virtual void ChangeKeyPrefix(string keyPrefix)
    {
        _keyPrefix = keyPrefix;
    }   
    public virtual string FormatKey(string key)
    {
        return $"{_keyPrefix}:{key}";
    }
    private string GetKeyPrefix()
    {
        var type = typeof(T);

        // Nếu là IEnumerable<TInner>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            var innerType = type.GetGenericArguments()[0];
            return $"List_{innerType.Name}";
        }

        // Ngược lại thì lấy tên type bình thường
        return type.Name;
    }
}

public class RedisGenericCollectionRepository<TItem> : RedisGenericRepository<IEnumerable<TItem>>, IRedisGenericCollectionRepository<TItem> where TItem : class
{
    private readonly IConnectionMultiplexer _redisDb;

    public RedisGenericCollectionRepository(IDistributedCacheProvider distributedCacheProvider, IConnectionMultiplexer redisDb) : base(distributedCacheProvider, redisDb)
    {
        _redisDb = redisDb;
    }

    public async Task<IEnumerable<TItem>> GetListItem(string key, Func<TItem, bool> predicate)
    {
        var items = await Get(key);
        if (items == null) return null;
        items = items.Where(predicate).ToList();
        return items;
    }

    public async Task<TItem> GetFirstOrDefault(string key, Func<TItem, bool> predicate)
    {
        var items = await Get(key);
        if (items == null) return null;
        var item = items.Where(predicate).FirstOrDefault();
        return item;
    }
    public async Task AddItem(string key, TItem item)
    {
        var items = await Get(key);
        if (items == null)
        {
            items = new List<TItem>();
        }
        var listItem = items.ToList();
        listItem.Add(item);
        await Add(key, listItem);
    }

    public async Task AddItem(string key, TItem item, int second)
    {
        var items = await Get(key);
        if (items == null)
        {
            items = new List<TItem>();
        }
        var listItem = items.ToList();
        listItem.Add(item);
        await Add(key, listItem,second);
    }
    public async Task<bool> UpdateItem(string key, Func<TItem, bool> predicate, Action<TItem> updateAction)
    {
        var items = await Get(key);
        if (items == null) return false;
        var item = items.FirstOrDefault(predicate);
        if (item == null) return false;
        updateAction(item);
        await Add(key, items);
        return true;
    }
    public async Task<bool> DeleteItem(string key, Func<TItem, bool> predicate)
    {
        var items = await Get(key);
        if (items == null) return false;
        var item = items.FirstOrDefault(predicate);
        if (item == null) return false;
        var listItem = items.ToList();
        listItem.Remove(item);
        await Add(key, listItem);
        return true;
    }

    public async Task<bool> DeleteRange(string key, Func<TItem, bool> predicate)
    {
        var items = await Get(key);
        if (items == null) return false;
        var listItem = items.ToList();
        listItem.RemoveAll(item=> predicate(item));
        await Add(key, listItem);
        return true;
    }
}

public class RedisGenericFactory : IRedisGenericFactory
{
    private readonly IDistributedCacheProvider _distributedCacheProvider;
    private readonly IConnectionMultiplexer _redisDb;

    public RedisGenericFactory(IDistributedCacheProvider distributedCacheProvider, IConnectionMultiplexer redisDb)
    {
        _distributedCacheProvider = distributedCacheProvider;
        _redisDb = redisDb;
    }
    public IRedisGenericRepository<T> Create<T>(string prefix = "") where T : class
    {

        var redisService = new RedisGenericRepository<T>(_distributedCacheProvider, _redisDb);
        if (!string.IsNullOrEmpty(prefix))
        {
            redisService.ChangeKeyPrefix(prefix);
        }
        return redisService;
    }
    public IRedisGenericCollectionRepository<TItem> CreateCollection<TItem>(string prefix = "") where TItem : class
    {
        var redisService = new RedisGenericCollectionRepository<TItem>(_distributedCacheProvider, _redisDb);
        if (!string.IsNullOrEmpty(prefix))
        {
            redisService.ChangeKeyPrefix(prefix);
        }
        return redisService;
    }



}
