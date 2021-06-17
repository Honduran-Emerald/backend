using Emerald.Domain.Models.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public interface IMessagingService
    {
        public async Task Send(User user, string title, string body, string? imageId = null, Object? @object = null)
        {
            if (user.MessagingToken != null)
            {
                await Send(user.MessagingToken, title, body, imageId, @object);
            }
        }

        Task Send(string token, string title, string body, string? imageId = null, Object? @object = null);
    }
}
