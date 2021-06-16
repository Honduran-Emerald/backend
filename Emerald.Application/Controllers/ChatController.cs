using Emerald.Application.Models.Response.Chat;
using Emerald.Application.Services;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.ChatAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [Route("chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery] ObjectId userId,
            [FromQuery] int offset,
            [FromServices] IConfiguration configuration,
            [FromServices] IUserService userService,
            [FromServices] IChatMessageRepository chatMessageRepository,
            [FromServices] ChatMessageModelFactory chatMessageModelFactory)
        {
            User user = await userService.CurrentUser();

            List<ChatMessage> chatMessages = await chatMessageRepository.GetQueryable()
                .Where(c => c.ReceiverId == userId || c.SenderId == userId)
                .Where(c => c.ReceiverId == user.Id || c.SenderId == user.Id)
                .OrderBy(c => c.CreationTime)
                .Skip(offset)
                .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                .ToListAsync();

            return Ok(new ChatGetResponse(
                await chatMessageModelFactory.Create(
                    chatMessages.Select(c => new ChatMessageForUser(c, c.ReceiverId == user.Id 
                        ? ChatMessageUserType.Sender 
                        : ChatMessageUserType.Receiver))
                                .ToList())));
        }

        [HttpGet("query")]
        public async Task<IActionResult> Query()
        {
            return Ok();
        }

        [HttpGet("send/text")]
        public async Task<IActionResult> SendText()
        {
            return Ok();
        }

        [HttpGet("send/image")]
        public async Task<IActionResult> SendImage()
        {
            return Ok();
        }
    }
}
