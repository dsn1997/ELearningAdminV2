using IIG.Core.Providers.MongoDbProvider.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace IIG.Core.Providers.MongoDbProvider.Infrastructure
{
    public interface IMongoGenericRepository<TDocument>
         where TDocument : Document
    {
        Task<IEnumerable<TDocument>> FilterAsync(Expression<Func<TDocument, bool>> filterExpression);

        Task<TDocument> FindByIdAsync(string id);

        Task<TDocument> FindByIdAsync(Expression<Func<TDocument, bool>> filterExpression);

        Task InsertOneAsync(TDocument document);

        Task ReplaceOneAsync(TDocument document);

        Task DeleteByIdAsync(string id);

        Task DeleteManyAsync(Expression<Func<TDocument, Guid>> filterExpression, Guid id);

        Task DeleteManyAsync(Expression<Func<TDocument, Guid>> filterExpression, List<Guid> ids);

        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);

        Task InsertManyAsync(IEnumerable<TDocument> documents);

        Task CreateIndexAsync(Expression<Func<TDocument, object>> epr);

        Task<(List<TDocument> list, long total)> ListAsync(IBaseSpecification<TDocument> spec);

        Task RemoveFileByteContent(FilterDefinition<TDocument> filterDefinition,
            UpdateDefinition<TDocument> updateDefinition);
    }
}
