using IIG.Application.Models;
using IIG.Application.Models.Keycodes;
using IIG.Application.Models.KeyCodes;
using IIG.Application.Models.Questionnaires;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Common.MongoDataModels.MockTests;

using Microsoft.AspNetCore.Mvc;

namespace IIG.Application.Services;

public interface IMockTestService
{
    Task<VerifyKeyCodeDto> VerifyKeyCodeAsync(VerifyKeyCodeRequest request);

    //Task<MockTestResultDto> ViewMockTestResultAsync(string keyCode);

    Task<MockTestStructureModel> GetMocktestStructureAsync(string keyCode);

    Task<StartedDoingAnswerResponse> StartDoingAnswerAsync(StartedDoingAnswerRequest request);

    Task MarkQuestionsTask(MarkQuestionRequest request);

    Task<MgMockTestMenuModel> SaveAnswerAsync([FromBody] SaveAnswerRequest request);

    Task<MgMockTestMenuModel> GetMockTestMenuAsync(MockTestKeyCodeBaseRequest request);

    Task<IEnumerable<AnswerResponse>> GetAnswerAsync(GetAnswerRequest request);

    Task<QuestionnaireDto> GetQuestionnaireDetailAsync(GetQuestionnaireDetailRequest request);

    //Task<FileStreamResult> StreamingPlayAsync(string keyCode, Guid fileId);

    Task<MgMockTestPartModel> GetMockTestPartDetailAsync([FromBody] GetMockTestPartDetailRequest request);

    //Task AutoSubmitMockTestKeyCodeAsync();

    //Task AutoDeleteMockTestKeyCodeAsync();

    //Task<OverloadCodeTest> CheckOverloadMockTestKeyCodeAsync();
    //Task<MgMockTestKeyCodeModel> GetMocktestResultStructure(string keyCode);
}