namespace Emerald.Application.Models.Quest.Events
{
    public class ResponseEventModel
    {
        ResponseEventType Type { get; set; }


        public ResponseEventModel(ResponseEventType type)
        {
            Type = type;
        }
    }
}
