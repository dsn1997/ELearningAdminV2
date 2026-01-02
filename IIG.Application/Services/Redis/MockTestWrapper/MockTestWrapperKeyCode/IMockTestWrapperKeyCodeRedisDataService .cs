using IIG.Core.Common.MongoDataModels.Keycodes;


namespace IIG.Application.Services.Redis;

public interface IMockTestWrapperKeyCodeRedisDataService
{
    Task<MgMockTestKeyCodeModel> GetMockTestKeyCodeInfoByUser(Guid mockTestWrapperId, Guid webUserId);
    Task InsertMockTestKeyCodeInfoByUser(Guid mockTestWrapperId, Guid webUserId, MgMockTestKeyCodeModel model);
    Task DeleteMockTestKeyCodeInfoByUser(Guid mockTestWrapperId, Guid webUserId);
}