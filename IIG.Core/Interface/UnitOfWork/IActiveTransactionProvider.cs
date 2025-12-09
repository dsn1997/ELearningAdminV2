using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Interface.UnitOfWork
{
    public interface IActiveTransactionProvider
    {
        // returns the IDbContextTransaction for given dbContextName + uowId (create via factory if missing)
        IDbContextTransaction GetOrCreate(string dbContextName, Guid uowId, Func<IDbContextTransaction> factory);

        // commit/rollback all transactions for a UoW
        Task CommitTransactionsAsync(Guid uowId, CancellationToken cancellationToken = default);
        Task RollbackTransactionsAsync(Guid uowId);

        void CommitTransactions(Guid uowId);
        void RollbackTransactions(Guid uowId);
        void ReleaseTransactions(Guid uowId); // dispose and remove
    }
}
