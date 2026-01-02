using AutoMapper;
using IIG.Application.Models;
using IIG.Core.Base;
using IIG.Core.Entities;
using IIG.Core.Providers.Interfaces;
using IIG.Web.Data.Models;
using IIG.Web.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static IIG.Core.Common.ConfigureModels.Constants;


namespace IIG.Application.Services.Redis;

public partial class MockTestWrapperRedisDataService : IMockTestWrapperRedisDataService
{
    private readonly ILogger<MockTestWrapperRedisDataService> _logger;
    private readonly IAppFactory _appFactory;
    private readonly IMockTestWrapperDA _dataDA;
    private readonly IMockTestRedisDataService _mockTestDataService;
    private readonly IMapper _mapper;

    private readonly IRedisGenericFactory _redisGenericFactory;


    public MockTestWrapperRedisDataService(
        ILogger<MockTestWrapperRedisDataService> logger,
        IAppFactory appFactory,
        IMockTestWrapperDA dataDA,
        IMockTestRedisDataService mockTestDataService,
        IQuestionaireRedisDataService questionnaireDataService,
        IMapper mapper,
        IRedisGenericFactory redisGenericFactory
        )
    {
        _logger = logger;
        _appFactory = appFactory;
        _dataDA = dataDA;
        _mockTestDataService = mockTestDataService;
        _mapper = mapper;
        _redisGenericFactory = redisGenericFactory;
    }


    public async Task<IEnumerable<Menu_MockTestWrapperDto>> GetListByGroupId(Guid MockTestWrapperGroupId, string languageCode)
    {
        try
        {
            var redisService = _redisGenericFactory.CreateCollection<Menu_MockTestWrapperDto>(RedisKey.MockTestWrapper.ByGroupId);
            var dataList = await redisService.GetOrCreateWithDistributedLockAsync(
                $"{MockTestWrapperGroupId}",
                () => _dataDA.GetListByGroup(MockTestWrapperGroupId, languageCode),
                3,
                0,
                TimeSpan.FromSeconds(10),
                (int)TimeSpan.FromMinutes(5).TotalSeconds
            );
            return dataList.ToList();
        }
        catch
        {
            throw;
        }

    }

   
    public async Task<MockTestWrapperDetailDto> GetDetailById(Guid wrapperId, string languageCode)
    {
        try
        {
            var redisService = _redisGenericFactory.Create<MockTestWrapperDetailDto>();
            var data = await redisService.GetOrCreateWithDistributedLockAsync(
                $"{wrapperId}",
                () => GetDetailByIdJob(wrapperId, languageCode),
                3,
                0,
                TimeSpan.FromSeconds(10),
                (int)TimeSpan.FromMinutes(5).TotalSeconds
            );
            return data;
        }
        catch
        {
            throw;
        }
    }

    private async Task<MockTestWrapperDetailDto> GetDetailByIdJob(Guid wrapperId, string languageCode)
    {
        var wrapperData = await _dataDA.GetDetailById(wrapperId, languageCode);
        var mockTestData = await _mockTestDataService.GetMockTest(wrapperData.MockTestId);
        var structure = _mapper.Map<MockTestStructureModel>(mockTestData);
        wrapperData.MockTestStructure = _mapper.Map<MockTestStructureWithQuestionTagModel>(structure);
        wrapperData.Tags = mockTestData.GeneralInfo?.Tags;
        var listPartId = wrapperData.MockTestStructure.MockTestSections.SelectMany(s => s.Parts.Select(p => p.Id)).ToList();
        var questionTagQuery = (from question in _appFactory.Repository<Question>().GetAll()
                                join questionnaire in _appFactory.Repository<Questionnaire>().GetAll() on question.QuestionnaireId equals questionnaire.Id
                                join partQuestionnaire in _appFactory.Repository<MocktestPartQuestionnaire>().GetAll() on questionnaire.Id equals partQuestionnaire.QuestionnaireId
                                where listPartId.Contains(partQuestionnaire.MocktestPartId) && question.QuestionTags != null
                                group question by partQuestionnaire.MocktestPartId into g
                                select new
                                {
                                    partId = g.Key,
                                    tagIds = g.SelectMany(p => p.QuestionTags.Where(p => !p.Deleted.HasValue).Select(p => p.TagId)),
                                    tagNames = new List<string>()
                                }).AsNoTracking();
        var listPartTagId = await questionTagQuery.ToListAsync();
        var listTagIds = listPartTagId.SelectMany(p => p.tagIds).ToList();
        var listTags = await _appFactory.Repository<Tag>().GetAll().Where(p => listTagIds.Contains(p.Id)).Select(p => new { p.Id, p.Name }).ToListAsync();

        wrapperData.MockTestStructure.MockTestSections = wrapperData.MockTestStructure.MockTestSections.Select(p=>
        {
            p.Parts = p.Parts.Select(part=>
            {
                var partTagIds = listPartTagId.FirstOrDefault(q => q.partId == part.Id)?.tagIds.Distinct().ToList() ?? new List<Guid>();
                part.QuestionTags = listTags.Where(q => partTagIds.Contains(q.Id)).Select(q => q.Name).ToList();
                return part;
            }).ToList();
            return p;
        }).ToList();

        return wrapperData;
    }
}
