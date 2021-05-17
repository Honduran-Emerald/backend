namespace Emerald.Application.Models.Quest.Events
{
    public class ResponseEventModel
    {
        public ResponseEventType Type { get; set; }

        public ResponseEventModel(ResponseEventType type)
        {
            Type = type;
        }
    }
}
