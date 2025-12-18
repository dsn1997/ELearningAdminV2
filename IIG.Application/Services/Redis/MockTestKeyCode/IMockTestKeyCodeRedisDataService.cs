using IIG.Application.Models.MockTestKeyCode;
using IIG.Core.Common.Enums;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Core.Entities;

namespace IIG.Application.Services;

public interface IMockTestKeyCodeRedisDataService
{
    Task<MgMockTestKeyCodeModel> GetMockTestKeyCodeAsync(string keyCode);
    Task<Tuple<CourseScoring, bool>> GetTupleCourseScoring(string keyCode);
    Task<MockTestKeyCodeDetailDto> GetKeyCodeDetailByCookieAsync(Guid cookie);
    Task<MgMockTestKeyCodeModel> InsertOrUpdateKeyCodeAsync(string keyCode, MgMockTestKeyCodeModel mgMockTestKeyCode, int seconds = 0);
    Task<bool> DeleteKeyCodeAnswer(string keyCode, Func<MgKeyCodeAnswerModel, bool> predicate);
    Task<IEnumerable<MgKeyCodeAnswerModel>> GetKeyCodeAnswerAsync(string keyCode);
    Task DeleteKeyCodeAsync(string keyCode);

    Task<bool> InsertKeyCodeAnswer(string keyCode, MgKeyCodeAnswerModel model);

    //Task<int> CountMockTestKeyCodeExamining();
    //Task ClearCountMockTestKeyCodeExamining();
}