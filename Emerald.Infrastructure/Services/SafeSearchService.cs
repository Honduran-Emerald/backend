using Google.Api.Gax;
using Google.Api.Gax.Grpc;
using Google.Cloud.Vision.V1;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Services
{
    public class SafeSearchService : ISafeSearchService
    {
        private ImageAnnotatorClient client;
        private IImageService imageService;

        public SafeSearchService(IConfiguration configuration, IImageService imageService)
        {
            this.imageService = imageService;
            var builder = new ImageAnnotatorClientBuilder();
            client = builder.Build();
        }

        public async Task<bool> Detect(string imageId)
        {
            return await Detect(await Image.FromStreamAsync(await imageService.Download(imageId)));
        }

        public async Task<bool> Detect(Image image)
        {
            SafeSearchAnnotation safeSearchAnnotation = await client.DetectSafeSearchAsync(
                image, null,
                CallSettings.FromRetry(
                    RetrySettings.FromConstantBackoff(5, TimeSpan.FromSeconds(1),
                    exception => false)));

            return safeSearchAnnotation.Adult == Likelihood.Likely
                || safeSearchAnnotation.Adult == Likelihood.VeryLikely
                || safeSearchAnnotation.Medical == Likelihood.Likely
                || safeSearchAnnotation.Medical == Likelihood.VeryLikely
                || safeSearchAnnotation.Violence == Likelihood.Likely
                || safeSearchAnnotation.Violence == Likelihood.VeryLikely;
        }
    }
}
