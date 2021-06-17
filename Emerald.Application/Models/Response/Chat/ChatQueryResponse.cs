using Emerald.Application.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Chat
{
    public class ChatQueryResponse
    {
        public List<ChatModel> Chats { get; set; }

        public ChatQueryResponse(List<ChatModel> chats)
        {
            Chats = chats;
        }
    }
}
