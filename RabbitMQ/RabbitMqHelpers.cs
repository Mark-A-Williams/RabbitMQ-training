using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ
{
    public class RabbitMqHelpers
    {
        public static IModel GetRabbitMqChannel()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var channel = factory.CreateConnection().CreateModel();
            channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false);

            return channel;
        }

        public static IMessage CreateMessageFromModel(object model = default)
        {
            var key = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;

            var modelAsJson = JsonConvert.SerializeObject(model);
            var messageBody = Encoding.UTF8.GetBytes(modelAsJson);

            return new SimpleMessage
            {
                Key = key,
                SentTimestamp = timestamp,
                EncodedContent = messageBody
            };
        }
    }
}
