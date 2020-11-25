using RabbitMQ;
using System;
using System.Threading;

namespace MessageReceiver
{
    public static class MessageService
    {
        public static void ProcessMessage(IMessage message)
        {
            Console.WriteLine($"Beginning processing of message {message.Key}");
            Thread.Sleep(2000);

            if (ShouldFail(message))
            {
                throw new Exception("Message processing failed, due to reasons.");
            }

            Thread.Sleep(5000);
            Console.WriteLine($"Completed processing of message {message.Key}. Message content: {message.DecodedContent}");
        }

        private static bool ShouldFail(IMessage message)
        {
            return message.DecodedContent.Contains("fail", StringComparison.OrdinalIgnoreCase);
        }

        private static bool ShouldFail()
        {
            var rng = new Random();
            var number = rng.Next(1, 4);
            return number == 3;
        }
    }
}
