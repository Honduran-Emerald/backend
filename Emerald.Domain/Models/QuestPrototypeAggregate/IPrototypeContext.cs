using MongoDB.Bson;

namespace Emerald.Domain.Models.QuestPrototypeAggregate
{
    public interface IPrototypeContext
    {
        string ConvertImageId(int reference);
        ObjectId ConvertModuleId(int moduleId);
    }
}
