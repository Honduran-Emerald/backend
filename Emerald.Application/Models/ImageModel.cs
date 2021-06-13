using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models
{
    public class ImageModel
    {
        public int Reference { get; set; }
        public string Image { get; set; }

        public ImageModel()
        {
            Reference = default!;
            Image = default!;
        }
    }
}
