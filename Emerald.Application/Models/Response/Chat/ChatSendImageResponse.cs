using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Chat
{
    public class ChatSendImageResponse
    {
        public string ImageId { get; set; }

        public ChatSendImageResponse(string imageId)
        {
            ImageId = imageId;
        }
    }
}
