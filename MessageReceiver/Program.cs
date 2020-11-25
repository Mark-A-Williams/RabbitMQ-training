using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MessageReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            using var channel = RabbitMqHelpers.GetRabbitMqChannel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };

            channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

            Console.ReadKey();
        }
    }
}
