using RabbitMQ.Client;
using System;

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
    }
}
