using Microsoft.Extensions.DependencyInjection;
using IIG.Core.Repository;
using IIG.Core.Entities;
using IIG.Core.Interface.UnitOfWork;

namespace IIG.Core.Interface
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Guid Id { get; }
        IUnitOfWork Outer { get; } // nếu nested
        Task CompleteAsync();
        bool IsCompleted { get; }

        // DataLoader per UoW (simple)
        IDataLoaderContext DataLoader { get; }
    }

    public interface IUnitOfWorkManager
    {
        IUnitOfWork Current { get; }
        IUnitOfWork Begin(); // creates new UoW and sets Current
    }
}
