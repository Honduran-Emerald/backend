using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (DomainException domainException)
            {
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
