
using IIG.Application.Models.KeyCodes;
using IIG.Core.Base;
using IIG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IIG.Application.Data;

public class KeyCodeResultDA : IKeyCodeResultDA
{
    private readonly IAppFactory _appFactory;

    public KeyCodeResultDA(IAppFactory appFactory)
    {
        _appFactory = appFactory;
    }

    public async Task<KeyCodeResultDto> GetDetailFromKeyCode(string keyCode)
    {
        var query = from kc in _appFactory.Repository<KeycodeResult>().GetAll()
                    where kc.Keycode == keyCode
                    select new KeyCodeResultDto
                    {
                        KeyCode = kc.Keycode,
                        TotalCorrectAnswer = kc.TotalCorrectAnswer,
                        TotalQuestion = kc.TotalQuestion,
                        RankingScore = kc.RankingScore,
                        ComponentsDetails = kc.ComponentsDetails,
                    };
        var result = await query.FirstOrDefaultAsync();
        return result;
    }
}