using IIG.Core.Common.Models.Paging;
using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;


namespace IIG.Application.Services.Mongo
{
    public class MongoMockTestService : IMongoMockTestService
    {
        private readonly IMongoGenericRepository<MgMockTestModel> _mongoMockTestRepos;
        public MongoMockTestService(IMongoGenericRepository<MgMockTestModel> mongoMockTestRepos)
        {
            _mongoMockTestRepos = mongoMockTestRepos;
        }

        public async Task<PaginationSet<MgMockTestModel>> ListSpecificationAsync(MgMockTestListRequest request)
        {
            var spec = new MockTestFilterSpecification(request);
            (List<MgMockTestModel> list, long total) =  await _mongoMockTestRepos.ListAsync(spec);

            var response = new PaginationSet<MgMockTestModel>();
            response.Items = list;
            if (request.PageNum != null) response.PageNum = request.PageNum.Value;
            if (request.PageSize != null) response.PageSize = request.PageSize.Value;
            response.TotalRecords = (int)total;

            return response;
        }

        public async Task DeleteManyAsync(Guid id)
        {
            await _mongoMockTestRepos.DeleteManyAsync(x=>x.MockTestId,id);
        }

        public async Task InsertOneAsync(MgMockTestModel input)
        {
            await _mongoMockTestRepos.InsertOneAsync(input);
        }

        public async Task<MgMockTestModel> FindByIdAsync(Guid id)
        {
            var result = await _mongoMockTestRepos.FindByIdAsync(x=>x.MockTestId == id);
            return result;
        }
    }
}
