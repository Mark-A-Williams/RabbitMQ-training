using System;
using System.Text;
using Newtonsoft.Json;
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
                Console.WriteLine("Enter a new message:");
                var input = Console.ReadLine();
                
                var body = GetSerializedMessage(input);

                channel.BasicPublish(exchange: "", routingKey: "hello", body: Encoding.UTF8.GetBytes(body));
                Console.WriteLine(" [x] Sent {0}", body);
            }
        }

        private static string GetSerializedMessage(object input)
        {
            var message = RabbitMqHelpers.CreateMessageFromModel(input);
            return JsonConvert.SerializeObject(message);
        }
    }
}
