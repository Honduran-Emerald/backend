using Emerald.Infrastructure.Services;
using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public class DevelopmentSafeSearchService : ISafeSearchService
    {
        public Task<bool> Detect(string imageId)
        {
            return Task.FromResult(false);
        }

        public Task<bool> Detect(Image image)
        {
            return Task.FromResult(false);
        }
    }
}
