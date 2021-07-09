using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.RequestEvent
{
    public class TextRequestEventModel : RequestEventModel
    {
        [Required]
        public string Text { get; set; }

        public TextRequestEventModel()
        {
            Text = default!;
        }
    }
}
