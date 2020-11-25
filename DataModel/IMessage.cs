using System;

namespace DataModel
{
    interface IMessage
    {
        DateTimeOffset SentTimestamp { get; set; }
        byte[] Body { get; set; }
    }
}
