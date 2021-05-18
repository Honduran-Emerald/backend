using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Infrastructure
{
    public static class MediatorExtensions
    {
        public static async Task PublishEntity(this IMediator mediator, Entity entity)
        {
            if (entity.DomainEvents != null)
            {
                foreach (var domainEvent in entity.DomainEvents)
                    await mediator.Publish(domainEvent);
            }
        }
    }
}
