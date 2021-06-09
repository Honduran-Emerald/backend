using Emerald.Domain.SeedWork;
using MediatR;
using System.Threading.Tasks;

namespace Emerald.Infrastructure
{
    public static class MediatorExtensions
    {
        public static async Task PublishEntity(this IMediator mediator, IEntity entity)
        {
            if (entity.DomainEvents != null)
            {
                foreach (var domainEvent in entity.DomainEvents)
                    await mediator.Publish(domainEvent);

                entity.ClearEvents();
            }
        }
    }
}
