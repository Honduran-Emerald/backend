using Emerald.Application.Models;
using Emerald.Domain.Models;
using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
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
        private IQuestPrototypeRepository questPrototypeRepository;
        private IQuestRepository questRepository;
        private UserManager<User> userManager;

        public TestController(IQuestPrototypeRepository questPrototypeRepository, IQuestRepository questRepository, UserManager<User> userManager)
        {
            this.questPrototypeRepository = questPrototypeRepository;
            this.questRepository = questRepository;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost("init")]
        public async Task<IActionResult> Init(
            [FromQuery] LocationModel location,
            [FromQuery] string title,
            [FromQuery] string description,
            [FromQuery] List<string> tags,
            [FromQuery] string imageId)
        {
            User user = await userManager.GetUserAsync(User);
            QuestPrototype questPrototype = new QuestPrototype(
                title,
                description,
                tags,
                new Location(
                    location.Longitude,
                    location.Latitude),
                imageId);

            await questPrototypeRepository.Add(questPrototype);

            Domain.Models.QuestAggregate.Quest quest = new Domain.Models.QuestAggregate.Quest(
                user,
                questPrototype);

            await questRepository.Add(quest);
            /*
            Domain.Models.QuestAggregate.Quest quest = new Domain.Models.QuestAggregate.Quest(user);
            quest.AddQuestVersion(new Domain.Models.QuestVersionAggregate.QuestVersion(
                new Domain.Models.Location(
                    location.Longitude,
                    location.Latitude),
                title,
                description,
                imageName,
                1));
            await questRepository.Add(quest);
            */

            return Ok(quest.Id);
        }
    }
}
