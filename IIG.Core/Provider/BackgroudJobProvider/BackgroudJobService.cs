using IIG.Core.Common.ConfigureModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Providers.BackgroudJob
{
    public class RabbitMQBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMQSettingOptions _options;
        private readonly ILogger<RabbitMQBackgroundService> _logger;

        public RabbitMQBackgroundService(
            IServiceProvider serviceProvider,
            IOptions<RabbitMQSettingOptions> options,
            ILogger<RabbitMQBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_options.Enabled)
            {
                _logger.LogInformation("RabbitMQ Background Service is disabled");
                return;
            }

            _logger.LogInformation("RabbitMQ Background Service started");

            using var scope = _serviceProvider.CreateScope();

            var listeners = scope.ServiceProvider
                .GetServices<IRabbitListener>();

            foreach (var listener in listeners)
            {
               await  listener.RegisterAsync(stoppingToken);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
        }
    }

}
