using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Emerald.Infrastructure.Services;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public class FirebaseMessagingService : IMessagingService
    {
        private FirebaseMessaging firebaseMessaging;
        private IImageService imageService;
        private IUserRepository userRepository;

        public FirebaseMessagingService(IImageService imageService, IUserRepository userRepository)
        {
            var app = FirebaseApp.Create();
            firebaseMessaging = FirebaseMessaging.GetMessaging(app);

            this.imageService = imageService;
            this.userRepository = userRepository;
        }
        
        public async Task Send(string token, string title, string body, string? imageId = null, Object? @object = null)
        {
            await firebaseMessaging.SendAsync(
                new Message
                {
                    Token = token,
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body,
                        ImageUrl = imageId == null 
                            ? null : imageService.ImageToUrl(imageId),
                    },
                    Data = @object == null 
                        ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(
                            JsonConvert.SerializeObject(@object))
                });
        }
    }
}
