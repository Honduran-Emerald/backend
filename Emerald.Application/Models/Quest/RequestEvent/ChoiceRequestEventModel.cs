using System.ComponentModel.DataAnnotations;

namespace Emerald.Application.Models.Quest.Events
{
    public class ChoiceRequestEventModel
    {
        [Required]
        public int Choice { get; set; }
    }
}
