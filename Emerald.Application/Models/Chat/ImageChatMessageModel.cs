using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Chat
{
    public class ImageChatMessageModel : ChatMessageModel
    {
        public string ImageId { get; set; }

        public ImageChatMessageModel(ObjectId userId, string username, string? userImageId, string imageId)
            : base(userId, username, userImageId)
        {
            ImageId = imageId;
        }
    }
}
