using Emerald.Application.Models.Chat;
using Emerald.Domain.Models.ChatAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class ChatModelFactory : IModelFactory<Chat, ChatModel>
    {
        private IUserRepository userRepository;
        private IUserService userService;
        private IChatMessageRepository chatMessageRepository;

        public ChatModelFactory(IUserRepository userRepository, IUserService userService, IChatMessageRepository chatMessageRepository)
        {
            this.userRepository = userRepository;
            this.userService = userService;
            this.chatMessageRepository = chatMessageRepository;
        }

        public async Task<ChatModel> Create(Chat source)
        {
            User current = await userService.CurrentUser();
            User otherUser = await userRepository.Get(
                source.UserReceiverId == current.Id
                ? source.UserSenderId
                : source.UserReceiverId);

            ChatMessage? chatMessage = await chatMessageRepository.GetQueryable()
                .Where(c => c.ReceiverId == current.Id &&
                            c.SenderId == otherUser.Id

                         || c.SenderId == current.Id &&
                            c.ReceiverId == otherUser.Id)
                .OrderByDescending(c => c.CreationTime)
                .FirstOrDefaultAsync();

            return new ChatModel(
                otherUser.Id,
                otherUser.UserName,
                otherUser.ImageId,
                source.LastTimeReceived,
                chatMessage == null
                    ? DateTime.UtcNow : chatMessage.CreationTime,
                chatMessage == null
                    ? null : chatMessage.ToPreviewMessage());
        }
    }
}
