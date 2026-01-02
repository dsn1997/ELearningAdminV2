
using IIG.Application.Data;
using IIG.Web.Data.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace IIG.Application.DI
{
    public static partial class ServiceApplicationExtensions
    {

        private static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileDA, FileDA>();
            services.AddScoped<IFileTypeDA, FileTypeDA>();

            services.AddScoped<IMockTestDA, MockTestDA>();
            services.AddScoped<IMockTestKeyCodeDA, MockTestKeyCodeDA>();
            services.AddScoped<IKeyCodeAnswerDA, KeyCodeAnswerDA>();
            services.AddScoped<IKeyCodeResultDA, KeyCodeResultDA>();

            services.AddScoped<IMockTestPartDA, MockTestPartDA>();
            services.AddScoped<IMockTestPartQuestionnaireDA, MockTestPartQuestionnaireDA>();
            services.AddScoped<IMockTestSectionDA, MockTestSectionDA>();
            services.AddScoped<IStepQuestionnaireDA, StepQuestionnaireDA>();
            services.AddScoped<IQuestionnaireDA, QuestionnaireDA>();
            services.AddScoped<ILeftSectionDA, LeftSectionDA>();
            services.AddScoped<IQuestionDA, QuestionDA>();
            services.AddScoped<IAnswerDA, AnswerDA>();
            services.AddScoped<IMockTestWrapperDA, MockTestWrapperDA>();

            return services;
        }

    }
}
