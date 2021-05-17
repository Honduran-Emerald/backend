﻿using Emerald.Domain.Events;
using Emerald.Infrastructure.ViewModelHandlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ViewModels.EventHandler
{
    public class QuestFinishDomainEventHandler : INotificationHandler<QuestFinishDomainEvent>
    {
        private QuestViewModelStash stash;

        public QuestFinishDomainEventHandler(QuestViewModelStash stash)
        {
            this.stash = stash;
        }

        public async Task Handle(QuestFinishDomainEvent notification, CancellationToken cancellationToken)
        {
            await stash.IncreaseFinish(notification.QuestId);
        }
    }
}