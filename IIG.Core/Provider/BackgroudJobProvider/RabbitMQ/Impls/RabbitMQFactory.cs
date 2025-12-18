using IIG.Core.Common.ConfigureModels;

using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace IIG.Core.Providers.RabbitMQProvider.Impls
{
    public class RabbitMQFactory : IRabbitMQFactory
    {
        private readonly RabbitMQSettingOptions _rabbitMQSetting;

        public RabbitMQFactory(IOptions<RabbitMQSettingOptions> options)
        {
            _rabbitMQSetting = options.Value;
        }

        public IConnection Connection => CreateConnection();

        private IConnection CreateConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                UserName = _rabbitMQSetting.UserName,
                Password = _rabbitMQSetting.Password,
                HostName = _rabbitMQSetting.HostName,
                VirtualHost = _rabbitMQSetting.VHost,
                Port = _rabbitMQSetting.Port,
            };

            return connectionFactory.CreateConnection();
        }

        public int GetMaxChannel()
        {
            return _rabbitMQSetting.MaxChannel.HasValue ? _rabbitMQSetting.MaxChannel.Value : 300;
        }
    }
}
