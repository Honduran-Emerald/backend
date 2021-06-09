using Emerald.Application.Models.Quest;
using Emerald.Domain.Models;
using MongoDB.Bson;

namespace Emerald.Application.Models.Prototype
{
    public class QuestPrototypeModel
    {
        public ObjectId QuestId { get; set; }

        public QuestModel? Quest { get; set; }

        public string? Title { get; set; }
        public string? ImageId { get; set; }
        public string? Description { get; set; }

        public Location? Location { get; set; }
        public string? LocationName { get; set; }

        public string? AgentProfileName { get; set; }
        public string? AgentProfileImageId { get; set; }

        public bool Released { get; set; }
        public bool Public { get; set; }
        public bool Outdated { get; set; }

        public QuestPrototypeModel(ObjectId questId, QuestModel? quest, string? title, string? imageId, string? description, Location? location, string? locationName, string? agentProfileName, string? agentProfileImageId, bool released, bool @public, bool outdated)
        {
            QuestId = questId;
            Quest = quest;
            Title = title;
            ImageId = imageId;
            Description = description;
            Location = location;
            LocationName = locationName;
            AgentProfileName = agentProfileName;
            AgentProfileImageId = agentProfileImageId;
            Released = released;
            Public = @public;
            Outdated = outdated;
        }
    }
}
