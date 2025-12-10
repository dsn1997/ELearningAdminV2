using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Core.Entities;

namespace IIG.Web.BL.Services.Interfaces;

public interface IMockTestRedisDataService
{
    Task<MgMockTestModel> GetMockTest(Guid id, int expiredInSeconds = 60);
    Task<MgMockTestModel> InsertOrUpdateMockTestAsync(Guid id, MgMockTestModel model,  int expiredInSeconds = 0);
    Task<MocktestTranslation> GetMockTestTranslation(Guid mockTestId);
    Task<IEnumerable<MocktestSection>> GetMockTestSections(Guid mockTestId);
}