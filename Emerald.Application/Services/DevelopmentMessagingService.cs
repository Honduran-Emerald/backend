using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public class DevelopmentMessagingService : IMessagingService
    {
        public Task Send(string token, string title, string body, string? imageId = null, object? @object = null)
        {
            return Task.CompletedTask;
        }
    }
}
