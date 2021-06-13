using Emerald.Domain.SeedWork;
using MediatR;
using System.Threading.Tasks;

namespace Emerald.Infrastructure
{
    public static class MediatorExtensions
    {
        public static async Task PublishEntity<T>(this IMediator mediator, IEntity<T> entity)
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
