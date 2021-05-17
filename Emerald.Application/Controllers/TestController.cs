using Emerald.Application.Models;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IQuestRepository questRepository;
        private UserManager<User> userManager;

        public TestController(IQuestRepository questRepository, UserManager<User> userManager)
        {
            this.questRepository = questRepository;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost("init")]
        public async Task<IActionResult> Init(
            [FromQuery] LocationModel location,
            [FromQuery] string title,
            [FromQuery] string description)
        {
            User user = await userManager.GetUserAsync(User);
            Domain.Models.QuestAggregate.Quest quest = new Domain.Models.QuestAggregate.Quest(user);
            quest.AddQuestVersion(new Domain.Models.QuestVersionAggregate.QuestVersion(
                quest,
                new Domain.Models.Location(
                    location.Longitude,
                    location.Latitude),
                title,
                description,
                1));
            await questRepository.Add(quest);

            return Ok();
        }
    }
}
