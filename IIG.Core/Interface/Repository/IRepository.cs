using IIG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Repository
{

    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        TEntity? GetById(object id);
        Task<TEntity?> GetByIdAsync(object id);

        void Insert(TEntity entity);
        Task<TEntity?> InsertAsync(TEntity entity);

        TEntity Update(TEntity entityToUpdate);
        Task<TEntity> UpdateAsync(TEntity entityToUpdate);

        IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> include);
        TEntity? FirstOrDefault(Expression<Func<TEntity, bool>>? filter = null);
        TEntity? FirstOrDefault(Guid id);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? filter = null);

        int Count(Expression<Func<TEntity, bool>>? filter = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
        //Task DeleteAsync(object id);

        //void Delete(TEntity entityToDelete);
        //Task DeleteAsync(TEntity entityToDelete);

        //void Delete(Expression<Func<TEntity, bool>> predicate);
        //Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        //TEntity? FirstOrDefault(Expression<Func<TEntity, bool>>? filter = null);
        //TEntity? FirstOrDefault(object id);

        //int Count(Expression<Func<TEntity, bool>>? filter = null);

        //TEntity FirstQueryable(Expression<Func<TEntity, bool>> predicate, string include = "");
        //IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate, string include = "");
        //IQueryable<TEntity> GetAll(string include = "");
        //IQueryable<TEntity> FromSqlRaw(string query);
    }
}
