//using Dapper;
//using IIG.Application.Models;
//using IIG.Application.Models.Keycodes;
//using IIG.Application.Models.MockTest;
//using Microsoft.EntityFrameworkCore;
//using System.Data;

//namespace IIG.Application.Data;

//public class MockTestDA : IMockTestDA
//{
//    private readonly IIGLmsdbContext _iIGLmsdbContext;

//    public MockTestDA(IIGLmsdbContext iIGLmsdbContext)
//    {
//        _iIGLmsdbContext = iIGLmsdbContext;
//    }

//    public async Task<VerifyKeyCodeDto> VerifyKeyCodeAsync(string keyCode, string languageCode)
//    {
//        DynamicParameters param = new DynamicParameters()
//            .AddParam("@key_code", keyCode)
//            .AddParam("@language_code", languageCode);

//        var x = await _iIGLmsdbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<VerifyKeyCodeDto>("web_get_mocktest_info_by_key_code", param, commandType: CommandType.StoredProcedure);
//        return x;
//    }

//    public async Task<bool> CheckKeyCodeAlreadyExistedAsync(string keyCode)
//    {
//        DynamicParameters param = new DynamicParameters()
//            .AddParam("@key_code", keyCode);

//        return await _iIGLmsdbContext.Database.GetDbConnection().ExecuteScalarAsync<bool>("web_mocktest_check_key_code_existed", param, commandType: CommandType.StoredProcedure);
//    }

//    public async Task<bool> WebVerifyKeycodeObjectType(VerifyKeyCodeRequest request)
//    {
//        DynamicParameters param = new DynamicParameters()
//            .AddParam("@key_code", request.Keycode)
//            .AddParam("@mocktest_object_id", request.MocktestObjectId)
//            .AddParam("@mocktest_type_id", request.MocktestTypeId);

//        return await _iIGLmsdbContext.Database.GetDbConnection().ExecuteScalarAsync<bool>("web_verify_keycode_object_type", param, commandType: CommandType.StoredProcedure);
//    }

//    public async Task<MockTestDetailModel> GetDetailByIdAsync(Guid mockTestId, DateTime? publishedAt = null)
//    {
//        DynamicParameters param = new DynamicParameters()
//            .AddParam("@id", mockTestId)
//            .AddParam("@published_at", publishedAt);

//        return await _iIGLmsdbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<MockTestDetailModel>("mocktest_get_by_id", param,
//            commandType: CommandType.StoredProcedure);
//    }

//    public async Task<MockTestPublicInfoModel> GetPublicInfoAsync(Guid mockTestId)
//    {
//        DynamicParameters param = new DynamicParameters()
//          .AddParam("@id", mockTestId);

//        return await _iIGLmsdbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<MockTestPublicInfoModel>("mocktest_get_public_info_by_id", param,
//            commandType: CommandType.StoredProcedure);
//    }

//    public async Task StartedDoingAnswerAsync(StartedDoingAnswerModel model)
//    {
//        DynamicParameters param = new DynamicParameters()
//            .AddParam("@key_code", model.KeyCode)
//            .AddParam("@started_doing_exam_date", model.StartedDoingExamDate)
//            .AddParam("@client_ip", model.ClientIp)
//            .AddParam("@browser", model.Browser)
//            .AddParam("@time_remaining", model.TimeRemaining > 0 ? model.TimeRemaining : 0)
//            .AddParam("@cookie", model.Cookie);

//        await _iIGLmsdbContext.Database.GetDbConnection().ExecuteAsync("web_mocktest_key_code_start_doing_answer", param, commandType: CommandType.StoredProcedure);
//    }

//    public async Task UpdateRegistrationTokenKeyCode(string keyCode, string registrationToken)
//    {
//        var mockTestKeyCodes = await _iIGLmsdbContext.MocktestKeyCodes.Where(m => m.Code == keyCode).FirstOrDefaultAsync();
//        if (mockTestKeyCodes != null)
//        {
//            mockTestKeyCodes.RegistrationToken = registrationToken;
//            await _iIGLmsdbContext.SaveChangesAsync();
//        }

//    }
//}