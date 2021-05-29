using Emerald.Application.Models.Quest.RequestEvent;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Emerald.Application.Models.Quest.Events
{
    public class ChoiceRequestEventModel : RequestEventModel
    {
        [Required]
        public int Choice { get; set; }

        private ChoiceRequestEventModel()
        {
            Choice = default!;
        }
    }
}
