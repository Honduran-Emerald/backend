using Emerald.Infrastructure.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Services
{
    public class ImageIndexService : IImageIndexService
    {
        private IMongoCollection<ImageIndexModel> collection;
        private IImageService imageService;

        public ImageIndexService(IMongoDbContext dbContext, IImageService imageService)
        {
            collection = dbContext.Emerald.GetCollection<ImageIndexModel>("ImageIndex");
            this.imageService = imageService;
        }

        public async Task DecreaseImageReference(string imageId)
        {
            ImageIndexModel indexModel = await collection.Find(i => i.Id == imageId).FirstOrDefaultAsync();

            if (indexModel == null)
            {
                indexModel = new ImageIndexModel();
                indexModel.Id = imageId;
                await collection.InsertOneAsync(indexModel);
            }

            indexModel.DecreaseReferenceCount();

            if (indexModel.ReferenceCount == 0)
            {
                await imageService.Delete(imageId);
                await collection.DeleteOneAsync(i => i.Id == indexModel.Id);
            }
            else
            {
                await collection.ReplaceOneAsync(i => i.Id == indexModel.Id, indexModel);
            }
        }

        public async Task IncreaseImageReference(string imageId)
        {
            ImageIndexModel indexModel = await collection.Find(i => i.Id == imageId).FirstOrDefaultAsync();

            if (indexModel == null)
            {
                indexModel = new ImageIndexModel();
                indexModel.Id = imageId;
                await collection.InsertOneAsync(indexModel);
            }

            indexModel.IncreaseReferenceCount();
            await collection.ReplaceOneAsync(i => i.Id == indexModel.Id, indexModel);
        }
    }
}
