using Emerald.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Threading.Tasks;

namespace Emerald.Application.Infrastructure.ActionFilter
{
    public class QuestSyncMiddleware
    {
        private readonly RequestDelegate next;

        public QuestSyncMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<User> userManager)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                context.Response.OnStarting(async () =>
                {
                    User user = await userManager.GetUserAsync(context.User);

                    if (context.Request.Method == HttpMethod.Post.Method)
                    {
                        user.GenerateNewSyncToken();
                        await userManager.UpdateAsync(user);
                    }

                    context.Response.Headers.Add("Sync-Token", user.SyncToken);
                });

                User user = await userManager.GetUserAsync(context.User);
                await userManager.UpdateAsync(user);

                if (context.Request.Headers.TryGetValue("Sync-Token", out var syncToken) &&
                    syncToken.Count > 0 &&
                    user.SyncToken != syncToken[0])
                {
                    context.Response.StatusCode = 250;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = "Got invalid sync token"
                    });

                    return;
                }

            }

            await next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class QuestSyncMiddlewareExtensions
    {
        public static IApplicationBuilder UseQuestSyncMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<QuestSyncMiddleware>();
        }
    }
}
