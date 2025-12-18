

using IIG.Application.Models;
using IIG.Application.Models.KeyCodes;

namespace IIG.Application.Services;

public interface IKeyCodeAnswerService
{
    Task SubmitMockTestAsync(string keyCode);
   
    Task SubmitMockTestIternalAsync(string keyCode);
   
    //Task SubmitMultipleMockTestAsync(List<string> list);
    
    //Task DeleteMultipleMockTestAsync(List<string> list);

    //Task SubmitMockTestIternaForOldDatalAsync();
    //Task<MockTestKeyCodeBasicDto> ValidateBeforeSubmitMockTestAsync(string keyCode, bool isSubmitted =false);
    //Task<List<ResponseCheckMocktestModel>> GetResponseCheckMocktest(string keyCode, bool isSubmitted = false);
    //Task SendResultExamEmail(string code);
    //Task DeleteDataAsync(string keyCode);
}