using IIG.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.EntityFrameworkCore.EntityFramework.UnitOfWork
{
    public class BatchDataLoader<TKey, TValue> : IDataLoader<TKey, TValue>
    {
        private readonly Func<IReadOnlyList<TKey>, Task<IReadOnlyList<TValue>>> _batchFunc;

        private readonly Dictionary<TKey, TaskCompletionSource<TValue>> _cache = new();
        private readonly List<TKey> _queue = new();
        private Task _dispatchTask;
        private readonly object _lock = new();

        public BatchDataLoader(Func<IReadOnlyList<TKey>, Task<IReadOnlyList<TValue>>> batchFunc)
        {
            _batchFunc = batchFunc;
        }

        public Task<TValue> LoadAsync(TKey key)
        {
            lock (_lock)
            {
                if (_cache.TryGetValue(key, out var existing))
                    return existing.Task;

                var tcs = new TaskCompletionSource<TValue>();
                _cache[key] = tcs;
                _queue.Add(key);

                if (_dispatchTask == null)
                    _dispatchTask = DispatchAsync();

                return tcs.Task;
            }
        }

        private async Task DispatchAsync()
        {
            await Task.Yield();

            List<TKey> keys;

            lock (_lock)
            {
                keys = _queue.ToList();
                _queue.Clear();
                _dispatchTask = null;
            }

            var results = await _batchFunc(keys);

            for (int i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                var value = results[i];
                _cache[key].SetResult(value);
            }
        }

        public async Task<ILookup<TKey, TValue>> LoadManyAsync(IEnumerable<TKey> keys)
        {
            var tasks = keys.Select(LoadAsync);
            var values = await Task.WhenAll(tasks);

            return values.ToLookup(x => keys.First());
        }
    }
}
