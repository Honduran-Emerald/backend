using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Request.Chat
{
    public class ChatSendImageRequest
    {
        public ObjectId UserId { get; set; }
        public string BinaryImage { get; set; }

        public ChatSendImageRequest()
        {
            UserId = default!;
            BinaryImage = default!;
        }
    }
}
