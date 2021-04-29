using AspNetCore.Identity.Mongo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Domain.Models.UserAggregate
{
    public class User : MongoUser
    {
        public User()
        {
        }

        public User(string userName) : base(userName)
        {
        }
    }
}
