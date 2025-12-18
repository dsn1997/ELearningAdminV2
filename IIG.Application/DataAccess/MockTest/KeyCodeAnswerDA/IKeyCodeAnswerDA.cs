

using IIG.Application.Models.KeycodeChooses;
using IIG.Application.Models.KeyCodes;

namespace IIG.Application.Data;

public interface IKeyCodeAnswerDA 
{
    Task BatchInsertKeyCodeChose(IEnumerable<KeyCodeChooseResponse> listKeyCodeChoose);

    Task InsertKeyCodeResult(KeyCodeResultDto KeyCodeResultDto);

    Task UpdateKeyCodeResult(KeyCodeResultDto keyCodeResultDto);


    Task UpdateSubmittedDateKeyCode(string keyCode);
    
    Task DeleteManyAsync(List<string> keyCodes);

    //Task<MailRequest> SendResultExamEmail(string keyCode);
}