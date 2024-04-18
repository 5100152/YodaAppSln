using Azure.AI.OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YodaApp.Models;

namespace YodaApp.Services.Interfaces
{
    public interface IAiAssistant
    {
        string GetRandomStarWarsFact();

        ChatResponseMessage GetCompletion(IList<ChatMessage> chatInboundHistory, ChatMessage userMessage);
    }
}
