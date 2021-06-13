using Emerald.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Infrastructure.Models
{
    public class ImageIndexModel
    {
        public string Id { get; set; }
        public int ReferenceCount { get; set; }

        public ImageIndexModel(string id)
        {
            Id = id;
            ReferenceCount = 0;
        }

        public ImageIndexModel()
        {
            Id = default!;
        }

        public void IncreaseReferenceCount()
        {
            ++ReferenceCount;
        }

        public void DecreaseReferenceCount()
        {
            --ReferenceCount;
        }
    }
}
