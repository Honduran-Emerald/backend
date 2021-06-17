using Emerald.Domain.Models.ChatMessageAggregate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.ChatAggregate
{
    [BsonKnownTypes(
        typeof(TextChatMessage),
        typeof(ImageChatMessage))]
    public abstract class ChatMessage : Entity
    {
        public ObjectId SenderId { get; set; }
        public ObjectId ReceiverId { get; set; }

        public DateTime CreationTime { get; set; }

        public ChatMessage(ObjectId senderId, ObjectId receiverId)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            CreationTime = DateTime.UtcNow;
        }

        public abstract string ToPreviewMessage();
    }
}
