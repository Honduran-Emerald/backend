namespace Emerald.Domain.Models.ComponentAggregate
{
    public class AnswerComponent : Component
    {
        public string Text { get; set; }

        public AnswerComponent(string text)
        {
            Text = text;
        }

        private AnswerComponent()
        {
            Text = default!;
        }
    }
}
