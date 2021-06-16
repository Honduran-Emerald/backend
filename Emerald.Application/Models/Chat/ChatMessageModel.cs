using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Chat
{
    public class ChatMessageModel
    {
        public ObjectId UserId { get; set; }
        public string Username { get; set; }
        public string? UserImageId { get; set; }

        public ChatMessageModel(ObjectId userId, string username, string? userImageId)
        {
            UserId = userId;
            Username = username;
            UserImageId = userImageId;
        }
    }
}
