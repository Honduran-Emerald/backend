namespace Emerald.Application.Models.Quest.Component
{
    public class AnswerComponentModel : ComponentModel
    {
        public string Text { get; set; }

        public AnswerComponentModel()
        {
            Text = default!;
        }

        public AnswerComponentModel(string componentId, string text) : base(componentId, ComponentType.Answer)
        {
            Text = text;
        }
    }
}
