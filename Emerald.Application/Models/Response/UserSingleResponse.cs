namespace Emerald.Application.Models.Response
{
    public class UserSingleResponse
    {
        public UserModel User { get; set; }

        public UserSingleResponse(UserModel user)
        {
            User = user;
        }

        private UserSingleResponse()
        {
            User = default!;
        }
    }
}
