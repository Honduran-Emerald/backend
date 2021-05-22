namespace Emerald.Domain.Models.ComponentAggregate
{
    public class TextComponent : Component
    {
        public string Text { get; set; }

        public TextComponent(string text)
        {
            Text = text;
        }

        private TextComponent()
        {
            Text = "";
        }
    }
}
