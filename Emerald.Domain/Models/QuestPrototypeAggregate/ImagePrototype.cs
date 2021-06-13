using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.QuestPrototypeAggregate
{
    public class ImagePrototype
    {
        public int Reference { get; set; }
        public string ImageId { get; set; }

        public ImagePrototype(int reference, string imageId)
        {
            Reference = reference;
            ImageId = imageId;
        }

        public ImagePrototype()
        {
            Reference = default!;
            ImageId = default!;
        }
    }
}
