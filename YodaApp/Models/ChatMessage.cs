using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YodaApp.Enums;

namespace YodaApp.Models
{
    public class ChatMessage
    {
        public ChatMessageTypeEnum MessageType { get; set; }
        public string? MessageBody { get; set; }

    }
}
