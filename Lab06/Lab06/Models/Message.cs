using System;
using System.Collections.Generic;
using System.Text;

namespace Lab06.Models
{
    public class Message
    {
        public string MessageId { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public DateTime SendDate { get; set; }
        public string Text { get; set; }
    }
}
