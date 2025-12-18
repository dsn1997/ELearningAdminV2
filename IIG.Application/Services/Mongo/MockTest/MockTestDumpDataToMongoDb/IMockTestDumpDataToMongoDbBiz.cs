using IIG.Core.Common.MongoDataModels.MockTests;

namespace IIG.Web.BL.Services.Interfaces.MockTests;

public interface IMockTestDumpDataToMongoDbBiz
{
    Task DumpAsync(Guid id, bool? isTypeToeflChallenge = null, DateTime? publishedAt = null);
    Task DumpInternalAsync(Guid id, bool? isTypeToeflChallenge = null, DateTime? publishedAt = null);

    Task<MgMockTestModel> GetOrDumpAsync(Guid id, bool? isTypeToeflChallenge = null);

    Task DeleteAsync(Guid id);
}