using Emerald.Infrastructure;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vitamin.Value.Domain;

namespace Emerald.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private IHttpContextAccessor httpContextAccessor;
        private IGridFSBucket imageBucket;

        public ImageService(IHttpContextAccessor httpContextAccessor, IMongoDbContext dbContext)
        {
            this.httpContextAccessor = httpContextAccessor;

            imageBucket = new GridFSBucket(dbContext.Emerald, new GridFSBucketOptions
            {
                BucketName = "Images"
            });

        }

        public string ImageToUrl(string imageId)
        {
            var request = httpContextAccessor.HttpContext!.Request;
            return $"{request.Scheme}://{request.Host}image/{imageId}";
        }

        public async Task<Stream> Download(string imageId)
        {
            return await imageBucket.OpenDownloadStreamByNameAsync(imageId);
        }

        public async Task<string> Upload(Stream stream)
        {
            string imageId = Utility.RandomString(16);
            await imageBucket.UploadFromStreamAsync(imageId, stream);
            return imageId;
        }

        public async Task Delete(string imageId)
        {
            var file = await imageBucket
                .Find(Builders<GridFSFileInfo>.Filter.Eq(f => f.Filename, imageId))
                .FirstAsync();
            await imageBucket.DeleteAsync(file.Id);
        }
    }
}
