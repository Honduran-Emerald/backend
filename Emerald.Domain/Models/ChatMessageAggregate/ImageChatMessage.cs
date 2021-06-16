using Emerald.Domain.Models.ChatAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.ChatMessageAggregate
{
    public class ImageChatMessage : ChatMessage
    {
        public string ImageId { get; set; }

        public ImageChatMessage(ObjectId senderId, ObjectId receiverId, string imageId)
            : base(senderId, receiverId)
        {
            ImageId = imageId;
        }
    }
}
