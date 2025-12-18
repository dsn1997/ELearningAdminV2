namespace IIG.Core.Services
{
    public interface IHttpRequestService
    {
        Task<string> GetCurrentLanguageCode();

        string GetClientIpAddress();
    }
}
