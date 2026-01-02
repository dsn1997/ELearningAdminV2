using IIG.Application.Models;
using IIG.Application.Models.Keycodes;
using IIG.Application.Models.KeyCodes;
using IIG.Application.Models.Questionnaires;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Web.Data.Models;

using Microsoft.AspNetCore.Mvc;

namespace IIG.Application.Services;

public interface IMockTestWrapperKeyCodeService
{
    Task<VerifyKeyCodeDto> VerifyMockTestAsync(VerifyMockTestWrapperDto request);
    Task<StartedDoingAnswerMockTestWrapperResponse> StartDoingAnswerAsync(StartedDoingAnswerMockTestWrapperRequest request);

    Task<MockTestStructureModel> GetMocktestStructureAsync(Guid mockTestWrapperId);
    Task<MgMockTestMenuModel> GetMockTestMenuAsync(MockTestKeyCodeBaseRequest request);
    Task<MgMockTestPartModel> GetMockTestPartDetailAsync(GetMockTestPartDetailRequest request);
    Task<QuestionnaireDto> GetQuestionnaireDetailAsync(GetQuestionnaireDetailRequest request);
    Task<IEnumerable<AnswerResponse>> GetAnswerAsync(GetAnswerRequest request);
    Task<MockTestResultDto> ViewMockTestResultAsync(string keyCode);
    //Task<MockTestViewResultDetailModel> ViewMockTestResultDetailAsync(Guid mockTestId, DateTime? submitDate);
    Task MarkQuestionsTask(MarkQuestionRequest request);
    Task<MgMockTestMenuModel> SaveAnswerAsync(SaveAnswerRequest request);
    Task SubmitMockTestAsync(Guid mockTestWrapperId);
    //Task AutoDeleteUnSubmittedKeyCode();
    Task<FileStreamResult> StreamingPlayAsync(string keyCode, Guid fileId);
}