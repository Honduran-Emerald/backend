using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.ChatAggregate
{
    public class Chat : Entity
    {
        public ObjectId UserSenderId { get; set; }
        public ObjectId UserReceiverId { get; set; }

        public DateTime LastTimeReceived { get; set; }

        public Chat(ObjectId userSenderId, ObjectId userReceiverId)
        {
            UserSenderId = userSenderId;
            UserReceiverId = userReceiverId;
            LastTimeReceived = DateTime.UtcNow;
        }

        public void UpdateLastTimeRead()
        {
            LastTimeReceived = DateTime.UtcNow;
        }
    }
}
