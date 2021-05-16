using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Events
{
    public class ChoiceRequestEventModel
    {
        [Required]
        public int Choice { get; set; }
    }
}
