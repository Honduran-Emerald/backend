using Emerald.Domain.Events;
using Emerald.Domain.Models.ModuleAggregate.ResponseEvents;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Emerald.Domain.EventHandler
{
    public class ExperienceResponseDomainEventHandler
        : INotificationHandler<QuestResponseDomainEvent<ExperienceResponseEvent>>
    {
        private IUserRepository userRepository;

        public ExperienceResponseDomainEventHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Handle(QuestResponseDomainEvent<ExperienceResponseEvent> notification, CancellationToken cancellationToken)
        {
            User user = await userRepository.Get(notification.UserId);
            user.AddExperience(notification.Event.Experience);
            await userRepository.Update(user);
        }
    }
}
