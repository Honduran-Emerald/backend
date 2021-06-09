using Emerald.Domain.Models.QuestPrototypeAggregate;
using Emerald.Domain.Models.QuestPrototypeAggregate.Components;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Emerald.Application.Models.Quest.Component
{
    public enum ComponentType
    {
        Text,
        Image,
        Answer
    }

    public static class JsonSerializerSettingsExtension
    {
        public static void AddComponentPrototypeConverter(this JsonSerializerSettings serializer)
        {
            serializer.Converters.Add(JsonSubtypesConverterBuilder
                .Of<ComponentPrototype>("type")
                .RegisterSubtype<TextComponentPrototype>(ComponentType.Text)
                .RegisterSubtype<AnswerComponentPrototype>(ComponentType.Answer)
                .RegisterSubtype<ImageComponentPrototype>(ComponentType.Image)
                .SerializeDiscriminatorProperty()
                .Build());
        }
    }
}
