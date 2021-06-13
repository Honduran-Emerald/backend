using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestPrototypeAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Exceptions;
using Emerald.Infrastructure.Services;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class QuestPrototypeRepository : IQuestPrototypeRepository
    {
        private IMongoCollection<QuestPrototype> collection;
        private IMediator mediator;
        private IImageIndexService imageIndexService;

        public QuestPrototypeRepository(IMongoDbContext dbContext, IMediator mediator, IImageIndexService imageIndexService)
        {
            collection = dbContext.Emerald.GetCollection<QuestPrototype>("QuestPrototypes");
            this.mediator = mediator;
            this.imageIndexService = imageIndexService;
        }

        public async Task Add(QuestPrototype questPrototype)
        {
            foreach (ImagePrototype image in questPrototype.Images)
            {
                await imageIndexService.IncreaseImageReference(image.ImageId);
            }

            await collection.InsertOneAsync(questPrototype);
            await mediator.PublishEntity(questPrototype);
        }

        public async Task<QuestPrototype> Get(ObjectId id)
        {
            var module = await collection.Find(o => o.Id == id)
                .FirstOrDefaultAsync();

            if (module == null)
            {
                throw new MissingElementException($"QuestPrototype '{id}' not found");
            }

            return module;
        }

        public async Task Update(QuestPrototype questPrototype)
        {
            var images = await collection
                .Find(q => q.Id == questPrototype.Id)
                .Project(q => q.Images)
                .FirstAsync();

            if (images != null)
            {
                foreach (ImagePrototype newImage in questPrototype.Images
                    .Where(i1 => images.Any(i2 => i1.ImageId == i2.ImageId) == false))
                {
                    await imageIndexService.IncreaseImageReference(newImage.ImageId);
                }

                foreach (ImagePrototype oldImage in images
                    .Where(i1 => questPrototype.Images.Any(i2 => i1.ImageId == i2.ImageId) == false))
                {
                    await imageIndexService.DecreaseImageReference(oldImage.ImageId);
                }
            }

            await collection.ReplaceOneAsync(f => f.Id == questPrototype.Id, questPrototype);
            await mediator.PublishEntity(questPrototype);
        }
    }
}
