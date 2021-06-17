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

        public ImageChatMessageModel(DateTime creationTime, bool received, string imageId)
            : base(creationTime, received, ChatType.Image)
        {
            ImageId = imageId;
        }
    }
}
