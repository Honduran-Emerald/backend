namespace Emerald.Application.Models.Response.Quest
{
    public class QuestPlayEventResponse
    {
        public ResponseEventCollectionModel ResponseEventCollection { get; set; }

        public QuestPlayEventResponse(ResponseEventCollectionModel responseEventCollection)
        {
            ResponseEventCollection = responseEventCollection;
        }

        public QuestPlayEventResponse()
        {
            ResponseEventCollection = default!;
        }
    }
}
