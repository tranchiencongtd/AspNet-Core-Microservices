using System.Text;
using Contracts.Common.Interfaces;
using Contracts.Messages;
using RabbitMQ.Client;

namespace Infrastructure.Messages
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly ISeriallizeService _seriallizeService;

        public RabbitMQProducer(ISeriallizeService seriallizeService)
        {
            _seriallizeService = seriallizeService;
        }

        public void SendMessage<T>(T message)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("orders", exclusive: false);
            var jsonData = _seriallizeService.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonData);

            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
        }
    }
}