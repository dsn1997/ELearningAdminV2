using IIG.Application.Models.KeyCodes;
using IIG.Application.Models.MockTestKeyCode;
using IIG.Core.Base;
using IIG.Core.Common.Enums;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Entities;
using IIG.Core.Helper;
using IIG.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IIG.Application.Data;

public class MockTestKeyCodeDA : IMockTestKeyCodeDA
{
    private readonly IAppFactory _appFactory;

    public MockTestKeyCodeDA(IAppFactory appFactory)
    {
        _appFactory = appFactory;
    }

    //public async Task InsertAsync(MockTestKeyCodeInsert request)
    //{
    //    DynamicParameters param = new DynamicParameters()
    //        .AddParam("@code", request.Code)
    //        .AddParam("@mocktest_id", request.MocktestId)
    //        .AddParam("@web_user_id", request.WebUserId)
    //        .AddParam("@course_id", request.CourseId)
    //        .AddParam("@order_id", request.OrderId)
    //        .AddParam("@course_price_id", request.CoursePriceId)
    //        .AddParam("@start_date", request.StartDate)
    //        .AddParam("@end_date", request.EndDate)
    //        .AddParam("@started_doing_exam_date", request.StartedDoingExamDate)
    //        .AddParam("@submitted_date", request.SubmittedDate)
    //        .AddParam("@client_ip", request.ClientIp)
    //        .AddParam("@browser", request.Browser)
    //        .AddParam("@time_remaining", request.TimeRemaining)
    //        .AddParam("@mocktest_published_at", request.MocktestPublishedAt)
    //        .AddParam("@type", request.Type)
    //        .AddParam("@challenge_name", request.ChallengeName)
    //        .AddParam("@identification_number", request.IdentificationNumber);

    //    await _iIGLmsdbContext.Database.GetDbConnection()
    //        .ExecuteAsync("[dbo].[mocktest_key_code_insert]", param, commandType: CommandType.StoredProcedure);
    //}

    //public async Task BatchInsertAsync(List<MockTestKeyCodeInsert> listMockTestKeyCode)
    //{
    //    var dataTable = GenerateDataTableMockTestKeyCode(listMockTestKeyCode);

    //    DynamicParameters param = new DynamicParameters()
    //        .AddParam("@list_mock_test_key_code", dataTable.AsTableValuedParameter("[dbo].[mocktest_key_code_type]"));

    //    await _iIGLmsdbContext.Database.GetDbConnection().ExecuteAsync("[dbo].[mocktest_key_code_batch_insert]", param, commandType: CommandType.StoredProcedure);
    //}

    public async Task<DateTime?> GetMockTestPublishedAtByKeyCode(string keyCode)
    {
      var result = await _appFactory.Repository<MocktestKeyCode>().GetAll().Where(f => f.Code == keyCode).Select(f => f.MocktestPublishedAt).FirstOrDefaultAsync();
        return result;
    }

    public async Task<MockTestKeyCodeBasicDto> GetDetailByKeyCode(string keyCode, string languageCode, DateTime? mocktestPublishedAt)
    {
        var query = from mtkc in _appFactory.Repository<MocktestKeyCode>().GetAll()
                    where mtkc.Code == keyCode 
                    select new MockTestKeyCodeBasicDto
                    {
                        Code = mtkc.Code,
                        MockTestId = mtkc.MocktestId,
                        MockTestName = mtkc.Mocktest.MocktestTranslations.Where(p => p.LanguageCode == languageCode)
                                                                          .Select(p=>p.Name)
                                                                          .FirstOrDefault(),
                        SubmittedDate = mtkc.SubmittedDate,
                        MockTestScoreType = mtkc.Mocktest.ScoreType!= null? mtkc.Mocktest.ScoreType : EMockTestScoreType.CorrectScore,
                        LinkUrlViewMore = mtkc.Mocktest.LinkUrlViewMore,
                        TextViewMore = mtkc.Mocktest.TextViewMore,
                    };
        var resultDto = await query.FirstOrDefaultAsync();
        return resultDto;
    }

    //public async Task<IEnumerable<MockTestSectionResultDto>> GetMockTestSectionResult(IEnumerable<SectionScoreBasicDto> sectionDtos, DateTime? submittedDate, DateTime? publishedAt)
    //{
    //    var dataTable = GenerateDataTableMockTestSectionType(sectionDtos);
    //    DynamicParameters param = new DynamicParameters()
    //        .AddParam("@list_mocktest_section", dataTable.AsTableValuedParameter("[dbo].[mocktest_section_result_type]"))
    //        .AddParam("@submitted_at", submittedDate)
    //        .AddParam("@mockest_published_at", publishedAt);

    //    return await _iIGLmsdbContext.Database.GetDbConnection().QueryAsync<MockTestSectionResultDto>("[dbo].[web_mocktest_section_result_list]", param,
    //        commandType: CommandType.StoredProcedure);
    //}

    //private DataTable GenerateDataTableMockTestSectionType(IEnumerable<SectionScoreBasicDto> sectionDtos)
    //{
    //    var output = new DataTable();

    //    output.Columns.Add("id", typeof(Guid));
    //    output.Columns.Add("score", typeof(int));
    //    foreach (var section in sectionDtos)
    //    {
    //        output.Rows.Add(section.SectionId, section.Score);
    //    }

    //    return output;
    //}

    //private DataTable GenerateDataTableMockTestKeyCode(List<MockTestKeyCodeInsert> mockTestKeyCodeInserts)
    //{
    //    var output = new DataTable();
    //    output.Columns.Add("code", typeof(string));
    //    output.Columns.Add("mocktest_id", typeof(Guid));
    //    output.Columns.Add("web_user_id", typeof(Guid));
    //    output.Columns.Add("course_id", typeof(Guid));
    //    output.Columns.Add("order_id", typeof(Guid));
    //    output.Columns.Add("course_price_id", typeof(Guid));
    //    output.Columns.Add("start_date", typeof(DateTime));
    //    output.Columns.Add("end_date", typeof(DateTime));
    //    output.Columns.Add("started_doing_exam_date", typeof(DateTime));
    //    output.Columns.Add("submitted_date", typeof(DateTime));
    //    output.Columns.Add("client_ip", typeof(string));
    //    output.Columns.Add("browser", typeof(string));
    //    output.Columns.Add("time_remaining", typeof(int));
    //    output.Columns.Add("mocktest_published_at", typeof(DateTime));
    //    output.Columns.Add("type", typeof(int));
    //    output.Columns.Add("challenge_name", typeof(string));
    //    output.Columns.Add("identification_number", typeof(string));
    //    output.Columns.Add("full_name", typeof(string));

    //    foreach (var item in mockTestKeyCodeInserts)
    //    {
    //        output.Rows.Add(item.Code, item.MocktestId, item.WebUserId, item.CourseId, item.OrderId, item.CoursePriceId, item.StartDate, item.EndDate, item.StartedDoingExamDate, item.SubmittedDate, item.ClientIp, item.Browser, item.TimeRemaining, item.MocktestPublishedAt, item.Type, item.ChallengeName, item.IdentificationNumber, null);
    //    }

    //    return output;
    //}

    public async Task<MockTestKeyCodeDetailDto> GetKeyCodeDetailByCookieAsync(Guid cookie)
    {
        var exist = await _appFactory.Repository<MocktestKeyCode>().GetAll().Where(f => f.Cookie == cookie && f.IsAutoGenerate != true).Select(p => new MockTestKeyCodeDetailDto
        {
            Code = p.Code,
            Browser = p.Browser,
            ChallengeName = p.ChallengeName,
            IdentificationNumber = p.IdentificationNumber,
            ClientIp = p.ClientIp,
            Cookie = p.Cookie,
            CourseId = p.CourseId,
            CoursePriceId = p.CoursePriceId,
            Created = p.Created,
            EndDate = p.EndDate,
            IsAutoGenerate = p.IsAutoGenerate,
            MocktestId = p.MocktestId,
            MocktestPublishedAt = p.MocktestPublishedAt,
            Modified = p.Modified,
            OrderId = p.OrderId,
            StartDate = p.StartDate,
            StartedDoingExamDate = p.StartedDoingExamDate,
            SubmittedDate = p.SubmittedDate,
            TimeRemaining = p.TimeRemaining,
            Type = (EMockTestKeyCodeType)p.Type,
            WebUserId = p.WebUserId
        }).FirstOrDefaultAsync();
        return exist;
    }

    public async Task<MockTestKeyCodeDetailDto> GetKeyCodeDetailByKeyCodeAsync(string keyCode)
    {
        var mockTestKeyCode = await (from data in  _appFactory.Repository<MocktestKeyCode>().GetAll()
                                    join mockTest in _appFactory.Repository<Mocktest>().GetAll() on data.MocktestId equals mockTest.Id
                                    where data.Code == keyCode
                                     select new MockTestKeyCodeDetailDto
                                    {
                                        Code = keyCode,
                                        Browser = data.Browser,
                                        ChallengeName = data.ChallengeName,
                                        IdentificationNumber = data.IdentificationNumber,
                                        ClientIp = data.ClientIp,
                                        Cookie = data.Cookie,
                                        CourseId = data.CourseId,
                                        CoursePriceId = data.CoursePriceId,
                                        Created = data.Created,
                                        EndDate = data.EndDate,
                                        IsAutoGenerate = data.IsAutoGenerate,
                                        MocktestId = data.MocktestId,
                                        MocktestPublishedAt = data.MocktestPublishedAt,
                                        Modified = data.Modified,
                                        OrderId = data.OrderId,
                                        StartDate = data.StartDate,
                                        StartedDoingExamDate = data.StartedDoingExamDate,
                                        SubmittedDate = data.SubmittedDate,
                                        TimeRemaining = data.TimeRemaining,
                                        Type = (EMockTestKeyCodeType)data.Type,
                                        WebUserId = data.WebUserId,
                                       MockTestObjectId = mockTest.MocktestObjectId,
                                       MockTestTypeId = mockTest.MocktestTypeId,

                                    }).FirstOrDefaultAsync();
        return mockTestKeyCode;

    }

    public async Task<IEnumerable<MockTestSectionCorrectAnswerDto>> GetMockTestSectionCorrectAnswers(Guid mocktestId, IEnumerable<SectionCorrectAnswerBasic> sectionCorrectAnswerBasics, DateTime? submittedDate, DateTime? publishedAt)
    {
        var query = from mts in _appFactory.Repository<MocktestSection>().GetAll()
                    where mts.MocktestId == mocktestId
                    orderby mts.SortOrder
                    select new MockTestSectionCorrectAnswerDto
                    {
                        SectionId = mts.Id,
                        NumberOfCorrectAnswer = sectionCorrectAnswerBasics.FirstOrDefault(f => f.MockTestSectionId == mts.Id) != null ? sectionCorrectAnswerBasics.FirstOrDefault(f => f.MockTestSectionId == mts.Id).NumberOfCorrectAnswer : 0,
                        SectionName = mts.Name,
                        RankingScoreId = mts.RankingScoreId,       
                        MinScore = mts.RankingScore.MinScore,
                        MaxScore = mts.RankingScore.MaxScore,
                    };
        var resultDto = await query.ToListAsync();
        if (resultDto.Any())
            return resultDto;

        // lấy thông tin ranking score
        var rankingScoreIds = resultDto.Where(f => f.RankingScoreId != null).Select(f => f.RankingScoreId).Distinct().ToList();

        var scoreDetails = await _appFactory.Repository<ScoreDetail>().GetAll()
            .Where(p => rankingScoreIds.Contains(p.RankingScoreId)).Select(p => new
            {
                RankingScoreId = p.RankingScoreId,
                FromScore = p.FromScore,
                ToScore = p.ToScore,
                ExactScore = p.ExactScore,
                NumberOfCorrectQuestions = p.NumberOfCorrectQuestions,
            }).ToListAsync();
        // map thông tin ranking score
        resultDto = resultDto.Select(p =>
        {
            p.NumberOfCorrectAnswer = sectionCorrectAnswerBasics.FirstOrDefault(f => f.MockTestSectionId == p.SectionId)?.NumberOfCorrectAnswer ?? 0;
            var scoreDetailForRanking = scoreDetails.Where(f => f.RankingScoreId == p.RankingScoreId && f.NumberOfCorrectQuestions == p.NumberOfCorrectAnswer).FirstOrDefault();
            if (scoreDetailForRanking != null)
            {
                p.FromScore = scoreDetailForRanking.FromScore;
                p.ToScore = scoreDetailForRanking.ToScore;
                p.ExactScore = scoreDetailForRanking.ExactScore;
            }
            return p;
        }).ToList();

        return resultDto;

    }

    //public async Task<IEnumerable<string>> GetListMockTestKeyCodeNeedToAutoSubmit()
    //{
    //    return await _iIGLmsdbContext.Database.GetDbConnection().QueryAsync<string>("[dbo].[web_user_get_all_key_code_need_to_submit]",
    //        commandType: CommandType.StoredProcedure);
    //}

    //public async Task<IEnumerable<string>> GetListMockTestKeyCodeNeedToAutoDelete()
    //{
    //    return await _iIGLmsdbContext.Database.GetDbConnection().QueryAsync<string>("[dbo].[web_user_get_all_key_code_need_to_delete]",
    //        commandType: CommandType.StoredProcedure);
    //}

    //public async Task<int> CountMockTestKeyCodeExamining()
    //{
    //    return await _iIGLmsdbContext.MocktestKeyCodes.Where(
    //        x =>
    //            x.SubmittedDate == null &&
    //            x.StartedDoingExamDate != null &&
    //            x.EndDate >= DateTime.UtcNow &&
    //            x.IsDelete == null &&
    //            x.IsAutoGenerate == true
    //            ).CountAsync();
    //}

    //private DataTable GenerateDataTableMockTestSectionCorrectAnswerType(IEnumerable<SectionCorrectAnswerBasic> sectionCorrectAnswerBasics)
    //{
    //    var output = new DataTable();
    //    output.Columns.Add("mocktest_section_id", typeof(Guid));
    //    output.Columns.Add("number_correct_answer", typeof(int));
    //    foreach (var section in sectionCorrectAnswerBasics)
    //    {
    //        output.Rows.Add(section.MockTestSectionId, section.NumberOfCorrectAnswer);
    //    }

    //    return output;
    //}

    //public async Task<IEnumerable<string>> GetListMockTestKeyCodeHaveNotTotalCorrectAnswer()
    //{
    //    var listKeyCode = await _iIGLmsdbContext.KeycodeResults.Where(f => !f.ComponentsDetails.Contains("NumberOfCorrectAnswer")).Select(f => f.Keycode).ToListAsync();
    //    listKeyCode = listKeyCode.Distinct().ToList();
    //    var listKeyCodeNotIncludeTF = await _iIGLmsdbContext.MocktestKeyCodes.Where(f => listKeyCode.Contains(f.Code) && f.IsAutoGenerate == null).Select(f => f.Code).ToListAsync();

    //    return listKeyCodeNotIncludeTF;

    //}

    //public async Task<IEnumerable<MgKeyCodeAnswerModel>> GetMockTestKeyCodeChoose(string keyCode)
    //{
    //    var listKeycodeChooses = await _iIGLmsdbContext.KeycodeChooses.Where(f => f.Keycode == keyCode).ToListAsync();
    //    if (listKeycodeChooses.Any())
    //    {
    //        var listQuestionId = listKeycodeChooses.Select(f => f.QuestionId).Distinct().ToList();
    //        var listMocktestpartId = listKeycodeChooses.Select(f => f.MocktestPartId).Distinct().ToList();
    //        var listQuestion = await _iIGLmsdbContext.Questions.Where(f => listQuestionId.Contains(f.Id)).Select(f => new { questionId = f.Id, QuestionnaireId = f.QuestionnaireId }).ToListAsync();
    //        var listQuestionnareId = listQuestion.Select(f => f.QuestionnaireId).Distinct().ToList();
    //        var listMockTestpart = await _iIGLmsdbContext.MocktestParts.Where(f => listMocktestpartId.Contains(f.Id)).Select(f => new { mocktestPartId = f.Id, MocktestSectionId = f.MocktestSectionId }).ToListAsync();
    //        var listQuestionnare = await _iIGLmsdbContext.Questionnaires.Where(f => listQuestionnareId.Contains(f.Id)).Select(f => new { f.Id, f.Type }).ToListAsync();
    //        return listKeycodeChooses.Select(f =>
    //        {
    //            var questionnareIdTmp = listQuestion.FirstOrDefault(x => x.questionId == f.QuestionId)?.QuestionnaireId ?? Guid.Empty;
    //            return new MgKeyCodeAnswerModel
    //            {
    //                KeyCode = f.Keycode,
    //                AnswerId = f.AnswerId,
    //                AnswerText = f.AnswerText,
    //                MatchingQuestionId = f.MatchingQuestionId,
    //                MockTestPartId = f.MocktestPartId,
    //                MockTestSectionId = listMockTestpart.FirstOrDefault(x => x.mocktestPartId == f.MocktestPartId)?.MocktestSectionId ?? Guid.Empty,
    //                QuestionId = f.QuestionId,
    //                QuestionnaireId = questionnareIdTmp,
    //                QuestionnaireType = (EQuestionnaireType)(listQuestionnare.FirstOrDefault(x => x.Id == questionnareIdTmp)?.Type ?? 0),
    //            };
    //        });
    //    }
    //    return new List<MgKeyCodeAnswerModel>();

    //}
}