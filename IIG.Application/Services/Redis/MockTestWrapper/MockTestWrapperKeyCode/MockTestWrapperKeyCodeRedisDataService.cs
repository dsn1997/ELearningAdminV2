using AutoMapper;
using IIG.Core.Base;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Providers.Interfaces;
using Microsoft.Extensions.Logging;
using static IIG.Core.Common.ConfigureModels.Constants;


namespace IIG.Application.Services.Redis;

public partial class MockTestWrapperKeyCodeRedisDataService : IMockTestWrapperKeyCodeRedisDataService
{
    private readonly ILogger<MockTestWrapperKeyCodeRedisDataService> _logger;
    private readonly IAppFactory _appFactory;

    private readonly IMapper _mapper;

    private readonly IRedisGenericFactory _redisGenericFactory;


    public MockTestWrapperKeyCodeRedisDataService(
        ILogger<MockTestWrapperKeyCodeRedisDataService> logger,
        IAppFactory appFactory,
        IMapper mapper,
        IRedisGenericFactory redisGenericFactory
        )
    {
        _logger = logger;
        _appFactory = appFactory;

        _mapper = mapper;
        _redisGenericFactory = redisGenericFactory;
    }

    public async Task<MgMockTestKeyCodeModel> GetMockTestKeyCodeInfoByUser(Guid mockTestWrapperId, Guid webUserId)
    {
        try
        {
            var redisService = _redisGenericFactory.Create<MgMockTestKeyCodeModel>(RedisKey.MockTestWrapperKeyCode.MockTestByUserId);
            var redisKey = $"{mockTestWrapperId}:{webUserId}";
            var data = await redisService.Get(redisKey);
            return data;
        }
        catch
        {
            throw;
        }

    }

    public async Task InsertMockTestKeyCodeInfoByUser(Guid mockTestWrapperId, Guid webUserId, MgMockTestKeyCodeModel model)
    {
        try
        {
            var redisService = _redisGenericFactory.Create<MgMockTestKeyCodeModel>(RedisKey.MockTestWrapperKeyCode.MockTestByUserId);
            var redisKey = $"{mockTestWrapperId}:{webUserId}";
            await redisService.Add(redisKey, model, model.TimeRemaining +30);
        }
        catch
        {
            throw;
        }

    }

    public async Task DeleteMockTestKeyCodeInfoByUser(Guid mockTestWrapperId, Guid webUserId)
    {
        try
        {
            var redisService = _redisGenericFactory.Create<MgMockTestKeyCodeModel>(RedisKey.MockTestWrapperKeyCode.MockTestByUserId);
            var redisKey = $"{mockTestWrapperId}:{webUserId}";
            await redisService.Remove(redisKey);
        }
        catch
        {
            throw;
        }

    }
}

