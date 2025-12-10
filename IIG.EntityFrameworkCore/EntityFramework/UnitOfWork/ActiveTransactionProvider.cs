using IIG.Core.Interface;
using IIG.Core.Interface.Repository.Dtos;
using IIG.Core.Interface.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.EntityFrameworkCore.EntityFramework.UnitOfWork
{
    public class ActiveTransactionProvider : IActiveTransactionProvider
    {
        private readonly Dictionary<(Guid UowId, Type DbContextType), EfTransactionHolder> _txs = new();

        private string Key(string dbContextName, Guid uowId) => $"{dbContextName}|{uowId}";

        public EfTransactionHolder GetOrCreate<TDbContext>(
        Guid uowId,
        IDbContextFactory<TDbContext> factory)
        where TDbContext : DbContext
        {
            var key = (uowId, typeof(TDbContext));

            if (_txs.TryGetValue(key, out var holder))
                return holder;

            var ctx = factory.CreateDbContext();
            if (ctx.Database.GetDbConnection().State != System.Data.ConnectionState.Open)
                ctx.Database.OpenConnection();

            var trx = ctx.Database.BeginTransaction();

            holder = new EfTransactionHolder
            {
                DbContext = ctx,
                Transaction = trx
            };

            _txs[key] = holder;
            return holder;
        }
        public async Task SaveChangeAsync(Guid uowId, CancellationToken cancellationToken = default)
        {
            var keys = _txs.Keys.Where(k => k.UowId == uowId).ToList();
            foreach (var key in keys)
            {
                if (_txs.TryGetValue(key, out var tx))
                {
                    await tx.DbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }

        public  void SaveChange(Guid uowId)
        {
            var keys = _txs.Keys.Where(k => k.UowId == uowId).ToList();
            foreach (var key in keys)
            {
                if (_txs.TryGetValue(key, out var tx))
                {
                     tx.DbContext.SaveChanges();
                }
            }
        }

        public async Task CommitTransactionsAsync(Guid uowId, CancellationToken cancellationToken = default)
        {
            var keys = _txs.Keys.Where(k => k.UowId == uowId).ToList();
            foreach (var key in keys)
            {
                if (_txs.TryGetValue(key, out var tx))
                {
                    await tx.DbContext.SaveChangesAsync(cancellationToken);
                    await tx.Transaction.CommitAsync(cancellationToken);
                }
            }
        }

        public void CommitTransactions(Guid uowId)
        {
            var keys = _txs.Keys.Where(k => k.UowId == uowId).ToList();
            foreach (var key in keys)
            {
                if (_txs.TryGetValue(key, out var tx))
                {
                    tx.DbContext.SaveChanges();
                    tx.Transaction.Commit();
                }
            }
        }

        public async Task RollbackTransactionsAsync(Guid uowId)
        {
            var keys = _txs.Keys.Where(k => k.UowId == uowId).ToList();
            foreach (var key in keys)
            {
                if (_txs.Remove(key, out var tx))
                {
                    try { await tx.Transaction.RollbackAsync(); } catch { /* swallow */ }
                    try { tx.Transaction.Dispose(); } catch { }
                    try { tx.DbContext.Dispose(); } catch { }
                }
            }
        }

        public void RollbackTransactions(Guid uowId)
        {
            var keys = _txs.Keys.Where(k => k.UowId == uowId).ToList();
            foreach (var key in keys)
            {
                if (_txs.Remove(key, out var tx))
                {
                    try { tx.Transaction.Rollback(); } catch { /* swallow */ }
                    try { tx.Transaction.Dispose(); } catch { }
                    try { tx.DbContext.Dispose(); } catch { }
                }
            }
        }

        public void ReleaseTransactions(Guid uowId)
        {
            var keys = _txs.Keys.Where(k => k.UowId == uowId).ToList();
            foreach (var key in keys)
            {
                if (_txs.Remove(key, out var tx))
                {
                    try { tx.Transaction.Dispose(); } catch { }
                    try { tx.DbContext.Dispose(); } catch { }
                }
            }
        }
    }
}
