using Emerald.Application.Models;
using Emerald.Domain.Models.UserAggregate;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class UserModelFactory : IModelFactory<User, UserModel>
    {
        private IUserService userService;

        public UserModelFactory(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<UserModel> Create(User user)
        {
            var currentUser = await userService.CurrentUser();

            return new UserModel(
                userId: user.Id.ToString(),
                userName: user.UserName,
                image: user.ImageId,
                level: user.GetLevel(),
                experience: user.Experience,
                glory: user.Glory,
                questCount: user.QuestIds.Count,
                trackerCount: user.TrackerIds.Count,
                user.Followers.Count,
                currentUser.Id == user.Id
                ? false : currentUser.Following.Contains(user.Id),
                currentUser.Id == user.Id
                ? false : currentUser.Followers.Contains(user.Id));
        }
    }
}
