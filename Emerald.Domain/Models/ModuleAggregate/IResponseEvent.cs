using MediatR;
using MongoDB.Bson;

namespace Emerald.Domain.Models
{
    public interface IResponseEvent
    {
        INotification ToDomainEvent(ObjectId userId, ObjectId trackerId);
    }
}
