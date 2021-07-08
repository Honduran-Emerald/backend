using Emerald.Domain.Models.QuestPrototypeAggregate;
using Emerald.Domain.Models.QuestPrototypeAggregate.Modules;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Emerald.Application.Models.Quest.Module
{
    public enum ModuleType
    {
        Choice,
        Location,
        Story,
        Ending,
        Passphrase,
        QrCode,
        Random,
        WideArea
    }

    public static class JsonSerializerSettingsExtension
    {
        public static void AddModulePrototypeConverter(this JsonSerializerSettings serializer)
        {
            serializer.Converters.Add(JsonSubtypesConverterBuilder
                .Of<ModulePrototype>("type")
                .RegisterSubtype<ChoiceModulePrototype>(ModuleType.Choice)
                .RegisterSubtype<StoryModulePrototype>(ModuleType.Story)
                .RegisterSubtype<EndingModulePrototype>(ModuleType.Ending)
                .RegisterSubtype<LocationModulePrototype>(ModuleType.Location)
                .RegisterSubtype<PasspharseModulePrototype>(ModuleType.Passphrase)
                .RegisterSubtype<QrCodeModulePrototype>(ModuleType.QrCode)
                .RegisterSubtype<RandomModulePrototype>(ModuleType.Random)
                .RegisterSubtype<WideAreaModulePrototype>(ModuleType.WideArea)
                .SerializeDiscriminatorProperty()
                .Build());
        }
    }
}
