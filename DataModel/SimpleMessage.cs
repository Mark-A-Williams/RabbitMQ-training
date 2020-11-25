using System;

namespace DataModel
{
    public class SimpleMessage : IMessage
    {
        public DateTimeOffset SentTimestamp { get ; set; }
        public byte[] Body { get; set; }
    }
}
