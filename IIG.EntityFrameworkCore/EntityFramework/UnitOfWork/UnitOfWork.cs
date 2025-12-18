using IIG.Core.Interface;
using IIG.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.EntityFrameworkCore.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUnitOfWorkManager _manager;
        private readonly IActiveTransactionProvider _transactionProvider;
        private bool _disposed;
        private bool _completed;

        public Guid Id { get; } = Guid.NewGuid();
        public IUnitOfWork Outer { get; }
        public IDataLoaderContext DataLoader { get; } = new DataLoaderContext();
        public bool IsCompleted => _completed;

        public UnitOfWork(IUnitOfWorkManager manager, IActiveTransactionProvider transactionProvider, IUnitOfWork outer = null)
        {
            _manager = manager;
            _transactionProvider = transactionProvider;
            Outer = outer;
        }

        public async Task CompleteAsync()
        {
            // Commit any active transactions created under this UoW
            await _transactionProvider.CommitTransactionsAsync(Id);
            _completed = true;
        }

        public async Task SaveChangesAsync()
        {
            // Commit any active transactions created under this UoW
            await _transactionProvider.SaveChangeAsync(Id);
            _completed = true;
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;

            try
            {
                if (!_completed)
                {
                    // rollback if not completed
                    await _transactionProvider.RollbackTransactionsAsync(Id);
                }
            }
            finally
            {
                // always release (dispose) transaction objects and remove entries
                _transactionProvider.ReleaseTransactions(Id);

                // restore previous current UoW
                (_manager as UnitOfWorkManager)?.RestoreOuter(Outer);

                _disposed = true;
            }
        }
    }
}
