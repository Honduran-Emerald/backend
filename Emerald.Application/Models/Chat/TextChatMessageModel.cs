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

        public TextChatMessageModel(DateTime creationTime, bool received, string text)
            : base(creationTime, received, ChatType.Text)
        {
            Text = text;
        }
    }
}
