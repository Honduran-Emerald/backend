using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Emerald.Application.Models.Request.Quest
{
    public class QuestCreateCreateRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageId { get; set; }

        [Required]
        public LocationModel Location { get; set; }

        [Required]
        public string LocationName { get; set; }

        [Required]
        public List<string> Tags { get; set; }

        public QuestCreateCreateRequest()
        {
            Title = default!;
            Description = default!;
            ImageId = default!;
            Location = default!;
            LocationName = default!;
            Tags = default!;
        }
    }
}
