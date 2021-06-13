using Emerald.Domain.Models.QuestVersionAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Events
{
    public class QuestVersionAddedDomainEvent : INotification
    {
        public QuestVersion QuestVersion { get; set; }

        public QuestVersionAddedDomainEvent(QuestVersion questVersion)
        {
            QuestVersion = questVersion;
        }
    }
}
