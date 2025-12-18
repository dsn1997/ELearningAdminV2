using IIG.Core.Entities;
using IIG.Core.Interface;
using IIG.Core.Interface.Repository.Dtos;
using IIG.Core.Interface.UnitOfWork;
using IIG.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IIG.EntityFrameworkCore.EntityFramework.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IDbContextFactory<IIGDbContext> _dbContextFactory;
        protected readonly IUnitOfWorkManager _uowManager;
        protected readonly IActiveTransactionProvider _transactionProvider;

        public Repository(IDbContextFactory<IIGDbContext> dbContextFactory, IUnitOfWorkManager uowManager, IActiveTransactionProvider transactionProvider)
        {
            _dbContextFactory = dbContextFactory;
            _uowManager = uowManager;
            _transactionProvider = transactionProvider;
        }
        private static readonly ConcurrentDictionary<Type, bool> EntityIsDbQuery =
         new ConcurrentDictionary<Type, bool>();

        private EfTransactionHolder GetOrCreateHolder()
        {
            var uow = _uowManager.Current
                ?? throw new Exception("Repository must run inside UoW.");

            return _transactionProvider.GetOrCreate(uow.Id, _dbContextFactory);

        }


        protected async Task<DbSet<TEntity>> GetTableAsync()
        {
            var holder = GetOrCreateHolder();
            return holder.DbContext.Set<TEntity>();

        }

        protected DbSet<TEntity> GetTable()
        {
            var holder = GetOrCreateHolder();
            return holder.DbContext.Set<TEntity>();
        }

        private IQueryable<TEntity> GetQueryable()
        {
            var table = GetTable();
            return table.AsQueryable();
        }

        public IQueryable<TEntity> GetAll()
        {
            return GetQueryable().AsNoTracking();
        }
        public IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> include)
        {
            return include(GetAll());
        }

        public TEntity? GetById(object id)
        {
            return GetTable().Find(id);
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            var table = await GetTableAsync();
            return await table.FindAsync(id);
        }

        public void Insert(TEntity entity)
        {
            if (entity is ICreationAudited audit)
            {
                audit.Created = DateTime.UtcNow;
            }
            GetTable().Add(entity);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            // ensure we are using the transaction for this UoW
            var table = await GetTableAsync();
            await table.AddAsync(entity);
            return entity;
        }

        public virtual async Task InsertRangeAsync(List<TEntity> entities)
        {
            // ensure we are using the transaction for this UoW
            var table = await GetTableAsync();
            await table.AddRangeAsync(entities);
        }

        public TEntity Update(TEntity entityToUpdate)
        {
            var table = GetTable();
            if (table.Entry(entityToUpdate).State == EntityState.Detached)
            {
                table.Attach(entityToUpdate);
            }
            table.Entry(entityToUpdate).State = EntityState.Modified;

            return entityToUpdate;

        }

        public Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            entityToUpdate = Update(entityToUpdate);
            return Task.FromResult(entityToUpdate);
        }
        public async Task<int> ExecuteUpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls,CancellationToken cancellationToken = default)
        {
            var table = await GetTableAsync();
            var now = DateTime.UtcNow;

            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> finalSetter;
            if (typeof(IModifiedAudited).IsAssignableFrom(typeof(TEntity)))
            {
                finalSetter = setters => setPropertyCalls.Compile()
                    .Invoke(setters)
                    .SetProperty(
                        e => ((IModifiedAudited)e).Modified,
                        now
                    );
            }
            else
            {
                finalSetter = setPropertyCalls;
            }
            return await table.Where(predicate).ExecuteUpdateAsync(finalSetter, cancellationToken);
        }
        public async Task DeleteAsync(object id)
        {
            var entityToDelete = await GetByIdAsync(id);
            if (entityToDelete == null)
            {
                throw new Exception($"not found entity id: {id}");
            }
            await DeleteAsync(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            var table = GetTable();
            if (table.Entry(entityToDelete).State == EntityState.Detached)
            {
                table.Attach(entityToDelete);
            }
            if (entityToDelete is ISoftDelete soft)
            {
                soft.Deleted = DateTime.UtcNow;
                table.Update(entityToDelete);
            }
            else
            {
                table.Remove(entityToDelete);
            }
        }

        public virtual Task DeleteAsync(TEntity entityToDelete)
        {
            Delete(entityToDelete);
            return Task.CompletedTask;
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetTable().Where(predicate);

            // Trường hợp Soft Delete
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                query.ExecuteUpdate(setters => setters
                    .SetProperty(e => ((ISoftDelete)e).Deleted, DateTime.UtcNow)
                );
            }
            else
            {
                // Delete cứng
                query.ExecuteDelete();
            }
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var table = await GetTableAsync();
            var query = table.Where(predicate);

            // Trường hợp Soft Delete
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                await query.ExecuteUpdateAsync(setters => setters
                    .SetProperty(e => ((ISoftDelete)e).Deleted, DateTime.UtcNow)
                );
            }
            else
            {
                // Delete cứng
                await query.ExecuteDeleteAsync();
            }
        }
        public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter != null)
            {
                return GetTable().FirstOrDefault(filter);
            }
            return null;
        }
        public virtual TEntity? FirstOrDefault(Guid id)
        {

            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            Expression<Func<object>> closure = () => id;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return GetTable().FirstOrDefault(Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam));
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter != null)
            {
                var table = await GetTableAsync();
                return await table.FirstOrDefaultAsync(filter);
            }
            return null;
        }

        public virtual int Count(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter != null)
            {
                return GetTable().Count(filter);
            }
            return 0;
        }


        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter != null)
            {
                var table = await GetTableAsync();
                return await table.CountAsync(filter);
            }
            return 0;
        }


    }

}
