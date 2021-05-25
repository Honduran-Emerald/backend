namespace Emerald.Application.Models.Response
{
    public class AuthenticationTokenResponse
    {
        public string Token { get; set; }

        public AuthenticationTokenResponse(string token)
        {
            Token = token;
        }

        public AuthenticationTokenResponse()
        {
            Token = default!;
        }
    }
}
