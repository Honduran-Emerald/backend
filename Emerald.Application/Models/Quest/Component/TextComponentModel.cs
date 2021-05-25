namespace Emerald.Application.Models.Quest.Component
{
    public class TextComponentModel : ComponentModel
    {
        public string Text { get; set; }

        public TextComponentModel(string componentId, string text) : base(componentId, ComponentType.Text)
        {
            Text = text;
        }
        
        private TextComponentModel()
        {
            Text = default!;
        }
    }
}
