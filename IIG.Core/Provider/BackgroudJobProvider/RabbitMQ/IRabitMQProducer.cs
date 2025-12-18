
namespace IIG.Core.Providers.RabbitMQProvider
{
    public interface IRabbitMQProducer
    {
        public void SendMessage<T>(T message, string exchangeName, string routingKey);
    }
}
