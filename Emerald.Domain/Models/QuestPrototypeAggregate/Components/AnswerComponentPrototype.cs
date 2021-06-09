using Emerald.Domain.Models.ComponentAggregate;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Components
{
    public class AnswerComponentPrototype : ComponentPrototype
    {
        public string Text { get; set; }

        private AnswerComponentPrototype()
        {
            Text = default!;
        }

        public override Component ConvertToComponent()
            => new AnswerComponent(Text);

        public override void Verify()
        {
            if (Text.Length == 0)
                throw new DomainException("AnswerComponent text can not be empty");
        }
    }
}
