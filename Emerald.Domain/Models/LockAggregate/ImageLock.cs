using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.LockAggregate
{
    public class ImageLock : Lock
    {
        public string ImageId { get; set; }

        public ImageLock(string imageId, string reason)
            : base(reason)
        {
            ImageId = imageId;
        }
    }
}
