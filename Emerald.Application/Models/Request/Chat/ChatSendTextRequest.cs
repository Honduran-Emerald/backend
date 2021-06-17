using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Request.Chat
{
    public class ChatSendTextRequest
    {
        public ObjectId UserId { get; set; }

        [Required]
        [MinLength(1)]
        public string Text { get; set; }

        public ChatSendTextRequest()
        {
            UserId = default!;
            Text = default!;
        }
    }
}
