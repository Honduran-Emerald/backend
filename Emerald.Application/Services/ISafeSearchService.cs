using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    /// <summary>
    /// Detect explicit content with google cloud vision api
    /// </summary>
    public interface ISafeSearchService
    {
        Task<bool> Detect(string imageId);
        Task<bool> Detect(Image image);
    }
}
