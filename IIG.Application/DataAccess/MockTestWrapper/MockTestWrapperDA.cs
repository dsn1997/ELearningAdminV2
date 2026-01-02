using IIG.Application.Models.MockTest;
using IIG.Core.Base;
using IIG.Core.Common.ErrorHandling;
using IIG.Core.Helper;
using IIG.Core.ModelDbContext;
using IIG.Core.Repository;
using IIG.Web.Data.Models;
using IIG.Web.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.Versioning;

namespace IIG.Application.Data;

public class MockTestWrapperDA : IMockTestWrapperDA
{
    private readonly IAppFactory _appFactory;
    private readonly IRepository<MockTestWrapper> _mockTestWrapperRepos;
    private readonly IRepository<MockTestWrapperTranslation> _mockTestWrapperTranslationRepos;

    public MockTestWrapperDA(IAppFactory appFactory,
        IRepository<MockTestWrapper> mockTestWrapperRepos,
        IRepository<MockTestWrapperTranslation> mockTestWrapperTranslationRepos)
    {
        _appFactory = appFactory;
        _mockTestWrapperRepos = mockTestWrapperRepos;
        _mockTestWrapperTranslationRepos = mockTestWrapperTranslationRepos;
    }

    public async Task<IEnumerable<Menu_MockTestWrapperDto>> GetListByGroup(Guid GroupId,string languageCode)
    {
        var query = from data in _mockTestWrapperRepos.GetAll()
                    join r_trans in _mockTestWrapperTranslationRepos.GetAll().Where(p=>p.LanguageCode == languageCode) on data.Id equals r_trans.MockTestWrapperId into tb_trans
                    from trans in tb_trans.DefaultIfEmpty()
                    where !data.Deleted.HasValue && data.MockTestWrapperGroupChooses.Any(x => x.MocktestWrapperGroupId == GroupId)
                    select new Menu_MockTestWrapperDto
                    {
                        Id = data.Id,
                        Name = trans != null? trans.Name : "",
                        IsDailyChallenge = data.IsDailyChallenge,
                        DailyChallengeEndDate = data.DailyChallengeEndDate,
                        ImageFileUrl = data.Image != null ? data.Image.FileName : null,
                        MockTest = new Menu_MockTestDetailDto
                        {
                            Id = data.MockTest.Id,
                            Questions = data.MockTest.Questions,
                            Time = data.MockTest.Time,
                            NumberTests = data.MockTest.NumberTests,
                            Sections = data.MockTest.MocktestSections.Select(p=>p.Name),
                        },
                    };
        var dataList = await query.ToListAsync();
        return dataList;
    }


    
    public async Task<MockTestWrapperDetailDto> GetDetailById(Guid wrapperId, string languageCode)
    {
        var query = from data in _mockTestWrapperRepos.GetAll()
                    join r_trans in _mockTestWrapperTranslationRepos.GetAll().Where(p => p.LanguageCode == languageCode) on data.Id equals r_trans.MockTestWrapperId into tb_trans
                    from trans in tb_trans.DefaultIfEmpty()
                    where !data.Deleted.HasValue && data.Id == wrapperId
                    select new MockTestWrapperDetailDto
                    {
                        Id = data.Id,
                        Instructions = trans.Instructions,
                        Descriptions = trans.Description,
                        MockTestId = data.MockTestId,
                    };
        var result = await query.FirstOrDefaultAsync();
        if (result == null)
            throw new ApiNotFoundException($"Not found Id: {wrapperId}");

        return result;
    }

}