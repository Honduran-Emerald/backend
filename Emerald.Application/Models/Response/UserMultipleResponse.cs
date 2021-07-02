using Emerald.Domain.Models.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response
{
    public class UserMultipleResponse
    {
        public List<UserModel> Users { get; set; }

        public UserMultipleResponse(List<UserModel> users)
        {
            Users = users;
        }
    }
}
