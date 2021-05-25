using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.QuestPrototypeAggregate
{
    public interface IPrototypeContext
    {
        ObjectId ConvertModuleId(int moduleId);
    }
}
