namespace Emerald.Application.Models.Quest.Component
{
    public class TextComponentModel : ComponentModel
    {
        public string Text { get; set; }

        public TextComponentModel(string text) : base(text, ComponentType.Text)
        {
            Text = text;
        }
    }
}
