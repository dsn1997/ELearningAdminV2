using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Web.BL.Services.Interfaces;
using IIG.Web.BL.Services.Interfaces.MockTests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using static IIG.Core.Common.ConfigureModels.Constants;
using IIG.Application.Models;
using IIG.Application.Services;
using IIG.Application.Models.Keycodes;
using IIG.Application.Models.Questionnaires;

namespace IIG.Web.Api.Controllers;

public class MockTestController : BaseController
{
    private readonly IMockTestService _mockTestBiz;

    public MockTestController(IMockTestService mockTestBiz
        )
    {
        _mockTestBiz = mockTestBiz;
    }

    [HttpPost("verify-key-code")]
    //[DisableRateLimiting]
    public async Task<VerifyKeyCodeDto> VerifyKeyCodeAsync([FromBody] VerifyKeyCodeRequest request)
    {
        return await _mockTestBiz.VerifyKeyCodeAsync(request);
    }

    //[HttpGet("view-result/{keyCode}")]
    ////[DisableRateLimiting]
    //public async Task<MockTestResultDto> ViewMockTestResultAsync(string keyCode)
    //{
    //    return await _mockTestBiz.ViewMockTestResultAsync(keyCode);
    //}

    [HttpGet("get-mocktest-structure/{keyCode}")]
    //[DisableRateLimiting]
    public async Task<MockTestStructureModel> GetMocktestStructureAsync(string keyCode)
    {
        return await _mockTestBiz.GetMocktestStructureAsync(keyCode);
    }

    [HttpPost("started-doing-answer")]
    //[DisableRateLimiting]
    public async Task<StartedDoingAnswerResponse> StartDoingAnswerAsync(StartedDoingAnswerRequest request)
    {
        return await _mockTestBiz.StartDoingAnswerAsync(request);
    }

    //[HttpPost("submit-mocktest/{keyCode}")]
    ////[DisableRateLimiting]
    //public async Task SubmitMockTestAsync(string keyCode)
    //{
    //    await _keyCodeAnswerService.SubmitMockTestAsync(keyCode);
    //}

    //[HttpGet("submit/response-check/keycode/{keyCode}")]
    //public async Task<List<ResponseCheckMocktestModel>> ResponseCheckSubmitMocktest(string keyCode)
    //{
    //    return await _keyCodeAnswerService.GetResponseCheckMocktest(keyCode);
    //}


    //[HttpPost("submit-mocktest-for-old-data")]
    //[AllowAnonymous]
    //public async Task SubmitMockTestForOldDataAsync()
    //{
    //    await _keyCodeAnswerService.SubmitMockTestIternaForOldDatalAsync();
    //}

    [HttpPost("mark-question")]
    //[DisableRateLimiting]
    public async Task MarkQuestionsTask(MarkQuestionRequest request)
    {
        await _mockTestBiz.MarkQuestionsTask(request);
    }

    [HttpPost("save-answer")]
    //[DisableRateLimiting]
    public async Task SaveAnswerAsync([FromBody] SaveAnswerRequest request)
    {
        await _mockTestBiz.SaveAnswerAsync(request);
    }

    [HttpGet("mocktest-menu")]
    //[DisableRateLimiting]
    public async Task<MgMockTestMenuModel> GetMockTestMenuAsync([FromQuery] MockTestKeyCodeBaseRequest request)
    {
        return await _mockTestBiz.GetMockTestMenuAsync(request);
    }

    [HttpGet("get-answer")]
    //[DisableRateLimiting]
    public async Task<IEnumerable<AnswerResponse>> GetAnswerAsync([FromQuery] GetAnswerRequest request)
    {
        return await _mockTestBiz.GetAnswerAsync(request);
    }

    [HttpGet("get-questionnaire-detail")]
    //[DisableRateLimiting]
    public async Task<QuestionnaireDto> GetQuestionnaireDetailAsync([FromQuery] GetQuestionnaireDetailRequest request)
    {
        return await _mockTestBiz.GetQuestionnaireDetailAsync(request);
    }

    //[HttpGet("streaming-play/{keycode}/{fileId}")]
    ////[DisableRateLimiting]
    //public async Task<FileStreamResult> StreamingPlayAsync([Required] string keyCode, [Required] Guid fileId)
    //{
    //    return await _mockTestBiz.StreamingPlayAsync(keyCode, fileId);
    //}

    [HttpGet("get-mocktest-part-detail")]
    //[DisableRateLimiting]
    public async Task<MgMockTestPartModel> GetMockTestPartDetailAsync([FromQuery] GetMockTestPartDetailRequest request)
    {
        return await _mockTestBiz.GetMockTestPartDetailAsync(request);
    }

    //[Authorize]
    //[HttpPost("auto-submit-mocktest-keycode")]
    //public async Task AutoSubmitMockTestAsync()
    //{
    //    await _mockTestBiz.AutoSubmitMockTestKeyCodeAsync();
    //}

    //[Authorize]
    //[HttpPost("auto-delete-mocktest-keycode")]
    //public async Task AutoDeleteMockTestKeyCodeAsync()
    //{
    //    await _mockTestBiz.AutoDeleteMockTestKeyCodeAsync();
    //}

    //[HttpGet("check-overload-mocktest-keycode")]
    //public Task<OverloadCodeTest> CheckOverloadMockTestKeyCodeAsync()
    //{
    //    return _mockTestBiz.CheckOverloadMockTestKeyCodeAsync();
    //}

    //[HttpGet("result/structure/{keyCode}")]
    //public async Task<MgMockTestKeyCodeModel> GetMocktestResultStructure(string keyCode)
    //{
    //    return await _mockTestBiz.GetMocktestResultStructure(keyCode);
    //}
}