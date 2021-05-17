using Emerald.Application.Models;
using Emerald.Domain.Models.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class UserModelFactory
    {
        public async Task<UserModel> Create(User user)
        {
            return new UserModel
            {
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                Image = user.Image,
                Level = user.GetLevel(),
                Experience = user.Experience,
                Glory = user.Glory,
                QuestCount = user.QuestIds.Count,
                TrackerCount = user.TrackerIds.Count
            };
        }
    }
}
