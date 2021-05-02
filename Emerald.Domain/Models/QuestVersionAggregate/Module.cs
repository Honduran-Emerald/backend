﻿using Emerald.Domain.Models.ComponentAggregate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestVersionAggregate
{
    public class Module : Entity
    {
        public override ObjectId Id { get; protected set; }

        public QuestVersion QuestVersion { get; private set; }
        public List<ObjectId> ComponentIds { get; private set; }

        public Module(QuestVersion questVersion)
        {
            QuestVersion = questVersion;
        }

        private Module()
        {
        }
    }
}
