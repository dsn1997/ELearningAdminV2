using AutoMapper;
using IIG.Application.Models.KeycodeChooses;
using IIG.Application.Models.KeyCodes;
using IIG.Core.Base;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.Enums;
using IIG.Core.Entities;
using IIG.Core.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using static IIG.Core.Common.ConfigureModels.Constants;

namespace IIG.Application.Data;

public class KeyCodeAnswerDA : IKeyCodeAnswerDA
{
    private readonly IAppFactory _appFactory;
    private readonly IMapper _mapper;
    private readonly IMockTestKeyCodeDA _iMockTestKeyCodeDA;
    //private readonly IKeyCodeResultDA _keyCodeResultDA;
    private readonly IConfiguration _configuration;
    private IRepository<KeycodeChoose> _keyCodeChooseRepos;
    private IRepository<KeycodeResult> _keyCodeResultRepos;
    private IRepository<MocktestKeyCode> _mockTestKeyCodeRepos;
    public KeyCodeAnswerDA(IAppFactory appFactory,
                           IMapper mapper,
                           IConfiguration configuration,
                           IMockTestKeyCodeDA iMockTestKeyCodeDA,
                           IRepository<KeycodeChoose> keyCodeChooseRepos,
                           IRepository<KeycodeResult> keyCodeResultRepos,
                           IRepository<MocktestKeyCode> mockTestKeyCodeRepos
        //IKeyCodeResultDA keyCodeResultDA
        )
    {
        _appFactory = appFactory;
        _mapper = mapper;
        _iMockTestKeyCodeDA = iMockTestKeyCodeDA;
        //_keyCodeResultDA = keyCodeResultDA;
        _keyCodeChooseRepos = keyCodeChooseRepos;
        _keyCodeResultRepos = keyCodeResultRepos;
        _mockTestKeyCodeRepos = mockTestKeyCodeRepos;
        _configuration = configuration;
    }

    public async Task BatchInsertKeyCodeChose(IEnumerable<KeyCodeChooseResponse> listKeyCodeChoose)
    {
        var entities = listKeyCodeChoose.Select(k => new KeycodeChoose
        {
            Keycode = k.KeyCode,
            MocktestPartId = k.MockTestPartId,
            AnswerId = k.AnswerId,
            QuestionId = k.QuestionId,
            AnswerText = k.AnswerText,
            CorrectAnswer = k.CorrectAnswer,
            MatchingQuestionId = k.MatchingQuestionId
        }).ToList();

        await _keyCodeChooseRepos.InsertRangeAsync(entities);
    }

    public async Task InsertKeyCodeResult(KeyCodeResultDto KeyCodeResultDto)
    {
        var entity = new KeycodeResult
        {
            Keycode = KeyCodeResultDto.KeyCode,
            TotalCorrectAnswer = KeyCodeResultDto.TotalCorrectAnswer,
            TotalQuestion = KeyCodeResultDto.TotalQuestion,
            RankingScore = KeyCodeResultDto.RankingScore,
            ComponentsDetails = KeyCodeResultDto.ComponentsDetails,
        };
        await _keyCodeResultRepos.InsertAsync(entity);
    }
    public async Task UpdateKeyCodeResult(KeyCodeResultDto keyCodeResultDto)
    {
        var itemKeyCodeResult = await _keyCodeResultRepos.GetAll().Where(f => f.Keycode == keyCodeResultDto.KeyCode).FirstOrDefaultAsync();
        if (itemKeyCodeResult != null)
        {
            itemKeyCodeResult.ComponentsDetails = keyCodeResultDto.ComponentsDetails;
            await _keyCodeResultRepos.UpdateAsync(itemKeyCodeResult);
        }
    }

    public async Task UpdateSubmittedDateKeyCode(string keyCode)
    {
         await _mockTestKeyCodeRepos.ExecuteUpdateAsync(p=>p.Code == keyCode, set=>set.SetProperty(p=>p.SubmittedDate,DateTime.Now));
    }

    public async Task DeleteManyAsync(List<string> keyCodes)
    {
        await _keyCodeResultRepos.DeleteAsync(p => keyCodes.Contains(p.Keycode));
        await _keyCodeChooseRepos.DeleteAsync(p => keyCodes.Contains(p.Keycode));
        await _mockTestKeyCodeRepos.DeleteAsync(p=> keyCodes.Contains(p.Code));

    }

    //public async Task<MailRequest> SendResultExamEmail(string code)
    //{
    //    var keyCode = await _iIGLmsdbContext.MocktestKeyCodes.Where(x => x.Code == code).FirstOrDefaultAsync();
    //    if (keyCode == null)
    //        return null;

    //    if (keyCode.IsAutoGenerate != true || keyCode.SubmittedDate == null)
    //    {
    //        return null;
    //    }

    //    var user = await _iIGLmsdbContext.ToeflChallengeUsers.FirstOrDefaultAsync(x => x.Id == keyCode.WebUserId);
    //    if (user == null)
    //        return null;

    //    // var contestName = await _iIGLmsdbContext.ToeflChallengeContests.Where(x => x.Id == user.ContestId)
    //    //     .Select(x => x.Name).FirstOrDefaultAsync();
    //    var tmpKeyCodeResultDto = await _keyCodeResultDA.GetDetailFromKeyCode(code);
    //    var keyCodeResult = _mapper.Map<KeyCodeResultModel>(tmpKeyCodeResultDto);

    //    var mockTest = await _iIGLmsdbContext.Mocktests.FirstOrDefaultAsync(x => x.Id == keyCode.MocktestId);
    //    if (mockTest == null)
    //        return null;
    //    var mockTestType = await _iIGLmsdbContext.MocktestTypes.Where(x => x.Id == mockTest.MocktestTypeId)
    //        .Select(m => m.Name).FirstOrDefaultAsync();
    //    if (mockTestType != Constants.MockTestType.ToeflJuniorChallenge &&
    //        mockTestType != Constants.MockTestType.ToeflPrimaryChallenge)
    //        return null;

    //    IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto =
    //        await _iMockTestKeyCodeDA.GetMockTestSectionResult(keyCodeResult.Sections, keyCode.SubmittedDate,
    //            keyCode.MocktestPublishedAt);
    //    var totalMaxScore = listMockTestSectionResultDto.Sum(x => x.MaxScore);
    //    var listSort = listMockTestSectionResultDto.OrderBy(x => x.SortOrder);
    //    var totalScore = GenerateScoreResult(EMockTestScoreType.CorrectScore, listSort);
    //    // var resultKeycode = await _iIGLmsdbContext.KeycodeResults.Where(k => k.Keycode == code).Select(k => k.ComponentsDetails).FirstOrDefaultAsync();
    //    // var listResultKeycode = JsonConvert.DeserializeObject<List<SectionScoreBasicDto>>(resultKeycode);
    //    // var data = new StringBuilder("<tr>");
    //    // var header = new StringBuilder("<tr>");
    //    //
    //    // listSort.ForEach(x =>
    //    // {
    //    //     AppendCell(data, "Số câu đúng");
    //    //     AppendCell(data, "Điểm");
    //    //     AppendHeader(header, x.SectionName, colspan: 2);
    //    // });
    //    // AppendHeader(header, "Tổng điểm", 1, 2);
    //    // header.Append("</tr>");
    //    // data.Append("</tr><tr>");
    //    //
    //    // listSort.ForEach(x =>
    //    // {
    //    //     AppendCell(data, GetNumberCorrect(listResultKeycode, x.SectionId));
    //    //     AppendCell(data, x.Score.ToString());
    //    // });
    //    // AppendCell(data, totalScore);
    //    // data.Append("</tr>");
    //    // var htmlTable = $"<table>{header}{data}</table>";
    //    var body = mockTestType switch
    //    {
    //        Constants.MockTestType.ToeflJuniorChallenge => EmailHelper.GenerateBody(
    //            int.Parse(totalScore) < Constants.ScoreToeflChallengerToSendEmail.PassingScore
    //                ? Constants.EmailTemplate.ResultExamToeflChallengeJuniorUnder50
    //                : Constants.EmailTemplate.ResultExamToeflChallengeJuniorOver50),
    //        Constants.MockTestType.ToeflPrimaryChallenge => EmailHelper.GenerateBody(
    //            int.Parse(totalScore) < Constants.ScoreToeflChallengerToSendEmail.PassingScore
    //                ? Constants.EmailTemplate.ResultExamToeflChallengePrimaryUnder50
    //                : Constants.EmailTemplate.ResultExamToeflChallengePrimaryOver50),
    //        _ => ""
    //    };

    //    var bodyEmail = body
    //        .Replace("{{StudentName}}", user.Name)
    //        .Replace("{{StartDate}}", _configuration["AppSettings:StartDateToeflChallenge"])
    //        .Replace("{{ExpireDate}}", _configuration["AppSettings:ExpireDateToeflChallenge"])
    //        .Replace("{{TotalScore}}",  $"{totalScore}/{totalMaxScore}");

    //    return new MailRequest
    //    {
    //        ToAddress = user.ParentEmail,
    //        Body = bodyEmail,
    //        Subject = mockTestType switch
    //        {
    //            Constants.MockTestType.ToeflJuniorChallenge => string.Format(
    //                int.Parse(totalScore) < Constants.ScoreToeflChallengerToSendEmail.PassingScore
    //                    ? Constants.EmailSubjects.EmailSubjectsResultExamToeflChallengeJuniorUnder50
    //                    : Constants.EmailSubjects.EmailSubjectsResultExamToeflChallengeJuniorOver50, 
    //                user.Name),
    //            Constants.MockTestType.ToeflPrimaryChallenge => string.Format(
    //                int.Parse(totalScore) < Constants.ScoreToeflChallengerToSendEmail.PassingScore
    //                    ? Constants.EmailSubjects.EmailSubjectsResultExamToeflChallengePrimaryUnder50
    //                    : Constants.EmailSubjects.EmailSubjectsResultExamToeflChallengePrimaryOver50, 
    //                user.Name),
    //            _ => ""
    //        }
    //    };
    //}

}