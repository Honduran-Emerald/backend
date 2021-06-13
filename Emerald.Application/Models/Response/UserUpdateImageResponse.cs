using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response
{
    public class UserUpdateImageResponse
    {
        public string ImageId { get; set; }

        public UserUpdateImageResponse(string imageId)
        {
            ImageId = imageId;
        }
    }
}
