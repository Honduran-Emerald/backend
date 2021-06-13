using Emerald.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Emerald.Application.Infrastructure.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, UserManager<User> userManager)
        {
            if (httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
            {
                User user = await userManager.GetUserAsync(httpContext.User);

                if (user == null)
                {
                    httpContext.Response.StatusCode = 401;

                    await httpContext.Response.WriteAsJsonAsync(new
                    {
                        Message = "Unable to find user"
                    });

                    return;
                }

                if (user.Locks.Count > 0)
                {
                    httpContext.Response.StatusCode = 251;

                    await httpContext.Response.WriteAsJsonAsync(new
                    {
                        Message = "User is locked"
                    });

                    return;
                }
            }

            await _next(httpContext);
        }
    }

    public static class UserValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserValidationMiddleware>();
        }
    }
}
