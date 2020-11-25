using System;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;

namespace MessageSender
{
    class Program
    {
        static void Main(string[] args)
        {
            using var channel = RabbitMqHelpers.GetRabbitMqChannel();

            while (true)
            {
                Console.WriteLine("Type your message and hit enter:");
                var message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: "hello", body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}
