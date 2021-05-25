namespace Emerald.Application.Models.Quest.Events
{
    public abstract class ResponseEventModel
    {
        public ResponseEventType Type { get; set; }

        public ResponseEventModel(ResponseEventType type)
        {
            Type = type;
        }

        public ResponseEventModel()
        {
            Type = default!;
        }
    }
}
