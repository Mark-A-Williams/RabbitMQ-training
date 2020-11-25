using Newtonsoft.Json;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageReceiver
{
    class Program
    {
        private static Dictionary<Guid, int> _keyRetries;
        private const int _maximumRetries = 5;

        static void Main(string[] args)
        {
            _keyRetries = new Dictionary<Guid, int>();

            using var channel = RabbitMqHelpers.GetRabbitMqChannel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                Console.WriteLine($"Received message");

                var message = GetMessageFromEvent(eventArgs);
                try
                {
                    MessageService.ProcessMessage(message);
                    channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    if (ShouldRetry(message.Key))
                    {
                        if (!_keyRetries.ContainsKey(message.Key))
                        {
                            _keyRetries.Add(message.Key, 1);
                        }
                        _keyRetries[message.Key]++;
                        Console.WriteLine($"FAILED. Number of attempts now {_keyRetries[message.Key]}\nException: {ex.Message}");
                        channel.BasicNack(deliveryTag: eventArgs.DeliveryTag, multiple: false, requeue: true);
                    }
                    else
                    {
                        Console.WriteLine("Exceeded maximum retries - ABORTING");
                        channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
                    }
                }
            };

            channel.BasicConsume(queue: "hello", autoAck: false, consumer: consumer);

            Console.ReadKey();
        }

        private static bool ShouldRetry(Guid key)
        {
            return !(_keyRetries.ContainsKey(key) && _keyRetries[key] < _maximumRetries);
        }

        private static IMessage GetMessageFromEvent(BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var decodedBody = Encoding.UTF8.GetString(body);
            var model = JsonConvert.DeserializeObject<SimpleMessage>(decodedBody);
            return model;
        }
    }
}
