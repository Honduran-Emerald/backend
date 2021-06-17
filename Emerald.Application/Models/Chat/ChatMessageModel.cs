using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Chat
{
    public class ChatMessageModel
    {
        public DateTime CreationTime { get; set; }
        public bool Received { get; set; }

        public ObjectId Sender { get; set; }
        public ChatType Type { get; set; }

        public ChatMessageModel(DateTime creationTime, bool received, ObjectId sender, ChatType type)
        {
            CreationTime = creationTime;
            Received = received;
            Sender = sender;
            Type = type;
        }
    }
}
