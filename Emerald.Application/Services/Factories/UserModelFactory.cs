using Emerald.Application.Models;
using Emerald.Domain.Models.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class UserModelFactory : IModelFactory<User, UserModel>
    {
        public async Task<UserModel> Create(User user)
        {
            return new UserModel(
                userId: user.Id.ToString(),
                userName: user.UserName,
                image: user.Image,
                level: user.GetLevel(),
                experience: user.Experience,
                glory: user.Glory,
                questCount: user.QuestIds.Count,
                trackerCount: user.TrackerIds.Count);
        }
    }
}
