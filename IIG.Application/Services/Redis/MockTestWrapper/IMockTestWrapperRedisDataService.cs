using IIG.Web.Data.Models;

namespace IIG.Application.Services.Redis;

public interface IMockTestWrapperRedisDataService
{
    Task<IEnumerable<Menu_MockTestWrapperDto>> GetListByGroupId(Guid MockTestWrapperGroupId,string languageCode);
    Task<MockTestWrapperDetailDto> GetDetailById(Guid wrapperId, string languageCode);
}