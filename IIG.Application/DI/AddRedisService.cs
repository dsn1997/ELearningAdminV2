
using IIG.Application.Services.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace IIG.Application.DI
{
    public static partial class ServiceApplicationExtensions
    {
      
      
        private static IServiceCollection AddRedisDataService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMockTestKeyCodeRedisDataService, MockTestKeyCodeRedisDataService>();
            services.AddScoped<IMockTestRedisDataService, MockTestRedisDataService>();
            services.AddScoped<IQuestionaireRedisDataService, QuestionaireRedisDataService>();
            services.AddScoped<IMockTestWrapperKeyCodeRedisDataService, MockTestWrapperKeyCodeRedisDataService>();
            services.AddScoped<IMockTestWrapperRedisDataService, MockTestWrapperRedisDataService>();
            return services;
        }

    }
}
