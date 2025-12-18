using RabbitMQ.Client;

namespace IIG.Core.Providers.RabbitMQProvider
{
    public interface IRabbitMQFactory
    {
        IConnection Connection { get; }
        public int GetMaxChannel();
    }
}
