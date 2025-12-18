using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using System.Text;

namespace IIG.Core.Providers.RabbitMQProvider.Impls
{

    public class NullRabbitMQProducer : IRabbitMQProducer
    {
        public void SendMessage<T>(T message, string exchangeName, string routingKey)
        {

        }    
    }
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IRabbitMQFactory _rabbitMQFactory;
        private readonly ConcurrentBag<IModel> _channelPool = new();
        private int _maxChannels = 10; // Giới hạn số lượng channel tối đa trong pool
        private int _currentChannelCount = 0;
        private readonly object _createLock = new();

        public RabbitMQProducer(IRabbitMQFactory rabbitMQFactory)
        {
            _rabbitMQFactory = rabbitMQFactory;
            _maxChannels = _rabbitMQFactory.GetMaxChannel();
        }

        public void SendMessage<T>(T message, string exchangeName, string routingKey)
        {
            var channel = GetChannel();
            bool success = false;

            try
            {
                for (int retryCount = 0; retryCount < 4; retryCount++) // retry 3 lần
                {

                    try
                    {

                        var json = JsonConvert.SerializeObject(message);
                        var body = Encoding.UTF8.GetBytes(json);
                        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, body: body);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending message: {ex.Message}");
                        if (retryCount < 3) 
                        {
                            Console.WriteLine("Retrying in 5 seconds...");
                            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                        }
                        else
                        {
                            throw new Exception("Max retries sending message reached. Giving up.");
                        }

                        // Nếu channel có vấn đề thì loại bỏ và tạo lại cho lần retry kế tiếp
                        if (channel == null || channel.IsClosed)
                        {
                            channel?.Dispose();
                            channel = GetChannel();
                        }
                    }


                }
            }
            finally
            {
                if (channel != null)
                {
                    if (channel.IsOpen)
                        ReturnChannel(channel);
                    else
                        channel.Dispose();
                }
            }
        }


        private IModel GetChannel()
        {
            // Lấy channel có sẵn trong pool
            if (_channelPool.TryTake(out var channel))
            {
                if (channel.IsOpen)
                    return channel;

                try { channel.Dispose(); } catch { }
            }

            // Nếu chưa đạt giới hạn pool thì tạo mới
            lock (_createLock)
            {
                if (_currentChannelCount < _maxChannels)
                {
                    var newChannel = _rabbitMQFactory.Connection.CreateModel();
                    Interlocked.Increment(ref _currentChannelCount);
                    return newChannel;
                }
            }

            // Nếu hết channel, chờ 100ms và thử lại
            Thread.Sleep(100);
            return GetChannel();
        }

        private void ReturnChannel(IModel channel)
        {
            if (channel == null) return;

            if (!channel.IsOpen)
            {
                try
                {
                    channel.Dispose();
                    Interlocked.Decrement(ref _currentChannelCount);
                }
                catch { }
                return;
            }

            _channelPool.Add(channel);
        }
    }
}
