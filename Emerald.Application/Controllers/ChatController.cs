using Emerald.Application.Models.Chat;
using Emerald.Application.Models.Request.Chat;
using Emerald.Application.Models.Response.Chat;
using Emerald.Application.Services;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.ChatAggregate;
using Emerald.Domain.Models.ChatMessageAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using Emerald.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [Route("chat")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private IMessagingService messagingService;

        public ChatController(IMessagingService messagingService)
        {
            this.messagingService = messagingService;
        }

        /// <summary>
        /// Get a pack of messages between the authorized user and a another user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> Get(
            [FromQuery] ObjectId userId,
            [FromQuery] int offset,
            [FromServices] IConfiguration configuration,
            [FromServices] IUserService userService,
            [FromServices] IChatMessageRepository chatMessageRepository,
            [FromServices] IChatRepository chatRepository,
            [FromServices] ChatMessageModelFactory chatMessageModelFactory,
            [FromServices] ChatModelFactory chatModelFactory)
        {
            User user = await userService.CurrentUser();

            List<ChatMessage> chatMessages = await chatMessageRepository.GetQueryable()
                .Where(c => c.ReceiverId == userId || c.SenderId == userId)
                .Where(c => c.ReceiverId == user.Id || c.SenderId == user.Id)
                .OrderByDescending(c => c.CreationTime)
                .Skip(offset)
                .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                .ToListAsync();

            Chat currentUserChat = await chatRepository.EmplaceGet(user.Id, userId);
            currentUserChat.UpdateLastTimeRead();
            await chatRepository.Update(currentUserChat);

            Chat otherUserChat = await chatRepository.EmplaceGet(userId, user.Id);

            return Ok(new ChatGetResponse(
                await chatModelFactory.Create(otherUserChat),
                await chatMessageModelFactory.Create(chatMessages)));
        }

        /// <summary>
        /// Query information about all active chats
        /// </summary>
        /// <returns></returns>
        [HttpGet("query")]
        public async Task<IActionResult> Query(
            [FromServices] IChatRepository chatRepository,
            [FromServices] IUserService userService,
            [FromServices] ChatModelFactory chatModelFactory)
        {
            User user = await userService.CurrentUser();

            return Ok(new ChatQueryResponse(
                await chatModelFactory.Create(await chatRepository.GetQueryable()
                    .OrderByDescending(c => c.LastTimeReceived)
                    .Where(c => c.UserReceiverId == user.Id)
                    .ToListAsync())));
        }

        /// <summary>
        /// Send a text chat message to a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("send/text")]
        public async Task<ActionResult<ChatSendTextRequest>> SendText(
            [FromBody] ChatSendTextRequest request,
            [FromServices] IChatMessageRepository chatMessageRepository,
            [FromServices] IUserService userService,
            [FromServices] IUserRepository userRepository,
            [FromServices] ChatMessageModelFactory chatMessageModelFactory,
            [FromServices] IChatRepository chatRepository)
        {
            User sender = await userService.CurrentUser();
            User receiver = await userRepository.Get(request.UserId);

            TextChatMessage message = new TextChatMessage(
                sender.Id,
                request.UserId,
                request.Text);

            await chatMessageRepository.Add(message);

            await SendMessageNotification(
                await chatMessageModelFactory.Create(message),
                sender,
                receiver);

            await chatRepository.EmplaceGet(sender.Id, receiver.Id);
            await chatRepository.EmplaceGet(receiver.Id, sender.Id);

            return Ok();
        }

        /// <summary>
        /// Send a image chat message to a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("send/image")]
        public async Task<IActionResult> SendImage(
            [FromBody] ChatSendImageRequest request,
            [FromServices] IImageService imageService,
            [FromServices] ISafeSearchService safeSearchService,
            [FromServices] IChatMessageRepository chatMessageRepository,
            [FromServices] IUserService userService,
            [FromServices] IUserRepository userRepository,
            [FromServices] ChatMessageModelFactory chatMessageModelFactory,
            [FromServices] IChatRepository chatRepository)
        {
            User sender = await userService.CurrentUser();
            User receiver = await userRepository.Get(request.UserId);

            byte[] binary = Convert.FromBase64String(request.BinaryImage);
            string imageId = await imageService.Upload(new MemoryStream(binary));

            ImageChatMessage message = new ImageChatMessage(
                sender.Id,
                request.UserId,
                imageId);

            await chatMessageRepository.Add(message);

            await SendMessageNotification(
                await chatMessageModelFactory.Create(message),
                sender,
                receiver);

            await chatRepository.EmplaceGet(sender.Id, receiver.Id);
            await chatRepository.EmplaceGet(receiver.Id, sender.Id);

            return Ok(new ChatSendImageResponse(
                imageId));
        }

        private async Task SendMessageNotification(ChatMessageModel chatMessage, User sender, User receiver)
        {
            await messagingService.Send(
                receiver,
                $"New message from {sender.UserName}",
                chatMessage switch
                {
                    TextChatMessageModel textChatMessage => textChatMessage.Text.Length > 50
                    ? textChatMessage.Text.Substring(0, 50)
                    : textChatMessage.Text,
                    ImageChatMessageModel => "Image",
                    _ => throw new ArgumentException("Got invalid chatmessage type")
                },
                sender.ImageId,
                chatMessage);
        }
    }
}
