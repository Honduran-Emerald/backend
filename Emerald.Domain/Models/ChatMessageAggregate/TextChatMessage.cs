using Emerald.Domain.Models.ChatAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.ChatMessageAggregate
{
    public class TextChatMessage : ChatMessage
    {
        public string Text { get; set; }

        public TextChatMessage(ObjectId senderId, ObjectId receiverId, string message) 
            : base(senderId, receiverId)
        {
            Text = message;
        }

        public override string ToPreviewMessage()
        {
            return Text.Length > 50
                ? Text.Substring(0, 50)
                : Text;
        }
    }
}
