namespace IIG.Core.Common.ConfigureModels
{
    public class AppSettingOptions
    {
        public const string AppSettings = "AppSettings";
        public string AuthenticateUrl { get; set; }
        public string TokenSecretKey { get; set; }
        public string TokenLifeTime { get; set; }
        public string RefreshTokenLifeTime { get; set; }
        public string ForgotCodeLifeTime { get; set; }
        public string RegisterEmailVerifyLifeTime { get; set; }
        public int MaxAccessFailed { get; set; }
        public string RedisConnectionString { get; set; }
        public string GoogleTranslationApiKey { get; set; }
        public string PublicFileBaseUrl { get; set; }
        
        public int NumberUserOverload { get; set; }
    }
}
