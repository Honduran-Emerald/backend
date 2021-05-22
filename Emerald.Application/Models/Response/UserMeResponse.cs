using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response
{
    public class UserMeResponse
    {
        public UserModel User { get; set; }

        public UserMeResponse(UserModel user)
        {
            User = user;
        }
    }
}
