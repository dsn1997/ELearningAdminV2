using IIG.Application.Data;
using IIG.Application.Models.MockTestKeyCode;
using IIG.Core.Base;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.Enums;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Entities;
using IIG.Core.Providers.Interfaces;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;



namespace IIG.Application.Services.Redis;

public partial class MockTestKeyCodeRedisDataService : IMockTestKeyCodeRedisDataService
{
    private readonly ILogger<MockTestKeyCodeRedisDataService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAppFactory _appFactory;
    private readonly IMockTestKeyCodeDA _mockTestKeyCodeDA;
    private readonly IMongoGenericRepository<MgMockTestKeyCodeModel> _mongoMockTestKeyCodeRepos;
    private readonly IMongoGenericRepository<MgKeyCodeAnswerModel> _mongoKeyCodeAnswerRepos;
    private readonly IRedisGenericRepository<MgMockTestKeyCodeModel> _redisMockTestKeyCodeService;
    private readonly IRedisGenericCollectionRepository<MgKeyCodeAnswerModel> _redisMockTestKeyCodeAnswerService;
    private readonly IRedisGenericRepository<Tuple<CourseScoring, bool>> _redisMockTestKeyCodeCourseScoringTupleService;
    private readonly IRedisGenericFactory _redisFactory;
    private readonly IConnectionMultiplexer _connection;


    public MockTestKeyCodeRedisDataService(
        ILogger<MockTestKeyCodeRedisDataService> logger,
        IHttpContextAccessor httpContextAccessor,
        IAppFactory appFactory,
        IMockTestKeyCodeDA mockTestKeyCodeDA,
        IMongoGenericRepository<MgMockTestKeyCodeModel> mongoMockTestKeyCodeRepos,
        IMongoGenericRepository<MgKeyCodeAnswerModel> mongoKeyCodeAnswerRepos,
        IRedisGenericFactory redisFactory,
        IConnectionMultiplexer connection
        )
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _appFactory = appFactory;
        _redisFactory = redisFactory;
        _mockTestKeyCodeDA = mockTestKeyCodeDA;
        _mongoMockTestKeyCodeRepos = mongoMockTestKeyCodeRepos;
        _mongoKeyCodeAnswerRepos = mongoKeyCodeAnswerRepos;
        _redisMockTestKeyCodeService = _redisFactory.Create<MgMockTestKeyCodeModel>(Constants.RedisKey.RedisMockTestKeyCodeModel);
        _redisMockTestKeyCodeCourseScoringTupleService = _redisFactory.Create<Tuple<CourseScoring, bool>>(Constants.RedisKey.RedisMockTestKeyCodeCourseScoringTuple);
        _redisMockTestKeyCodeAnswerService = _redisFactory.CreateCollection<MgKeyCodeAnswerModel>(Constants.RedisKey.RedisMockTestKeyCodeAnswer);
        _connection = connection;
    }
    public async Task<MgMockTestKeyCodeModel> GetMockTestKeyCodeAsync(string keyCode)
    {
        var redisKey = $"{keyCode}";

        var data = await _redisMockTestKeyCodeService.GetOrCreateWithDistributedLockAsync(
          $"{redisKey}",
          () => _mongoMockTestKeyCodeRepos.FindByIdAsync(x => x.KeyCode == keyCode),
          3,
          0,
          TimeSpan.FromSeconds(10),
          (int)TimeSpan.FromSeconds(60).TotalSeconds
      );
        return data;
    }

    public async Task<Tuple<CourseScoring, bool>> GetTupleCourseScoring(string keyCode)
    {
        var redisKey = $"{keyCode}";

        var data = await _redisMockTestKeyCodeCourseScoringTupleService.GetOrCreateWithDistributedLockAsync(
            $"{redisKey}",
            () =>
            {
                var courseScoring = _appFactory.Repository<CourseScoring>().GetAll().Include(x => x.MocktestKeyCode)
                              .ThenInclude(x => x.Mocktest).ThenInclude(x => x.MocktestSections)
                              .ThenInclude(x => x.MocktestParts).ThenInclude(x => x.MocktestPartQuestionnaires)
                              .ThenInclude(x => x.Questionnaire)
                              .AsNoTracking()
                              .Where(x => x.KeyCode == keyCode)
                              .AsEnumerable()
                              .Select(x => new Tuple<CourseScoring, bool>
                              (
                                  x,
                                   x.MocktestKeyCode.Mocktest.MocktestSections.Any(x =>
                                  x.MocktestParts.Any(p => p.MocktestPartQuestionnaires.Any(q =>
                                  q.Questionnaire.Type == (short)EQuestionnaireType.Record
                                  || q.Questionnaire.Type == (short)EQuestionnaireType.ReadTextALoud
                                  || q.Questionnaire.Type == (short)EQuestionnaireType.Writing

                                  )))
                              ))
                              .OrderByDescending(x => x.Item1.SubmittedDate).ThenByDescending(p => p.Item1.Status)
                              .FirstOrDefault();
                return Task.FromResult(courseScoring);
            },
            3,
            0,
            TimeSpan.FromSeconds(10),
            (int)TimeSpan.FromSeconds(60).TotalSeconds
        );
        return data;

    }

    public async Task<MockTestKeyCodeDetailDto> GetKeyCodeDetailByCookieAsync(Guid cookie)
    {
        var redisService = _redisFactory.Create<MockTestKeyCodeDetailDto>(Constants.RedisKey.RedisMockTestKeyCodeDetailByCookie);
        (var dataJson, var data) = await redisService.GetExact(cookie.ToString());
        if (string.IsNullOrEmpty(dataJson) && data == null)
        {
            data = await _mockTestKeyCodeDA.GetKeyCodeDetailByCookieAsync(cookie);
            await redisService.Add(cookie.ToString(), data, (int)TimeSpan.FromMinutes(5).TotalSeconds);
        }
        return data;
    }

    public async Task<MgMockTestKeyCodeModel> InsertOrUpdateKeyCodeAsync(string keyCode, MgMockTestKeyCodeModel mgMockTestKeyCode, int seconds = 0)
    {
        if (mgMockTestKeyCode == null || string.IsNullOrEmpty(keyCode))
        {
            _logger.LogError("Invalid parameters for updating mock test menu.");
            return null;
        }
        try
        {
            if (seconds == 0)
            {
                seconds = await _redisMockTestKeyCodeService.GetTTL(keyCode);
            }
            //seconds = seconds > 0 ? seconds : (int)TimeSpan.FromHours(3).TotalSeconds;
            await _redisMockTestKeyCodeService.Add(keyCode, mgMockTestKeyCode);
            return mgMockTestKeyCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating mock test menu for keyCode: {KeyCode}", keyCode);
            return null;
        }
    }

    public async Task<IEnumerable<MgKeyCodeAnswerModel>> GetKeyCodeAnswerAsync(string keyCode)
    {
        try
        {
            var redisKey = $"{keyCode}";
            var listData = await _redisMockTestKeyCodeAnswerService.GetOrCreateWithDistributedLockAsync(keyCode,
                () => _mongoKeyCodeAnswerRepos.FilterAsync(x => x.KeyCode.Equals(keyCode)),
                 3,
                0,
                TimeSpan.FromSeconds(10),
                (int)TimeSpan.FromMinutes(15).TotalSeconds
                );

            return listData;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving key code answer for keyCode: {KeyCode}", keyCode);
            return null;
        }
    }

    public async Task DeleteKeyCodeAsync(string keyCode)
    {
        var redisKey = $"{keyCode}";
        await _redisMockTestKeyCodeService.Remove(keyCode);
        await _redisMockTestKeyCodeAnswerService.Remove(keyCode);
        await _redisMockTestKeyCodeCourseScoringTupleService.Remove(keyCode);
    }
    public async Task<bool> DeleteKeyCodeAnswer(string keyCode, Func<MgKeyCodeAnswerModel, bool> predicate)
    {
        try
        {
            await _redisMockTestKeyCodeAnswerService.DeleteRange(keyCode, predicate);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> InsertKeyCodeAnswer(string keyCode, MgKeyCodeAnswerModel model)
    {
        try
        {
            //var seconds = await _redisMockTestKeyCodeAnswerService.GetTTL(keyCode);
            //seconds = seconds > 0 ? seconds : (int)TimeSpan.FromHours(3).TotalSeconds;
            var listAnswers = await GetKeyCodeAnswerAsync(keyCode);
            var listAnserSaved = listAnswers.ToList();
            listAnserSaved.Add(model);
            await _redisMockTestKeyCodeAnswerService.Add(keyCode, listAnserSaved);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting key code answer for keyCode: {KeyCode}", keyCode);
            return false;
        }
    }

    //public async Task<int> CountMockTestKeyCodeExamining()
    //{
    //    return await CountMockTestKeyCodeExaminingAction(0);
    //}

    //public async Task ClearCountMockTestKeyCodeExamining()
    //{
    //    var redisKey = Constants.RedisKey.RedisMockTestKeyCodeExaminingCount;
    //    var redisService = _redisFactory.Create<RedisDto<int>>(redisKey);
    //    await redisService.Remove(redisKey);
    //}
    //private async Task<int> CountMockTestKeyCodeExaminingAction(int retryCount = 0)
    //{
    //    var redisKey = Constants.RedisKey.RedisMockTestKeyCodeExaminingCount;
    //    var redisService = _redisFactory.Create<RedisDto<int>>(redisKey);
    //    var count = 0;

    //    TimeSpan lockExpiry = TimeSpan.FromSeconds(5); // Lock will expire after 5 seconds
    //    var lockValue = Guid.NewGuid().ToString();
    //    const int maxRetries = 3;
    //    var dataRedis = await redisService.Get(redisKey);
    //    if (dataRedis != null)
    //    {
    //        count = dataRedis.Data;
    //        return count;
    //    }
    //    else
    //    {

    //        if (await _connection.GetDatabase().LockTakeAsync($"{redisKey}_Lock", lockValue, lockExpiry))
    //        {
    //            try
    //            {
    //                count = await _mockTestKeyCodeDA.CountMockTestKeyCodeExamining();
    //                await redisService.Add(redisKey, new RedisDto<int>
    //                {
    //                    Data = count,
    //                });
    //            }
    //            catch (Exception ex)
    //            {
    //                _logger.LogError(ex, "Error counting mock test key codes examining.");
    //                return 0;
    //            }
    //            finally
    //            {
    //                // Release the lock
    //                await _connection.GetDatabase().LockReleaseAsync($"{redisKey}_Lock", lockValue);
    //            }
    //        }
    //        else
    //        {
    //            if (retryCount < maxRetries)
    //            {
    //                await Task.Delay(TimeSpan.FromSeconds(1));
    //                return await CountMockTestKeyCodeExaminingAction(retryCount + 1);
    //            }
    //            else
    //            {
    //                // 4. Fallback sau khi hết số lần thử lại 
    //                return await _mockTestKeyCodeDA.CountMockTestKeyCodeExamining();
    //            }
    //        }
    //    }
    //    return count;
    //}

}
