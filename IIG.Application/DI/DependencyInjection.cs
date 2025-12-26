using IIG.Application.AutoMapper;
using IIG.Application.BackgroundJob;
using IIG.Application.Data;
using IIG.Application.Services;
using IIG.Core.Base;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Providers.BackgroudJob;
using IIG.Core.Services;
using IIG.Core.Services.Interfaces;
using IIG.Web.BL.Services.Interfaces.MockTests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace IIG.Application.DI
{
    public static class ServiceApplicationExtensions
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

        private static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileDA, FileDA>();
            services.AddScoped<IFileTypeDA, FileTypeDA>();

            services.AddScoped<IMockTestDA, MockTestDA>();
            services.AddScoped<IMockTestKeyCodeDA, MockTestKeyCodeDA>();
            services.AddScoped<IKeyCodeAnswerDA, KeyCodeAnswerDA>();
            services.AddScoped<IMockTestPartDA, MockTestPartDA>();
            services.AddScoped<IMockTestPartQuestionnaireDA, MockTestPartQuestionnaireDA>();
            services.AddScoped<IMockTestSectionDA, MockTestSectionDA>();
            services.AddScoped<IStepQuestionnaireDA,StepQuestionnaireDA>();
            services.AddScoped<IQuestionnaireDA, QuestionnaireDA>();
            services.AddScoped<ILeftSectionDA, LeftSectionDA>();
            services.AddScoped<IQuestionDA, QuestionDA>();
            services.AddScoped<IAnswerDA, AnswerDA>();
         
            return services;
        }

        private static IServiceCollection AddRedisDataService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMockTestKeyCodeRedisDataService, MockTestKeyCodeRedisDataService>();
            services.AddScoped<IMockTestRedisDataService, MockTestRedisDataService>();
            services.AddScoped<IQuestionaireRedisDataService, QuestionaireRedisDataService>();
            return services;
        }

        private static IServiceCollection AddMongoDataService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMongoMockTestService, MongoMockTestService>();
            services.AddScoped<IMockTestDumpDataToMongoDbBiz, MockTestDumpDataToMongoDbBiz>();
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
            services.AddSingleton<IRabbitListener, MockTestKeyCodeRabbitListener>();

            return services;
        }
    
    }
}
