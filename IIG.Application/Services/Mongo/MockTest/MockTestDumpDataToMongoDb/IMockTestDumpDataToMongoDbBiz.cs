using IIG.Core.Common.MongoDataModels.MockTests;

namespace IIG.Application.Services.Mongo;

public interface IMockTestDumpDataToMongoDbBiz
{
    Task DumpAsync(Guid id, bool? isTypeToeflChallenge = null, DateTime? publishedAt = null);
    Task DumpInternalAsync(Guid id, bool? isTypeToeflChallenge = null, DateTime? publishedAt = null);

    Task<MgMockTestModel> GetOrDumpAsync(Guid id, bool? isTypeToeflChallenge = null);

    Task DeleteAsync(Guid id);
}