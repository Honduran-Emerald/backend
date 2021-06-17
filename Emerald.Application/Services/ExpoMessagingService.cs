using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Expo.Server.Client;
using Expo.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public class ExpoMessagingService : IMessagingService
    {
        private PushApiClient client;
        private IUserRepository userRepository;
        private IUserService userService;

        public ExpoMessagingService(PushApiClient client, IUserRepository userRepository, IUserService userService)
        {
            this.client = client;
            this.userRepository = userRepository;
            this.userService = userService;
        }

        public async Task Send(string token, string title, string body, string? imageId = null, object? @object = null)
        {
            PushTicketRequest request = new PushTicketRequest
            {
                PushTo = new List<string> { token },
                PushBadgeCount = 1,
                PushTitle = title,
                PushBody = body,
                PushData = @object
            };
            
            var result = await client.PushSendAsync(request);

            if (result?.PushTicketErrors?.Count() > 0)
            {
                User user = await userService.CurrentUser();
                user.MessagingToken = null;
                await userRepository.Update(user);
            }
        }
    }
}
