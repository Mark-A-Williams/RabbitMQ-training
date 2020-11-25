using System;

namespace RabbitMQ
{
    public interface IMessage
    {
        Guid Key { get; set; }
        DateTimeOffset SentTimestamp { get; set; }
        byte[] EncodedContent { get; set; }
        string DecodedContent { get; set; }
    }
}
