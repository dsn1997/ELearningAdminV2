using IIG.Application.Services;
using IIG.Core.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Application.DI
{
    public static class ServiceApplicationExtensions
    {
        public static IServiceCollection AddServiceApplicationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppFactory, AppFactory>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ITagService, TagService>();

            return services;
        }
    }
}
