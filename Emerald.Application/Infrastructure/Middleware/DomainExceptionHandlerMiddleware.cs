using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Application.Infrastructure.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class DomainExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public DomainExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<DomainExceptionHandlerMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (DomainException domainException)
            {
                if (httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
                {
                    logger.LogInformation(domainException.StackTrace);
                    logger.LogWarning(domainException, $"User '{httpContext.User.Identity.Name}' caused DomainException '{domainException.Message}'");
                }
                else
                {
                    logger.LogWarning($"Anonymous caused DomainException '{domainException.Message}'");
                }

                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Message = domainException.Message
                });
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DomainExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseDomainExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DomainExceptionHandlerMiddleware>();
        }
    }
}
