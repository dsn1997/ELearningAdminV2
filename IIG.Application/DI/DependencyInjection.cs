using IIG.Application.AutoMapper;
using IIG.Application.BackgroundJob;
using IIG.Application.Services;
using IIG.Core.Base;
using IIG.Core.Providers.BackgroudJob;
using IIG.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace IIG.Application.DI
{
    public static partial class ServiceApplicationExtensions
    {
        public static IServiceCollection AddServiceApplicationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppFactory, AppFactory>();
            services.AddScoped<IHttpRequestService, HttpRequestService>();
            services.AddAutoMapper(cfg => cfg.AddProfile(new AutoMapProfile()));
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

            AddDataAccess(services, configuration);
            AddRedisDataService(services, configuration);
            AddMongoDataService(services, configuration);
            AddService(services, configuration);

            return services;
        }

        private static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IMockTestService, MockTestService>();
            services.AddScoped<IFileService,FileService>();
            services.AddScoped<IFileTypeService, FileTypeService>();
            services.AddScoped<IFileUploaderService, FileUploaderService>();
            //services.AddSingleton<IRabbitListener, MockTestKeyCodeRabbitListener>();

            return services;
        }
    
    }
}
