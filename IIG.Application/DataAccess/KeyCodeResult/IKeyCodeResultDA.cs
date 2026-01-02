

using IIG.Application.Models.KeyCodes;

namespace IIG.Application.Data;

public interface IKeyCodeResultDA 
{
    Task<KeyCodeResultDto> GetDetailFromKeyCode(string keyCode);
}