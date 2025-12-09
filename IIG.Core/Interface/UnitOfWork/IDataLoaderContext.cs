using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Interface.UnitOfWork
{
    public interface IDataLoader<TKey, TValue>
    {
        Task<TValue> LoadAsync(TKey key);
        Task<ILookup<TKey, TValue>> LoadManyAsync(IEnumerable<TKey> keys);
    }
    public interface IDataLoaderContext
    {
        IDataLoader<TKey, TValue> GetOrCreate<TKey, TValue>(
            string key,
            Func<IDataLoader<TKey, TValue>> factory);
    }
}
