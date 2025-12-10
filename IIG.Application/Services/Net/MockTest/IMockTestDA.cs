using IIG.Web.Data.Models.MockTests;
using IIG.Web.Data.Models.MockTests.Keycodes;
using IIG.Web.Data.Models.MockTests.MockTest;

namespace IIG.Web.Data.Services.Interfaces.MockTests;

public interface IMockTestDA 
{
    Task<VerifyKeyCodeDto> VerifyKeyCodeAsync(string keyCode, string languageCode);
    Task<bool> CheckKeyCodeAlreadyExistedAsync(string keyCode);
    Task<bool> WebVerifyKeycodeObjectType(VerifyKeyCodeRequest request);
    Task<MockTestDetailModel> GetDetailByIdAsync(Guid mockTestId, DateTime? publishedAt = null);
    Task<MockTestPublicInfoModel> GetPublicInfoAsync(Guid mockTestId);
    Task StartedDoingAnswerAsync(StartedDoingAnswerModel model);
    Task UpdateRegistrationTokenKeyCode(string keyCode, string registrationToken);
}