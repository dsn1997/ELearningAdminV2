
using IIG.Application.Services.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace IIG.Application.DI
{
    public static partial class ServiceApplicationExtensions
    {
        private static IServiceCollection AddMongoDataService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMongoMockTestService, MongoMockTestService>();
            services.AddScoped<IMockTestDumpDataToMongoDbBiz, MockTestDumpDataToMongoDbBiz>();
            return services;
        }

    }
}
