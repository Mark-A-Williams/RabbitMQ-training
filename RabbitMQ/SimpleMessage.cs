using System;
using System.Text;

namespace RabbitMQ
{
    public class SimpleMessage : IMessage
    {
        public Guid Key { get; set; }
        public DateTimeOffset SentTimestamp { get ; set; }
        public byte[] EncodedContent { get; set; }
        public string DecodedContent
        {
            get
            {
                return Encoding.UTF8.GetString(EncodedContent);
            }
            set { }
        }
    }
}
