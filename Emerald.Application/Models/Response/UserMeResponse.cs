namespace Emerald.Application.Models.Response
{
    public class UserMeResponse
    {
        public UserModel User { get; set; }

        public UserMeResponse(UserModel user)
        {
            User = user;
        }

        private UserMeResponse()
        {
            User = default!;
        }
    }
}
