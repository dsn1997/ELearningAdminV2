using IIG.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.EntityFrameworkCore.EntityFramework.UnitOfWork
{
    public class DataLoaderContext : IDataLoaderContext
    {
        private readonly Dictionary<string, object> _loaders = new();

        public IDataLoader<TKey, TValue> GetOrCreate<TKey, TValue>(
            string key,
            Func<IDataLoader<TKey, TValue>> factory)
        {
            if (_loaders.TryGetValue(key, out var existing))
                return (IDataLoader<TKey, TValue>)existing;

            var loader = factory();
            _loaders[key] = loader;
            return loader;
        }
    }
}
