using IIG.Application.Models;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Helpers;
using IIG.Core.Providers.Caching;
using IIG.Core.Services;
using Microsoft.AspNetCore.Http;

namespace IIG.Application.Services
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCacheProvider _distributedCacheProvider;

        public HttpRequestService(IHttpContextAccessor httpContextAccessor,
            IDistributedCacheProvider distributedCacheProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _distributedCacheProvider = distributedCacheProvider;
        }

        public async Task<string> GetCurrentLanguageCode()
        {
            var languageCode = HttpRequestExtension.GetHeaderByKey(_httpContextAccessor.HttpContext.Request, Constants.RequestHeaderKey.LanguageCode);
            if (!string.IsNullOrEmpty(languageCode)) return languageCode;

            var languageCache = await _distributedCacheProvider
                  .Cache<IEnumerable<LanguageTagsDto>>()
                  .Get(Constants.RedisKey.RedisLanguageTags);

            return languageCache?.FirstOrDefault(x => x.IsDefault)?.Code;
        }

        public string GetClientIpAddress()
        {
            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                return _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];

            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }
}
