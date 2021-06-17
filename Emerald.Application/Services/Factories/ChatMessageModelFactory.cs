using Emerald.Application.Models.Chat;
using Emerald.Domain.Models.ChatAggregate;
using Emerald.Domain.Models.ChatMessageAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Application.Services.Factories
{
    public class ChatMessageModelFactory : IModelFactory<ChatMessage, ChatMessageModel>
    {
        private IChatRepository chatRepository;

        public ChatMessageModelFactory(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public async Task<ChatMessageModel> Create(ChatMessage source)
        {
            Chat chat = await chatRepository.EmplaceGet(
                source.SenderId,
                source.ReceiverId);

            switch (source)
            {
                case TextChatMessage message:
                    return new TextChatMessageModel(
                            source.CreationTime,
                            chat.LastTimeReceived > source.CreationTime,
                            chat.UserSenderId,
                            message.Text);

                case ImageChatMessage message:
                    return new ImageChatMessageModel(
                            source.CreationTime,
                            chat.LastTimeReceived > source.CreationTime,
                            chat.UserSenderId,
                            message.ImageId);

                default:
                    throw new DomainException("Got invalid chatmessage tye");
            }
        }
    }
}
