using IIG.Core.Interface.Repository.Dtos;
using Microsoft.EntityFrameworkCore;
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
        public EfTransactionHolder GetOrCreate<TDbContext>(Guid uowId, IDbContextFactory<TDbContext> factory) where TDbContext : DbContext;

        Task SaveChangeAsync(Guid uowId, CancellationToken cancellationToken = default);
        void SaveChange(Guid uowId);
        // commit/rollback all transactions for a UoW
        Task CommitTransactionsAsync(Guid uowId, CancellationToken cancellationToken = default);
        Task RollbackTransactionsAsync(Guid uowId);
        void CommitTransactions(Guid uowId);
        void RollbackTransactions(Guid uowId);
        void ReleaseTransactions(Guid uowId); // dispose and remove
    }
}
