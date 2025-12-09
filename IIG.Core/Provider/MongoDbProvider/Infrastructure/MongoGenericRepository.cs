using IIG.Core.Common.ConfigureModels;
using IIG.Core.Providers.MongoDbProvider.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using System.Net.Sockets;

namespace IIG.Core.Providers.MongoDbProvider.Infrastructure
{
    public class MongoGenericRepository<TDocument> : IMongoGenericRepository<TDocument>
          where TDocument : Document
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoGenericRepository(IMongoDatabase mongoDatabase)

        {
            _collection = mongoDatabase.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public virtual async Task<IEnumerable<TDocument>> FilterAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            var result = await _collection.FindAsync(filterExpression);
            return await result.ToListAsync();
        }

        public virtual async Task<TDocument> FindByIdAsync(string id)
        {
            if (ObjectId.TryParse(id, out _))
            {
                var result = await _collection.FindAsync(doc => doc.Id.Equals(id));
                return await result.SingleOrDefaultAsync();
            }

            return null;
        }

        public async Task<TDocument> FindByIdAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            var result = await _collection.FindAsync(filterExpression);
            return await result.FirstOrDefaultAsync();
        }

        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual async Task InsertOneAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public virtual async Task DeleteByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            await _collection.FindOneAndDeleteAsync(filter);
        }

        public async Task DeleteManyAsync(Expression<Func<TDocument, Guid>> filterExpression, Guid id)
        {
            var listWrites = new List<WriteModel<TDocument>>();

            var filterDefinition = Builders<TDocument>.Filter.Eq(filterExpression, id);
            listWrites.Add(new DeleteManyModel<TDocument>(filterDefinition));

            await BulkWriteAsync(listWrites);
        }

        public async Task InsertManyAsync(IEnumerable<TDocument> documents)
        {
            if (documents == null || !documents.Any()) return;

            var listWrites = new List<WriteModel<TDocument>>();
            foreach (var item in documents)
            {
                listWrites.Add(new InsertOneModel<TDocument>(item));
            }

            await BulkWriteAsync(listWrites);

        }

        public async Task DeleteManyAsync(Expression<Func<TDocument, Guid>> filterExpression, List<Guid> ids)
        {
            var listWrites = new List<WriteModel<TDocument>>();

            var filterDefinition = Builders<TDocument>.Filter.In(filterExpression, ids);
            listWrites.Add(new DeleteManyModel<TDocument>(filterDefinition));

            await BulkWriteAsync(listWrites);

        }

        public async Task CreateIndexAsync(Expression<Func<TDocument, object>> epr)
        {
            var indexKeysDefinition = Builders<TDocument>.IndexKeys.Ascending(epr);
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<TDocument>(indexKeysDefinition));
        }

        public virtual async Task<(List<TDocument> list, long total)> ListAsync(IBaseSpecification<TDocument> spec)
        {
            const string facetNameCount = "count";
            const string facetNameData = "data";

            var countFacet = AggregateFacet.Create(facetNameCount,
                    PipelineDefinition<TDocument, AggregateCountResult>.Create(new[]
                    {
                        PipelineStageDefinitionBuilder.Count<TDocument>(),
                    }));

            var dataFacet = spec.IsPagingEnabled ? AggregateFacet.Create(facetNameData,
                    PipelineDefinition<TDocument, TDocument>.Create(new[]
                    {
                        spec.IsDescending ? PipelineStageDefinitionBuilder.Sort(Builders<TDocument>.Sort.Descending(spec.OrderBy)) :
                        PipelineStageDefinitionBuilder.Sort(Builders<TDocument>.Sort.Ascending(spec.OrderBy)),
                        PipelineStageDefinitionBuilder.Skip<TDocument>(spec.Skip),
                        PipelineStageDefinitionBuilder.Limit<TDocument>(spec.Take),
                    })) : AggregateFacet.Create(facetNameData,
                    PipelineDefinition<TDocument, TDocument>.Create(new[]
                    {
                        spec.IsDescending ? PipelineStageDefinitionBuilder.Sort(Builders<TDocument>.Sort.Descending(spec.OrderBy)) :
                        PipelineStageDefinitionBuilder.Sort(Builders<TDocument>.Sort.Ascending(spec.OrderBy))
                    }));
            var option = new AggregateOptions();
            option.Collation = new Collation(spec.Locale, strength: CollationStrength.Secondary);

            var aggregation = await _collection.Aggregate(option)
                .Match(spec.Criteria)
                .Facet(countFacet, dataFacet)
                .ToListAsync();

            var count = aggregation.First()
                .Facets.First(x => x.Name == facetNameCount)
                .Output<AggregateCountResult>()?.FirstOrDefault()?.Count ?? 0;

            var data = aggregation.First()
               .Facets.First(x => x.Name == facetNameData)
               .Output<TDocument>();

            return (data.ToList(), count);
        }

        public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            var listWrites = new List<WriteModel<TDocument>>();

            var filterDefinition = Builders<TDocument>.Filter.Where(filterExpression);
            listWrites.Add(new DeleteManyModel<TDocument>(filterDefinition));
            await BulkWriteAsync(listWrites);

        }

        public async Task RemoveFileByteContent(FilterDefinition<TDocument> filterDefinition, UpdateDefinition<TDocument> updateDefinition)
        {
            await _collection.UpdateManyAsync(filterDefinition, updateDefinition);
        }

        private async Task BulkWriteAsync(IEnumerable<WriteModel<TDocument>> listWrites)
        {
            try
            {

                await _collection.BulkWriteAsync(listWrites);
            }
            catch (Exception ex)
            {
                //có lỗi do socket kết nối nên retry lại 
                try
                {

                    await _collection.BulkWriteAsync(listWrites);
                }
                catch (Exception ex2)
                {

                }
            }
        }
    }
}
