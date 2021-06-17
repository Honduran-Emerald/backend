using Emerald.Application.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Chat
{
    public class ChatGetResponse
    {
        public ChatModel ChatModel { get; set; }
        public List<ChatMessageModel> Messages { get; set; }

        public ChatGetResponse(ChatModel chatModel, List<ChatMessageModel> messages)
        {
            ChatModel = chatModel;
            Messages = messages;
        }
    }
}
