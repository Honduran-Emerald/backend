using Emerald.Application.Models.Chat;
using Emerald.Domain.Models.ChatAggregate;
using Emerald.Domain.Models.ChatMessageAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Application.Services.Factories
{
    public class ChatMessageModelFactory : IModelFactory<ChatMessageForUser, ChatMessageModel>
    {
        private IUserRepository userRepository;

        public ChatMessageModelFactory(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ChatMessageModel> Create(ChatMessageForUser source)
        {
            User user = await userRepository.Get(
                source.Type == ChatMessageUserType.Receiver
                ? source.ChatMessage.ReceiverId
                : source.ChatMessage.SenderId);

            switch (source.ChatMessage)
            {
                case TextChatMessage message:
                    return new TextChatMessageModel(
                            user.Id,
                            user.UserName,
                            user.ImageId,
                            message.Text);

                case ImageChatMessage message:
                    return new ImageChatMessageModel(
                            user.Id,
                            user.UserName,
                            user.ImageId,
                            message.ImageId);

                default:
                    throw new DomainException("Got invalid chatmessage tye");
            }
        }
    }
    public enum ChatMessageUserType
    {
        Sender,
        Receiver
    }

    public class ChatMessageForUser
    {
        public ChatMessage ChatMessage { get; set; }
        public ChatMessageUserType Type { get; set; }

        public ChatMessageForUser(ChatMessage chatMessage, ChatMessageUserType type)
        {
            ChatMessage = chatMessage;
            Type = type;
        }
    }
}
