using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Chat
{
    public class TextChatMessageModel : ChatMessageModel
    {
        public string Text { get; set; }

        public TextChatMessageModel(ObjectId userId, string username, string? userImageId, string text)
            : base(userId, username, userImageId)
        {
            Text = text;
        }
    }
}
