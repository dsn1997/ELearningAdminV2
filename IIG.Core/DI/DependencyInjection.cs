using IIG.Core.Common.ConfigureModels;
using IIG.Core.Providers.Caching;
using IIG.Core.Providers.Caching.Impls;
using IIG.Core.Providers.Impls;
using IIG.Core.Providers.Interfaces;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StackExchange.Redis;
using System.Net.Sockets;

namespace IIG.Core.DI
{
    public static class ServiceCoreExtensions
    {
        public static IServiceCollection AddServiceCoreConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRedisGenericFactory, RedisGenericFactory>();
            services.Configure<DbConnectionStringsOptions>(options=> configuration.GetSection(DbConnectionStringsOptions.DbConnectionStrings).Bind(options));
            AddMongoServices(services, configuration);
            AddCacheServices(services, configuration);

            return services;
        }

        public static IServiceCollection AddMongoServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var mongoDbSettings = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<DbConnectionStringsOptions>>();
                MongoClientSettings settings = MongoClientSettings.FromConnectionString(mongoDbSettings.Value.MongoDbConnection);
                // change some fields based on your needs
                settings.WaitQueueTimeout = TimeSpan.FromMinutes(1);
                settings.MinConnectionPoolSize = 100;
                settings.MaxConnectionPoolSize = 1000;
                settings.MaxConnecting = int.MaxValue;
                settings.ConnectTimeout = TimeSpan.FromSeconds(30);
                settings.SocketTimeout = TimeSpan.FromSeconds(60);
                settings.ServerSelectionTimeout = TimeSpan.FromSeconds(30);
                // Bật keepalive
                settings.ClusterConfigurator = cb =>
                {
                    cb.ConfigureTcp(tcp => tcp.With(
                        socketConfigurator: new Action<Socket>(socket =>
                        {
                            // Bật TCP KeepAlive
                            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                            // Tuỳ OS, có thể set thêm TCP keepalive time/interval bằng P/Invoke nếu cần
                        })
                    ));
                };

                return new MongoClient(settings).GetDatabase(mongoDbSettings.Value.MongoDbDatabaseName); ;
            });

            services.AddScoped(typeof(IMongoGenericRepository<>), typeof(MongoGenericRepository<>));

            return services;

        }

        public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettingOptions>(options =>
            {
                configuration.GetSection(AppSettingOptions.AppSettings).Bind(options);
            });
            var appSettingOptions = new AppSettingOptions();
            configuration.GetSection(AppSettingOptions.AppSettings).Bind(appSettingOptions);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = appSettingOptions.RedisConnectionString;
            });

            services.AddSingleton<IDistributedCacheProvider, DistributedCacheProvider>();
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(appSettingOptions.RedisConnectionString));
            services.AddScoped<IRedisGenericFactory, RedisGenericFactory>();

            return services;
        }
    }
}
