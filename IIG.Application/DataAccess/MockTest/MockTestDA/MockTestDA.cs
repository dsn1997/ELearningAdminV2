using IIG.Application.Models;
using IIG.Application.Models.Keycodes;
using IIG.Application.Models.MockTest;
using IIG.Core.Base;
using IIG.Core.Common.Enums;
using IIG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IIG.Application.Data;

public class MockTestDA : IMockTestDA
{
    private readonly IAppFactory _appFactory;

    public MockTestDA(IAppFactory appFactory)
    {
        _appFactory = appFactory;
    }

    public async Task<VerifyKeyCodeDto> VerifyKeyCodeAsync(string keyCode, string languageCode)
    {

        var mk = await (from p in _appFactory.Repository<MocktestKeyCode>().GetAll()
                        where p.Code == keyCode
                        select new
                        {
                            p.StartedDoingExamDate,
                            p.SubmittedDate,
                            p.Code,
                            p.MocktestId,
                            p.EndDate
                        }).FirstOrDefaultAsync();
        if (mk == null)
            return null;

        var result = new VerifyKeyCodeDto
        {
            MockTestId = mk.MocktestId,
            SubmittedDate = mk.SubmittedDate,
            StartedDoingExamDate = mk.StartedDoingExamDate,

        };

        // ===== CALCULATE STATUS  =====

        if (DateTime.UtcNow > mk.EndDate.Date.AddDays(1))
            result.Status = EMockTestKeyCodeStatus.Expired;
        else if (mk.StartedDoingExamDate == null)
            result.Status = EMockTestKeyCodeStatus.NotUsed;
        else if (mk.SubmittedDate == null)
            result.Status = EMockTestKeyCodeStatus.Using;
        else
            result.Status = EMockTestKeyCodeStatus.Used;
        return result;
    }

    public async Task<bool> CheckKeyCodeAlreadyExistedAsync(string keyCode)
    {
       var exist = await _appFactory.Repository<MocktestKeyCode>().GetAll()
            .AnyAsync(k => k.Code == keyCode);
        return exist;
    }

    public async Task<bool> WebVerifyKeycodeObjectType(VerifyKeyCodeRequest request)
    {
        var valid = await (from keyCode in _appFactory.Repository<MocktestKeyCode>().GetAll()
                           join mockTest in _appFactory.Repository<Mocktest>().GetAll() on keyCode.MocktestId equals mockTest.Id
                           where keyCode.Code == request.Keycode && mockTest.MocktestObjectId == request.MocktestObjectId && mockTest.MocktestTypeId == request.MocktestTypeId
                           select 1).AnyAsync();
        return valid;
    }

    public async Task<MockTestDetailModel> GetDetailByIdAsync(Guid mockTestId, DateTime? publishedAt = null)
    {
        var query = from mt in _appFactory.Repository<Mocktest>().GetAll()
                    where mt.Id == mockTestId
                    select new MockTestDetailModel
                    {
                        Id = mt.Id,
                        MockTestTypeId = mt.MocktestTypeId ?? Guid.Empty,
                        MockTestObjectId = mt.MocktestObjectId ?? Guid.Empty,
                        Status = (EMockTestStatus)mt.Status,
                        ScoreType = (EMockTestScoreType)mt.ScoreType,
                        Tags = mt.Tags,
                        Created = mt.Created ?? DateTime.MinValue,
                        Translations = mt.MocktestTranslations.Select(t => new MockTestTranslationDto
                        {
                            LanguageCode = t.LanguageCode,
                            Name = t.Name,
                        }).ToList()
                    };
        var result = await query.FirstOrDefaultAsync();
        return result;
    }

    public async Task<MockTestPublicInfoModel> GetPublicInfoAsync(Guid mockTestId)
    {
        var query = from mt in _appFactory.Repository<Mocktest>().GetAll()
                    where mt.Id == mockTestId
                    select new MockTestPublicInfoModel
                    {
                        Id = mt.Id,              
                        Status = (EMockTestStatus)mt.Status,
                      PublishedAt = mt.PublishedAt,
                    };
        var result = await query.FirstOrDefaultAsync();
        return result;
    }

    public async Task StartedDoingAnswerAsync(StartedDoingAnswerModel model)
    { 
        var entity = await _appFactory.Repository<MocktestKeyCode>().GetAll()
            .Where(k => k.Code == model.KeyCode)
            .FirstOrDefaultAsync();
        if(entity != null)
        {
            entity.StartedDoingExamDate = model.StartedDoingExamDate;
            entity.ClientIp = model.ClientIp;
            entity.Browser = model.Browser;
            entity.TimeRemaining = model.TimeRemaining;
            entity.Cookie = model.Cookie;
            entity.Modified = DateTime.UtcNow;
            _appFactory.Repository<MocktestKeyCode>().Update(entity);
        }    
        
    }

    //public async Task UpdateRegistrationTokenKeyCode(string keyCode, string registrationToken)
    //{
    //    var mockTestKeyCodes = await _iIGLmsdbContext.MocktestKeyCodes.Where(m => m.Code == keyCode).FirstOrDefaultAsync();
    //    if (mockTestKeyCodes != null)
    //    {
    //        mockTestKeyCodes.RegistrationToken = registrationToken;
    //        await _iIGLmsdbContext.SaveChangesAsync();
    //    }

    //}
}