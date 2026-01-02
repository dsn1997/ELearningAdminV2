using IIG.Core.Common.Models.Paging;
using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using System.Linq.Expressions;

namespace IIG.Application.Services.Mongo
{
    public interface IMongoMockTestService 
    {
        Task<PaginationSet<MgMockTestModel>> ListSpecificationAsync(MgMockTestListRequest request);
        Task<MgMockTestModel> FindByIdAsync(Guid id);
        Task InsertOneAsync(MgMockTestModel input);
        Task DeleteManyAsync(Guid id);
    }
}
