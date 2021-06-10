using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public interface IImageService
    {
        string ImageToUrl(string imageId);
        Task<Stream> Download(string imageId);
        Task<string> Upload(Stream stream);
    }
}
