using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Chat
{
    public class ChatModel
    {
        public ObjectId UserId { get; set; }
        public string Username { get; set; }
        public string? UserImageId { get; set; }

        public DateTime LastReceived { get; set; }
        public DateTime? NewestMessage { get; set; }

        public string? LastMessageText { get; set; }

        public ChatModel(ObjectId userId, string username, string? userImageId, DateTime lastRead, DateTime? newestMessage, string? lastMessageText)
        {
            UserId = userId;
            Username = username;
            UserImageId = userImageId;
            LastReceived = lastRead;
            NewestMessage = newestMessage;
            LastMessageText = lastMessageText;
        }
    }
}
