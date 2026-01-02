using IIG.Application.Models.KeyCodes;
using IIG.Application.Models.MockTestKeyCode;
using IIG.Core.Common.MongoDataModels.Keycodes;

using System.Data;

namespace IIG.Application.Data;

public interface IMockTestKeyCodeDA 
{
    //Task InsertAsync(MockTestKeyCodeInsert request);

    //Task BatchInsertAsync(List<MockTestKeyCodeInsert> listMockTestKeyCodeInsert);

    Task<DateTime?> GetMockTestPublishedAtByKeyCode(string keyCode);

    Task<MockTestKeyCodeBasicDto> GetDetailByKeyCode(string code, string keyCode, DateTime? mocktestPublishedAt);

    //Task<IEnumerable<MockTestSectionResultDto>> GetMockTestSectionResult(IEnumerable<SectionScoreBasicDto> sectionDtos, DateTime? dateTime, DateTime? submittedDate);
    Task<IEnumerable<MockTestSectionResultDto>> GetMockTestSectionResultV2(IEnumerable<SectionScoreBasicDto> sectionDtos, DateTime? submittedDate, DateTime? publishedAt);

    Task<MockTestKeyCodeDetailDto> GetKeyCodeDetailByCookieAsync(Guid cookie);

    Task<MockTestKeyCodeDetailDto> GetKeyCodeDetailByKeyCodeAsync(string keyCode);

    Task<IEnumerable<MockTestSectionCorrectAnswerDto>> GetMockTestSectionCorrectAnswers(Guid mocktestId, IEnumerable<SectionCorrectAnswerBasic> sectionCorrectAnswerBasics, DateTime? submittedDate, DateTime? publishedAt);

    //Task<IEnumerable<string>> GetListMockTestKeyCodeNeedToAutoSubmit();

    //Task<IEnumerable<string>> GetListMockTestKeyCodeNeedToAutoDelete();

    //Task<IEnumerable<string>> GetListMockTestKeyCodeHaveNotTotalCorrectAnswer();

    //Task<IEnumerable<MgKeyCodeAnswerModel>> GetMockTestKeyCodeChoose(string keyCode);

    //Task<int> CountMockTestKeyCodeExamining();
}