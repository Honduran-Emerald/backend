using Emerald.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Infrastructure.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserTokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserTokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, UserManager<User> userManager)
        {
            if (httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
            {
                if (await userManager.GetUserAsync(httpContext.User) == null)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsJsonAsync(new
                    {
                        Message = "Unable to find user"
                    });

                    return;
                }
            }

            await _next(httpContext);
        }
    }

    public static class UserTokenValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserTokenValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserTokenValidationMiddleware>();
        }
    }
}
