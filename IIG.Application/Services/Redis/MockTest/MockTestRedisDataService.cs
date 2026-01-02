using IIG.Application.Services.Mongo;
using IIG.Core.Base;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Core.Entities;
using IIG.Core.Providers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace IIG.Application.Services.Redis;

public partial class MockTestRedisDataService : IMockTestRedisDataService
{
    private readonly ILogger<MockTestRedisDataService> _logger;
    private readonly IAppFactory _appFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMockTestDumpDataToMongoDbBiz _mockTestDumpDataToMongoDbBiz;
    private readonly IRedisGenericFactory _redisGenericFactory;
    private readonly IRedisGenericRepository<MgMockTestModel> _redisMockTestRepos;

    public MockTestRedisDataService(
        IAppFactory appFactory,
        ILogger<MockTestRedisDataService> logger,
        IHttpContextAccessor httpContextAccessor,
        IMockTestDumpDataToMongoDbBiz mockTestDumpDataToMongoDbBiz,
        IRedisGenericFactory redisGenericFactory
        )
    {
        _logger = logger;
        _appFactory = appFactory;
        _httpContextAccessor = httpContextAccessor;
        _mockTestDumpDataToMongoDbBiz = mockTestDumpDataToMongoDbBiz;
        _redisGenericFactory = redisGenericFactory;
        _redisMockTestRepos = _redisGenericFactory.Create<MgMockTestModel>(Constants.RedisKey.RedisMockTestModel);
    }

    public async Task<MgMockTestModel> GetMockTest(Guid id, int expiredInSeconds = 600)
    {
        var redisKey = $"{id}";
        var data = await _redisMockTestRepos.GetOrCreateWithDistributedLockAsync(
                $"{redisKey}",
                async () =>
                {
                    return await _mockTestDumpDataToMongoDbBiz.GetOrDumpAsync(id);
                },
                3,
                0,
                TimeSpan.FromSeconds(10),
                (int)expiredInSeconds
            );

        return data;

    }
    public async Task<MgMockTestModel> InsertOrUpdateMockTestAsync(Guid id, MgMockTestModel model, int seconds = 0)
    {
        if (model == null || id == Guid.Empty)
        {
            _logger.LogError("Invalid parameters for updating mock test menu.");
            return null;
        }
        try
        {
            if (seconds == 0)
            {
                seconds = await _redisMockTestRepos.GetTTL(id.ToString());
            }
            seconds = seconds > 0 ? seconds : 600;
            await _redisMockTestRepos.Add(id.ToString(), model, seconds);
            return model;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating mock test for id: {KeyCode}", id);
            throw;
        }
    }
    public async Task<IEnumerable<MocktestSection>> GetMockTestSections(Guid mockTestId)
    {
        var redisService = _redisGenericFactory.CreateCollection<MocktestSection>();
        var redisKey = $"{mockTestId}";
        var dataList = await redisService.GetOrCreateWithDistributedLockAsync(
              $"{redisKey}",
              async () =>
              {
                  var listData = await _appFactory.Repository<MocktestSection>().GetAll()
                        .Include(x => x.MocktestParts)
                            .ThenInclude(x => x.MocktestPartQuestionnaires)
                                .ThenInclude(x => x.Questionnaire)
                                    .ThenInclude(x => x.Questions)
                    .Where(x => x.MocktestId == mockTestId)
                    .OrderBy(x => x.SortOrder)
                    .ToListAsync();
                  return listData;
              },
              3,
              0,
              TimeSpan.FromSeconds(10),
              (int)(TimeSpan.FromMinutes(10).TotalSeconds)
          );

        return dataList;

    }

    public async Task<MocktestTranslation> GetMockTestTranslation(Guid mockTestId)
    {
        var redisService = _redisGenericFactory.Create<MocktestTranslation>();
        var redisKey = $"{mockTestId}";
        var data = await redisService.GetOrCreateWithDistributedLockAsync(
            $"{redisKey}",
            async () =>
            {
                var tran = _appFactory.Repository<MocktestTranslation>().GetAll()
                                            .Include(x => x.Mocktest)
                                            .FirstOrDefault(x => x.LanguageCode == Constants.LanguageTags.TiengViet &&
                                                                  x.MocktestId == mockTestId);
                return tran;
            },
            3,
            0,
            TimeSpan.FromSeconds(10),
            (int)(TimeSpan.FromMinutes(10).TotalSeconds)
        );
        return data;

    }

}
