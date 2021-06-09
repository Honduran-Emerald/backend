using MongoDB.Bson;

namespace Emerald.Domain.Models.QuestPrototypeAggregate
{
    public interface IPrototypeContext
    {
        ObjectId ConvertModuleId(int moduleId);
        bool ContainsModuleId(int moduleId);
    }
}
