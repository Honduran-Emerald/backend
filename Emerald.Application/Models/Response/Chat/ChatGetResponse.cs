using Emerald.Application.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Chat
{
    public class ChatGetResponse
    {
        public List<ChatMessageModel> Messages { get; set; }

        public ChatGetResponse(List<ChatMessageModel> messages)
        {
            Messages = messages;
        }
    }
}
