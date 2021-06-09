using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Modules
{
    public class StoryModulePrototype : ModulePrototype
    {
        public int? NextModuleId { get; set; }

        private StoryModulePrototype()
        {
            NextModuleId = default!;
        }

        public override Module ConvertToModule(IPrototypeContext context)
            => new StoryModule(context.ConvertModuleId(Id), Objective, context.ConvertModuleId(NextModuleId!.Value));

        public override void Verify(IPrototypeContext context)
        {
            if (NextModuleId == null)
            {
                throw new DomainException($"({Id}) NextModuleId can not be null");
            }

            if (context.ContainsModuleId(NextModuleId.Value) == false)
            {
                throw new DomainException($"({Id}) NextModuleId in StoryModule not found got {NextModuleId.Value}");
            }
        }
    }
}
