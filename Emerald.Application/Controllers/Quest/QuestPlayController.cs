﻿using Emerald.Application.Models.Response;
using Emerald.Application.Models.Quest.Events;
using Emerald.Application.Services;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.RequestEvents;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;
using Emerald.Application.Models.Response.Quest;

namespace Emerald.Application.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/play")]
    public class QuestPlayController : ControllerBase
    {
        private ResponseEventModelFactory responseEventFactory;
        private QuestPlayService questPlayService;
        private UserManager<User> userManager;

        public QuestPlayController(ResponseEventModelFactory responseEventFactory, QuestPlayService questPlayService, UserManager<User> userManager)
        {
            this.responseEventFactory = responseEventFactory;
            this.questPlayService = questPlayService;
            this.userManager = userManager;
        }

        [HttpPost("event/position")]
        public async Task<IActionResult> HandleEvent(PositionRequestEventModel positionRequest)
        {


            return Ok();
        }

        [HttpPost("event/choice")]
        public async Task<ActionResult<QuestPlayEventResponse>> HandleEvent(
            [FromBody] ChoiceRequestEventModel choiceEvent)
        {
            try
            {
                ResponseEventCollection responseEvent = await questPlayService.HandleEvent(
                    await userManager.GetUserAsync(User),
                    new ChoiceRequestEvent(choiceEvent.Choice));

                return Ok(new QuestPlayEventResponse
                {
                    ResponseEventCollection = await responseEventFactory.Create(responseEvent)
                });
            }
            catch (DomainException e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }
    }
}
