using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IComponentRepository componentRepository;

        public TestController(IComponentRepository componentRepository)
        {
            this.componentRepository = componentRepository;
        }

        [HttpPost("addtextcomponent")]
        public async Task<IActionResult> AddTextComponent(string text)
        {
            await componentRepository.Add(new TextComponent(text));
            return Ok();
        }

        [HttpGet("allcomponents")]
        public async Task<IActionResult> AllComponents()
        {
            return Ok(await componentRepository.GetAll());
        }

        [Authorize]
        [HttpPost("addtestquest")]
        public async Task<IActionResult> AddTestQuest(
            [FromServices] UserManager<User> userManager,
            [FromServices] IQuestRepository questRepository,
            [FromServices] IQuestVersionRepository questVersionRepository)
        {
            /*
            User user = await userManager.GetUserAsync(User);

            Quest quest = new Quest(user);
            QuestVersion questVersion = new QuestVersion(quest, "Title", "Description", 1);
            questVersion.AddModule(new Module());

            quest.AddQuestVersion(questVersion);

            await questRepository.Add(quest);
            await questVersionRepository.Add(questVersion);
            
            return Ok(new 
            { 
                quest = quest, 
                questVersion = questVersion 
            });
            */

            return Ok();
        }
    }
}
