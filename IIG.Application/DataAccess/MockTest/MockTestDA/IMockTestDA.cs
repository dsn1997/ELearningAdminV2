using IIG.Application.Models;
using IIG.Application.Models.Keycodes;
using IIG.Application.Models.MockTest;

namespace IIG.Application.Data;

public interface IMockTestDA 
{
    Task<VerifyKeyCodeDto> VerifyKeyCodeAsync(string keyCode, string languageCode);
    Task<bool> CheckKeyCodeAlreadyExistedAsync(string keyCode);
    Task<bool> WebVerifyKeycodeObjectType(VerifyKeyCodeRequest request);
    Task<MockTestDetailModel> GetDetailByIdAsync(Guid mockTestId, DateTime? publishedAt = null);
    Task<MockTestPublicInfoModel> GetPublicInfoAsync(Guid mockTestId);
    Task StartedDoingAnswerAsync(StartedDoingAnswerModel model);
    //Task UpdateRegistrationTokenKeyCode(string keyCode, string registrationToken);
}